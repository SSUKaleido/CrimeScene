using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 각 씬의 정보를 담고 있는, 정적으로 생성되어 있어야 할 오브젝트 SceneInfo에 붙는 컴포넌트
*/
public abstract class BaseSceneInfo : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown; // 디폴트로 Unknow 이라고 초기화

	void Awake()
	{
		Init();
	}

    /**
    * 씬이 처음 로드되었을 때 해야 할 것들, 오버라이딩해서 사용
    * 모든 UI 기능에는 EventSystem이 필요하므로 그걸 인스턴스 생성함
    */
	protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            GameManager.Resource.Instantiate("UI/EventSystem").name = "EventSystem";
    }

    /**
    * 현재 씬을 닫고 다음 씬을 로드하기 전 현재 씬을 청소하는 함수
    * 오버라이딩 해서 사용
    */
    public abstract void Clear();
}