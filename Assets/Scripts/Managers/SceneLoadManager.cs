using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
* 씬 로드를 관리하는 매니저
* 씬 로드와 로드하기 전 씬 청소를 담당함
**/
public class SceneLoadManager
{
    /** BaseScene(부모 타입) 오브젝트를 찾아서 CurrentScene에 할당
    * SceneInfo는 각 씬에 정적으로 생성되어 있어야 함
    */
    public BaseSceneInfo CurrentScene { get { return GameObject.FindObjectOfType<BaseSceneInfo>(); } }

    /**
    * 현재 씬의 이름을 반환
    * @param type 사용자 정의한 Scene enum
    */
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type); // C#의 Reflection. Scene enum의 
        return name;
    }

    /**
    * 새 씬을 로드
    * @param type 사용자 정의한 Scene enum의 따른 씬 종류
    */
	public void LoadScene(Define.Scene type)
    {
        GameManager.Clear();

        SceneManager.LoadScene(GetSceneName(type)); // SceneManager는 UnityEngine의 SceneManager
    }

    /**
    * 새 씬을 로드하기 전 현재 씬을 청소
    */
    public void Clear()
    {
        CurrentScene.Clear();
    }
}