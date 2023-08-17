using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_StartScene_SceneMenu : UI_Scene
{
    enum Images
    {
        BackgroundPanel
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
        TitleText,
        GameStartButtonText,
        GameCountinueButtonText,
        GameExitButtonText,
        SettingButtonText
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

        // (확장 메서드) 버튼들에 UI_EvenetHandler를 붙이고 각 메서드를 등록한다.
        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnGameStartButton);
        GetButton((int)Buttons.GameCountinueButton).gameObject.BindEvent(OnGameCountinueButton);
        GetButton((int)Buttons.GameExitButton).gameObject.BindEvent(OnGameExitButton);
        GetButton((int)Buttons.SettingButton).gameObject.BindEvent(OnSettingButton);
	}

    public void OnGameStartButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_StartScene_PopupSubmitCaseCodeMenu>("StartScene_PopupSubmitCaseCodeMenu");
    }

    public void OnGameCountinueButton(PointerEventData data) {
        //GameManger.UI.ShowPopupUI<UI_StartScene_PopupCountinueCaseMenu>("StartScene_PopupCountinueCaseMenu");
    }

    public void OnGameExitButton(PointerEventData data) {
        Application.Quit();
    }

    public void OnSettingButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_StartScene_PopupSettingMenu>("StartScene_PopupSettingMenu");
    }
}