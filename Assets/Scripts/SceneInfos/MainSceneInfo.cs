using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInfo : BaseSceneInfo
{
    protected override void Init() // 상속 받은 Awake() 안에서 실행됨. "MainScene"씬 초기화
    {
        base.Init(); // BaseScene의 Init()

        SceneType = Define.Scene.MainScene;
        GameManager.UI.ShowSceneUI<UI_MainScene_SceneMenu>("MainScene_SceneMenu");

        GameManager.Input.AddInputAction(ActiveEscapeKey);
        
        //EvidenceHolder = GameObject.FindObjectOfType<AREvidenceHolder>();

        /**
        * 그 외 기타 MainScene 로딩 코드는 여기다 추가하면 됨!
        */
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
            if (GameManager.UI.CurrentTopUI() is UI_MainScene_PopupPauseMenu)
            {
                GameManager.Scene.LoadScene(Define.Scene.StartScene);
            }
            else
            {
                GameManager.UI.ShowPopupUI<UI_MainScene_PopupPauseMenu>("MainScene_PopupPauseMenu");
            }
        }
    }
}