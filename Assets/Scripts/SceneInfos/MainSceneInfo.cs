using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR.ARFoundation;

public class MainSceneInfo : BaseSceneInfo
{
    //private AREvidenceHolder EvidenceHolder;
    private XROrigin _xrOrigin = null;
    private ARSession _arSession = null;

	protected override void Init() // 상속 받은 Awake() 안에서 실행됨. "MainScene"씬 초기화
    {
        base.Init(); // BaseScene의 Init()

        SceneType = Define.Scene.MainScene;
        GameManager.UI.ShowSceneUI<UI_MainScene_SceneMenu>("MainScene_SceneMenu");

        GameManager.Input.AddInputAction(ActiveEscapeKey);

        InitXRSession();
        
        //EvidenceHolder = GameObject.FindObjectOfType<AREvidenceHolder>();

        /**
        * 그 외 기타 MainScene 로딩 코드는 여기다 추가하면 됨!
        */

        #if UNITY_EDITOR
            TestInstant();
        #endif
	}

    public override void Clear()
    {
        GameManager.Input.RemoveInputAction(ActiveEscapeKey);
    }

    /**
    * 취소 키 입력받았을 때 반응
    * 아무런 팝업 화면이 없었다면 일시정지 메뉴, 일시정지 메뉴였다면 메인 화면으로
    * 그 외 다른 팝업 UI였다면 팝업 UI 닫음
    */ 
    public void ActiveEscapeKey() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string CurrentTopUIType = GameManager.UI.CurrentTopUI().GetType().ToString();
            
            if (CurrentTopUIType == "UI_MainScene_PopupPauseMenu") {
                GameManager.Scene.LoadScene(Define.Scene.StartScene);
            }
            else if (CurrentTopUIType == "UI_MainScene_SceneMenu") {
                GameManager.UI.ShowPopupUI<UI_MainScene_PopupPauseMenu>("MainScene_PopupPauseMenu");
            }
            else {
                GameManager.UI.ClosePopupUI();
                if (GameManager.UI.CurrentTopUI().GetType().ToString() == "UI_MainScene_SceneMenu")
                {
                    SetCameraOn();
                }
            }   
        }
    }

    /**
    * Init()에서 처리하면 아직 ARSession, XROrigin이 직렬화되지 않은 상황에서 코드를 처리하려 할 수 있음.
    * 유니티 에디터에서는 작동하는 것 같아도 실제 모바일 빌드하면 동작 안하니 주의.
    * XROrigin, ARSession은 반드시 프리펩으로 씬에 생성되어 있어야 함.
    * 그렇지 않으면, 런타임 세션에서 초기화 처리해야 함. 방법 찾으면 수정 바람.
    */
    private void InitXRSession()
    {
        /**
        * 씬에서 XR 오리진, AR 세션을 찾음
        * 없으면 직접 생성
        */
        _xrOrigin = GameObject.FindObjectOfType<XROrigin>();
        if (_xrOrigin == null)
        {
            GameObject obj = GameManager.Resource.Instantiate("XROrigin");
            obj.name = "XROrigin";
            _xrOrigin = obj.GetComponent<XROrigin>();
        }

        _arSession = GameObject.FindObjectOfType<ARSession>();
        if (_arSession == null)
        {
            GameObject obj = GameManager.Resource.Instantiate("ARSession");
            obj.name = "ARSession";
            _arSession = obj.GetComponent<ARSession>();
        }
    }

    // AR 화면을 끔.
    public void SetCameraOff()
    {
        _xrOrigin.enabled = false;
        _arSession.enabled = false;
    }

    // AR 화면을 켬.
    public void SetCameraOn()
    {
        _xrOrigin.enabled = true;
        _arSession.enabled = true;
        _arSession.Reset();
    }

    private void TestInstant()
    {
        GameManager.Ingame.CreateEvidenceModel("FingerprintFilm");
    }
}