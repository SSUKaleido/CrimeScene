using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
* C#의 확장 메서드를 다루는 클래스
*/
public static class Extension
{
  // 확장 메서드
	public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component 
	{
		return Util.GetOrAddComponent<T>(go);
	}

  // 확장메서드
	public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) 
	{
		UI_Base.BindEvent(go, action, type);
	}
}