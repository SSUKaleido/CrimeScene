using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 프리팹 생성을 관리하는 매니저
* GameManager.Resource.Instatiate("KaleidoIcon")으로만 해도 알아서 프리펩 찾아서 생성해 줍니다
*/
public class ResourceManager 
{
    /**
    * 파일 경로를 받아와서 그 오브젝트를 반환해주는 메서드
    * @param path 이 path에 해당하는 경로의 T타입 오브젝트를 반환
    */
    public T Load<T>(string path) where T : Object
    {
        // Resources 폴더를 시작 경로로 하는 path에 해당하는 T 타입의 에셋 타입을 반환
        return Resources.Load<T>(path);
    }

    /**
    * 사용자 지정 Instantiate 메서드
    * @param path 인스턴스화 할 오브젝트의 이름 Load<T>(string path) 메서드에서 알아서 경로를 찾아서 생성해줌
    */
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // Load<T>(path) 메서드에서 오브젝트를 찾아 옴
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        // 못 찾았으면 null 리턴
        if (prefab == null)
        {
            Debug.Log($"Filed to load prefab : {path}");
            return null;
        }

        //찾은 오브젝트를 생성. 그냥 Instantiate() 하면 재귀 호출되므로 Object.Instatntiate()
        return Object.Instantiate(prefab, parent);
    }

    /** 사용자 지정 Destroy 메서드
    * @param go 없앨 오브젝트
    */
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 그냥 Destroy()하면 재귀 호출되므로 Object.Destroy()
        Object.Destroy(go);
    }
}