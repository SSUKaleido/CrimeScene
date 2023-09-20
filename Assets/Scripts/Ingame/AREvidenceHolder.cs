using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

/**
* AR Session Origin에 AR Tracked Image Manager와 함께 붙어 마커 인식을 수행하는 스크립트
* Tacked Image Manager에 감지 시 메서드를 등록하고(옵저버 패턴) 프리펩을 생성함.
* 카메라 이동·회전에 따라 오브젝트 추적과 스와이프에 따른 오브젝트 회전을 담당함.
*/
public class AREvidenceHolder : MonoBehaviour
{
    private ARTrackedImageManager trackedEvidenceMarkerManager = null;
    private Camera mainCamera = null;

     // AR Image Tracker에서 GameObject == null을 검사하는 것보다 ARTrackedImage == null을 검사하는 게 더 빠름
    private ARTrackedImage _trackedImage;
    private GameObject HoldingEvidence = null;
    private float swipeSensitivity;

    void Awake()
    {
        trackedEvidenceMarkerManager = gameObject.GetComponent<ARTrackedImageManager>();
        mainCamera = Camera.main;

        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        swipeSensitivity = Mathf.Max(screenSize.x, screenSize.y) / 10f;

        StartTrackEvidence();
    }

    public void StartTrackEvidence() {
        HoldingEvidence = null;
        trackedEvidenceMarkerManager.trackedImagesChanged += TrackedEvidenceMarker;
    }

    public void StopTrackEvidence() {
        trackedEvidenceMarkerManager.trackedImagesChanged -= TrackedEvidenceMarker;
    }

    /**
    * 단서 발견을 위해 사용할 메서드.
    * TrackedEvidenceMarkerManager에 등록되어야 함.
    * @param eventArgs AR Foundation 이미지 트래킹 이벤트. 여기서 이벤트 정보를 받아와 어떤 마커를 인식했는지 전달.
    */
    public void TrackedEvidenceMarker(ARTrackedImagesChangedEventArgs eventArgs)
    {
        /**
        * eventArgs에는 다음 값들이 있음
        * added: 처음 이미지가 인식되었을 때.
        * updated: 이미지가 계속 인식되고 있을 때.
        * removed: 이미지가 더는 인식되지 않을 때, 그러나 AR Foundation의 결함으로 실제로는 호출되지 않음
        * foreach 문을 통해 이벤트에서 인식된 이미지를 가져옴.
        */
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            /**
            * UnityEngine.XR.ARSubsystems.TrackingState.Tracking Enum에는 다음 값들이 있음
            * None: 초기 값. 아직 아무런 이미지도 인식하지 못한 상태.
            * Limited: 문제가 생김. 이미지가 잘 보이지 않거나 보이던 이미지가 사라진 상태.
            * Tacking: 이미지가 잘 보임.
            *
            * 문제는 removed 이벤트가 호출되지 않기 때문에 마커를 한 번 인식시키면 마커가 사라져도 그 마커가 Limited 상태로 계속 인식된다고
            * Tracking Image Manager가 전달함.
            * 따라서 인식한 이미지의 TrackingState가 Tracking인지 확인하는 조건문을 이용하여야 함.
            */
            if (_trackedImage == null && trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                CatchHoldingEvidence(trackedImage);
            }
            
        }
        /** added 이벤트가 무시될 수도 있으므로 update 이벤트에서도 같은 코드 실행 */
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (_trackedImage == null && trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                CatchHoldingEvidence(trackedImage);
            }
        }
    }

    private void CatchHoldingEvidence(ARTrackedImage trackedImage)
    {
        if (HoldingEvidence != null)
            Destroy(HoldingEvidence);
        
        /** 새 홀딩 프리펩을 생성 */
        string markerName = trackedImage.referenceImage.name;
        Evidence newEvidence = GameManager.Ingame.GetEvidencePerMarker(markerName);
        HoldingEvidence = GameManager.Ingame.CreateEvidenceModel(newEvidence);
        _trackedImage = trackedImage;

        StopTrackEvidence(); // 이미지 마커 추적 그만
        GameManager.Ingame.PrograssData.FindNewEvidence(newEvidence);
        StartHoldingEvidencePos();
        GameManager.Input.AddInputAction(TouchControlEvidence);
    }

    /**
    * 단서 그만 보기 용도 메서드.
    */
    private void RemoveHoldingEvidence()
    {
        if (HoldingEvidence != null)
            GameManager.Resource.Destroy(HoldingEvidence);
        HoldingEvidence = null;
        
        _trackedImage = null;
        GameManager.Input.RemoveInputAction(TouchControlEvidence);
        StartTrackEvidence();
    }

    /** 홀딩하고 있는 단서가 카메라 앞에 위치하도록 위치와 회전을 변경하는 메서드 **/
    private void StartHoldingEvidencePos()
    {
        StartCoroutine(UpdateHoldingEvidencePos());
    }

    private Vector2 beganTouchPos;
    private Vector2 curTouchPos;
    private bool isNowSwiping = false;
    private bool doubleClickCheck = false;
    private const float doubleTouchDelay = 0.3f;
    /**
    * 화면 스와이프 단서 회전과 더블 클릭 단서 제거를 담당하는 메서드
    * 이 메서드는 GameManager.Input에 등록되어야 함.
    */
    public void TouchControlEvidence()
    {
        if (Input.touchCount > 0)
        {
            Touch inputTouch = Input.GetTouch(0);

            /**
            * inputTouch.phase Enum에는 다음 값들이 있음.
            * Began: 화면 터치가 시작되었을 때
            * Moved: 터치가 움직였을 때
            * Ended: 터치가 끝났을 때
            */
            switch (inputTouch.phase)
            {
                case TouchPhase.Began :
                {
                    /** 스와이프 중 중복 입력을 막기 위해 break; */
                    if (isNowSwiping == true)
                        break;
                    
                    Debug.Log(0);
                    /** 더블 클릭 코루틴 구절 */
                    if (doubleClickCheck == false)
                    {
                        StartCoroutine(SwitchDoubleClick());
                    }
                    else
                    {
                        RemoveHoldingEvidence();
                        doubleClickCheck = false;
                    }
                    
                    beganTouchPos = inputTouch.position;

                    break;
                }
                case TouchPhase.Moved :
                {
                    if (isNowSwiping)
                    {
                        Vector3 deltaPos = inputTouch.deltaPosition;
                        Vector3 rotationAxis = mainCamera.transform.right * deltaPos.y - mainCamera.transform.up * deltaPos.x;
                        HoldingEvidence.transform.Rotate(rotationAxis, Space.World);
                    }
                    else
                    {
                        curTouchPos = inputTouch.position;
                        Vector2 curDiffPos = curTouchPos - beganTouchPos;
                        if (curDiffPos.magnitude > swipeSensitivity)
                        {
                            isNowSwiping = true;
                        }
                    }

                    break;
                }
                case TouchPhase.Ended :
                {
                    isNowSwiping = false;
                    break;
                }
            }
        }
    }

    private IEnumerator SwitchDoubleClick()
    {
        doubleClickCheck = true;
        yield return new WaitForSeconds(doubleTouchDelay);
        doubleClickCheck = false;
        yield break;
    }

    private IEnumerator UpdateHoldingEvidencePos()
    {
        while (true)
        {
            if (HoldingEvidence != null)
            {
                Vector3 cameraPosition = mainCamera.transform.position;
                Vector3 cameraForward = mainCamera.transform.forward;

                HoldingEvidence.transform.position = cameraPosition + cameraForward;
            }
            else
            {
                yield break;
            }

            yield return null;
        }
    }
}