using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 여러 매니저들을 통합적으로 관리하기 위해 사용하는 클래스
* GameManager.Ingame.…… 식으로 사용하면 됩니다
*/
public class GameManager : MonoBehaviour
{
    /** 인스턴스를 프로퍼티로 호출 **/
    static GameManager _instace;  
    public static GameManager Instance
    {
        get
        {
            // GameManager.cs의 start()가 실행되기 전에 다른 오브젝트가 인스턴스를 참조할 수 있으니 Init()
            Init();
            return _instace;
        }
    }

    /** 입력을 관리하는 InputManager **/
    InputManager _input = new InputManager();
    /** 프리펩 생성을 관리하는 ResourceManager **/
    ResourceManager _resource = new ResourceManager();
    /** 씬 로드를 관리하는 SceneLoadManager **/
    SceneLoadManager _SceneLoad = new SceneLoadManager();
    /** 팝업 UI 생성, 파괴를 관리하는 UIPopupMangaer **/
    UIManager _UI = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get  { return Instance._resource; } }
    public static SceneLoadManager SceneLoad { get { return Instance._SceneLoad; } }
    public static UIManager UI { get { return Instance._UI; } }


    /** 이 오브젝트가 생성되면 Init() 수행 **/
    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    /**
    * Mangers.cs를 컴포넌트로 가지는 GameManager 오브젝트 초기화
    */
    static void Init()
    {
        // GameManager 인스턴스가 없을 경우
        if (_instace == null)
        {
            // 존재하는 GameManager 인스턴스를 찾음
            GameObject obj = GameObject.Find("GameManager");

            // 찾아봐도 GameManager 인스턴스가 없으면
            if (obj == null) 
            {
                // 새 GameManager 인스턴스를 만들고 GameManager.cs를 컴포넌트로 추가
                obj = new GameObject { name = "GameManager" };
                obj.AddComponent<GameManager>();
            }

            // 씬이 바뀌어도 파괴되지 않도록(싱글톤) DonDestroyOnLoad()
            DontDestroyOnLoad(obj);
            //인스턴스 할당
            _instace = obj.GetComponent<GameManager>();
        }
    }
}
