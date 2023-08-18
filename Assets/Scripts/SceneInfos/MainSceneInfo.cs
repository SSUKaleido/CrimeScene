using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneInfo : BaseSceneInfo
{
	protected override void Init() // 상속 받은 Awake() 안에서 실행됨. "MainScene"씬 초기화
    {
        base.Init(); // BaseScene의 Init()

        SceneType = Define.Scene.MainScene;
        GameManager.UI.ShowSceneUI<UI_MainScene_SceneMenu>("MainScene_SceneMenu");

        GameManager.Input.AddInputAction(ActiveEscapeKey);

        /**
        * 그 외 기타 MainScene 로딩 코드는 여기다 추가하면 됨!
        */
	}

    public override void Clear()
    {
        GameManager.Input.RemoveInputAction(ActiveEscapeKey);
    }

    public void ActiveEscapeKey() {
        // 일시 정지 메뉴 열고 메인 씬으로
    }
}