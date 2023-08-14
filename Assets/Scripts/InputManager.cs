using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActiveEscapeKey();
        }
    }

    public void ActiveEscapeKey() {
        GameManager.instance.EscapeScene(); // 추후 메인 씬 UI 개발 후 이곳에 일시정지 메뉴 팝업 넣기
    }
}