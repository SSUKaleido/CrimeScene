using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private bool ExitContinuityCheck = false;
    private string toastMessage = "\'취소\' 명령을 한 번 더 입력하면 종료합니다";

    public IEnumerator ApplicationQuit()
    {
        if (ExitContinuityCheck == false)
        {
            ExitContinuityCheck = true;
            #if UNITY_EDITOR
                Debug.Log(toastMessage);
            #else
                ToastMessageHelper.ShowToastMessage(toastMessage);
            #endif
            yield return new WaitForSeconds(1.5f);
            ExitContinuityCheck = false;
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
