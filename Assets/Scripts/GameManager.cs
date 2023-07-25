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

    void Update()
    {
        
    }

    private bool ExitContinuityCheck = false;

    public IEnumerator ApplicationQuit()
    {
        if (ExitContinuityCheck == false)
        {
            ExitContinuityCheck = true;
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
