using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

    /**
    * 오브젝트의 모든 자식 오브젝트에 접근해서 name이 같은 오브젝트를 찾는 메서드
    * @param go 찾기 시작할 오브젝트
    * @param name 찾을 자식 오브젝트의 이름
    * @param recursive 자식의 자식까지 찾을 지 여부
    */
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
		}
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    /**
    * 바로 위 FindChild()의 제네릭 아닌 버전
    * 컴포넌트가 아닌 GameObject를 찾아 넘김
    * @param go 찾기 시작할 오브젝트
    * @param name 찾을 자식 오브젝트의 이름
    * @param recursive 자식의 자식까지 찾을 지 여부
    */
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    /**
    * 오브젝트 go에 컴포넌트 T가 있다면 제거하고, 없으면 붙여준다
    */
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
		if (component == null)
            component = go.AddComponent<T>();
        return component;
	}
}