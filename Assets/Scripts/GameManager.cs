using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string currentScene = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        currentScene = SceneManager.GetActiveScene().name;
    }

	public void LoadScene(string sceneType)
    {
        SceneManager.LoadScene(sceneType);
        return;
    }


    private bool ExitContinuityCheck = false;
    private string toastMessage = "취소 명령을 한 번 더 입력하면 종료합니다";

    public void EscapeScene() {
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
            if (currentScene == "MainScene")
                LoadScene("StartScene");
            else {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    ApplicationQuit();
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

    public void ApplicationQuit() {
        Application.Quit();
    }
}
