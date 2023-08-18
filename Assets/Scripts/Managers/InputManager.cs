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

    public void Clear()
    {
        KeyAction = null;
        //MouseAction = null;
    }
}