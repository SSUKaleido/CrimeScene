using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
* 팝업 UI 캔버스들에 붙을 스크립트
*/
public class UI_Popup : UI_Base
{
    public override void Init()
    {
        GameManager.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()  // 팝업이니까 고정 캔버스(Scene)과 다르게 닫는게 필요
    {
        GameManager.UI.ClosePopupUI(this);
    }

    public void CloseThisPopupUI(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        GameManager.UI.ClosePopupUI(this);
    }
}