using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/**
* StartScene_SceneMenu 캔버스에 붙을 컴포넌트
* 유니티 에디터에서 오브젝트들을 바인딩하지 않고 코드로 연결하려고 사용
*/
public class UI_StartScene_SceneMenu : UI_Scene
{
    enum Images
    {
    }

    enum Buttons
    {
        GameStartButton,
        GameCountinueButton,
        GameExitButton,
        SettingButton
    }

    enum Texts
    {
    }

    enum GameObjects
    {
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        /** Button, Text, Image 오브젝트들을 가져와 _objects 딕셔너리에 바인딩 **/
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        // (확장 메서드) 버튼들에 UI_EvenetHandler를 붙이고 각 메서드를 등록한다.
        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnGameStartButton);
        GetButton((int)Buttons.GameCountinueButton).gameObject.BindEvent(OnGameCountinueButton);
        GetButton((int)Buttons.GameExitButton).gameObject.BindEvent(OnGameExitButton);
        GetButton((int)Buttons.SettingButton).gameObject.BindEvent(OnSettingButton);
	}

    private void OnGameStartButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_StartScene_PopupSubmitCaseCodeMenu>("StartScene_PopupSubmitCaseCodeMenu");
    }

    private void OnGameCountinueButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_StartScene_PopupCountinueCaseMenu>("StartScene_PopupCountinueCaseMenu");
    }

    private void OnGameExitButton(PointerEventData data) {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnSettingButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_StartScene_PopupSettingMenu>("StartScene_PopupSettingMenu");
    }
}