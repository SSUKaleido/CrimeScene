using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    /** UI 정렬 순서. 고정 UI와 겹치지 않도록 10부터 레이어 카운트. 높은 숫자일수록 늦게 생성된 것 */
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); // 팝업 UI 캔버스들의 UI_Popup 스크립트들을 담을 스택
    UI_Scene _sceneUI = null; // 현재의 고정 캔버스 UI

    /**
    * UI_Root를 생성하는 프로퍼티
    * UI_Root는 하이어라키 창의 오브젝트들을 정리할 용도
    */
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UI_Root");
            if (root == null)
                root = new GameObject { name = "UI_Root" };
            return root;
		}
    }

    /**
    * 새로운 캔버스 세팅
    * @param go 추가할 캔버스, 캔버스에 Canvas 컴포넌트가 없으면 붙여서 가져옴
    * @param sort 정렬할지 여부. 정렬 X = 씬 고정 UI 캔버스라는 뜻
    */
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // 캔버스 렌더러 모드 ScreenSpaceOverlay
        canvas.overrideSorting = true; // 캔버스 안에 캔버스 중첩 경우 (부모 캔버스가 어떤 값을 가지던 나는 내 오더값을 가지려 할때)

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // soring 요청 X 라는 소리는 팝업이 아닌 일반 고정 UI
        {
            canvas.sortingOrder = 0;
        }
    }

    /**
    * SceneUI를 생성하는 메서드
    * 캔버스를 생성한 뒤 UI_Root의 자식으로 넣음
    */
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    /**
    * PopupUI를 생성하는 메서드
    * 캔버스를 생성한 뒤 UI_Root의 자식으로 넣음
    */
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name)) // 이름을 안받았다면 T로
            name = typeof(T).Name;

        GameObject go = GameManager.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

		return popup;
    }

    /**
    * 팝업 UI를 삭제하는 메서드.
    * 해당 UI가 가장 위에 있는 UI가 맞는지 검사하고 ClosePopupUI()를 호출함
    */
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0) // 비어있는 스택이라면 삭제 불가
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!"); // 스택의 가장 위에있는 Peek() 것만 삭제할 수 잇기 때문에 popup이 Peek()가 아니면 삭제 못함
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        GameManager.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--; // order 줄이기
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public UI_Base CurrentTopUI() {
        if (_popupStack.Count == 0)
            return _sceneUI;
        return _popupStack.Peek();
    }
}