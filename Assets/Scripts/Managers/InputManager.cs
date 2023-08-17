using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* 인풋을 관리하는 매니저
* 다른 클래스에서 옵저버 패턴으로 KeyAction에 입력을 등록하면
* 인풋 매니저에서 통합적으로 실행시킴
**/
public class InputManager
{
    public Action KeyAction = null;

    /** 
    * 인풋 매니저에 액션을 추가해주는 메서드.
    * @param 추가할 인풋 관련 메서드
    */
    public void AddInputAction(Action InputAction)
    {
        KeyAction += InputAction;
    }

    public void RemoveInputAction(Action InputAction)
    {
        KeyAction -= InputAction;
    }

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActiveEscapeKey();
        }
    }

    public void ActiveEscapeKey() {
        GameManager.instance.EscapeScene(); // 추후 메인 씬 UI 개발 후 이곳에 일시정지 메뉴 팝업 넣기
    } */
}