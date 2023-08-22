using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

/**
* AR Session Origin에 AR Tracked Image Manager와 함께 붙어 마커 인식을 수행하는 스크립트
* Tacked Image Manager에 감지 시 메서드를 등록하고(옵저버 패턴) 프리펩을 생성함.
* 카메라 이동·회전에 따라 오브젝트 추적, 스와이프에 따른 오브젝트 회전도 담당.
*/
public class AREvidenceHolder : MonoBehaviour
{
    private ARTrackedImageManager trackedEvidenceMarkerManager = null;
    private Camera mainCamera = null;

    private GameObject HoldingEvidence = null;

    /** 추적한 마커에 따라 생성할 오브젝트를 정해주는 딕셔너리. key: 마커 이름 value: 생성할 프리펩 */
    private Dictionary<string, string> _evidenceDic = new Dictionary<string, string>();

    private Quaternion deltaRotation = Quaternion.Euler(-90,0,0);

    void Awake()
    {
        trackedEvidenceMarkerManager = gameObject.GetComponent<ARTrackedImageManager>();
        mainCamera = gameObject.transform.GetChild(0).GetComponent<Camera>();

        // 임시로 칼레이도 아이콘 모델만 나오도록 함.
        _evidenceDic.Add("KaleidoIcon", "KaleidoIconModel");

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
        * update: 이미지가 계속 인식되고 있을 때.
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
            if (HoldingEvidence == null && trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                CatchHoldingEvidence(trackedImage);
            }
            
        }
        /** added 이벤트가 무시될 수도 있으므로 update 이벤트에서도 같은 코드 실행 */
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (HoldingEvidence == null && trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
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
        markerName = "KaleidoIcon"; // 나중에 이 줄 삭제
        HoldingEvidence = GameManager.Resource.Instantiate(_evidenceDic[markerName]);

        StopTrackEvidence(); // 이미지 마커 추적 그만
        /** SetHoldingEvidencePos를 InptManager에 넣어서 매 프레임마다 실행되도록 함. */
        //GameManager.Input.AddInputAction(SetHoldingEvidencePos);
        GameManager.Input.AddInputAction(TouchControlEvidence);
    }

    /**
    * 단서 그만 보기 용도로 사용할 메서드.
    * 더블 클릭 기능 넣을 것.
    */
    private void RemoveHoldingEvidence()
    {
        if (HoldingEvidence != null)
            Destroy(HoldingEvidence);
        HoldingEvidence = null;
        
        /** 매 프레임마다 실행했던 SetHoldingEvidencePos를 그만 실행 */
        //GameManager.Input.RemoveInputAction(SetHoldingEvidencePos);
        GameManager.Input.RemoveInputAction(TouchControlEvidence);
        StartTrackEvidence();
    }

    /** 홀딩하고 있는 단서가 카메라 앞에 위치하도록 위치와 회전을 변경하는 메서드 **/
    private void SetHoldingEvidencePos()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Quaternion cameraRotation = mainCamera.transform.rotation * deltaRotation;
        Vector3 cameraForward = mainCamera.transform.forward;

        HoldingEvidence.transform.position = cameraPosition + cameraForward;
        HoldingEvidence.transform.rotation = cameraRotation;
    }

    private bool DoubleClickCheck = false;
    /**
    * 화면 스와이프 단서 회전과 더블 클릭 단서 제거를 담당하는 메서드
    * 이 메서드는 GameManager.Input에 등록되어야 함.
    */
    public void TouchControlEvidence()
    {
        if (Input.touchCount > 0)
        {
            Touch inputTouch = Input.GetTouch(0);

            switch (inputTouch.phase)
            {
                /** 화면 터치가 시작되었을 때 */
                case TouchPhase.Began : {
                    if (DoubleClickCheck == false)
                        StartCoroutine(SwitchDoubleClick());
                    else
                        RemoveHoldingEvidence();
                    
                    break;
                }
                case TouchPhase.Moved : {
                    break;
                }
                case TouchPhase.Ended : {
                    break;
                }
            }
        }
    }

    private IEnumerator SwitchDoubleClick()
    {
        DoubleClickCheck = true;
        yield return new WaitForSeconds(0.2f);
        DoubleClickCheck = false;
    }
}