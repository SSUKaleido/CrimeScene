using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
* 클릭, 드래그 등 UI 이벤트를 처리하는 클래스
* IPointerClickHandler, IDragHandler 인터페이스를 상속받아 자동으로 이벤트 메서드를 동작시킨다
* 이 스크립트가 붙는 오브젝트들은 이벤트들을 받을 수 있다
*/
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

	public void OnPointerClick(PointerEventData eventData) // 클릭 이벤트 오버라이딩
	{
		if (OnClickHandler != null)
			  OnClickHandler.Invoke(eventData); // 클릭와 관련된 액션 실행
	}

	public void OnDrag(PointerEventData eventData) // 드래그 이벤트 오버라이딩
    {
		if (OnDragHandler != null)
        OnDragHandler.Invoke(eventData); // 드래그와 관련된 액션 실행
	}
}
