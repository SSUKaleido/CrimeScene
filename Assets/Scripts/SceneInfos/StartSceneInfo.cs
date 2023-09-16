using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneInfo : BaseSceneInfo
{
    protected override void Init()  // 상속 받은 Awake() 안에서 실행됨. "StartScene"씬 초기화
    {
        base.Init();

        SceneType = Define.Scene.StartScene;
        GameManager.UI.ShowSceneUI<UI_StartScene_SceneMenu>("StartScene_SceneMenu");

        GameManager.Input.AddInputAction(ActiveEscapeKey);
        GameManager.Sound.Init();

        /**
        * 그 외 기타 StartScene 로딩 코드는 여기다 추가하면 됨!
        */
    }

    public override void Clear()
    {
        GameManager.Input.RemoveInputAction(ActiveEscapeKey);
    }

    private bool ExitContinuityCheck = false;
    private string toastMessage = "취소 명령을 한 번 더 입력하면 종료합니다";

    public void ActiveEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ExitContinuityCheck == false)
            {
                #if UNITY_EDITOR
                    Debug.Log(toastMessage);
                #else
                    ToastMessageHelper.ShowToastMessage(toastMessage);
                #endif
                StartCoroutine(SwitchEscapeContinuity());
            }
            else
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
        }
    }

    private IEnumerator SwitchEscapeContinuity()
    {
        ExitContinuityCheck = true;
        yield return new WaitForSeconds(1.5f);
        ExitContinuityCheck = false;
    }
}