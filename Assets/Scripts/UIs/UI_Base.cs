using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/**
* 모든 캔버스에 붙을 컴포넌트의 부모
*/
public abstract class UI_Base : MonoBehaviour
{
    // 유니티에 존재하는 오브젝트를 바인딩해 보관할 사전
    // key 값은 오브젝트 타입, Value는 오브젝트
	protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

	public abstract void Init();

    /**
    * 원형 바인드 함수, 캔버스에 존재하는 모든 오브젝트들을 딕셔너리 _objects에 추가함
    * 제네릭 T에는 Button, Image 같은 오브젝트 타입이 들어감
    * @param type 오브젝트의 이름을 담아 구현한 enum의 Relfection
    */
    protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
        // enum type의 요소들을 string[]으로 리턴
		string[] names = Enum.GetNames(type);
        // Dictionary _objects의 value인 오브젝트들을 담을 배열 생성
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		_objects.Add(typeof(T), objects);

        // 오브젝트 타입이 T인 오브젝트들을 Dictionary의 Value인 objects 배열의 원소들에 하나하나 추가
		for (int i = 0; i < names.Length; i++)
		{
            // T는 대부분 Image, Button 같은 컴포넌트겠지만 간혹 Gameobject일수도 있음. 그러니 if문으로 조건 분기
			if (typeof(T) == typeof(GameObject))
				objects[i] = Util.FindChild(gameObject, names[i], true);
			else
				objects[i] = Util.FindChild<T>(gameObject, names[i], true);

			if (objects[i] == null)
				Debug.Log($"Failed to bind({names[i]})");
		}
	}

    protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (_objects.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}

	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); } // 오브젝트로서 가져오기
	protected TextMeshProUGUI  GetText(int idx) { return Get<TextMeshProUGUI >(idx); } // Text로서 가져오기
	protected Button GetButton(int idx) { return Get<Button>(idx); } // Button로서 가져오기
	protected Image GetImage(int idx) { return Get<Image>(idx); } // Image로서 가져오기

    /**
    * 오브젝트에 이벤트 함수들을 바인드하는 메서드
    * @param go 이벤트 함수들을 바인드받을 오브젝트
    * @param action 이벤트 함수 발생 시에 실행될 메서드
    * @param Define에 정의된 이벤트 종류별 enum, 어떤 액션에 등록할 것인지 정의
    */
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
	{
		UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action; // 혹시나 이미 있을까봐 빼줌
				evt.OnClickHandler += action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action; // 혹시나 이미 있을까봐 빼줌
				evt.OnDragHandler += action;
				break;
		}
	}
}