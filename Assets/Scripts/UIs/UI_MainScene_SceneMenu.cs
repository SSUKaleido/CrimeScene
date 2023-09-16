using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_SceneMenu : UI_Scene
{
    enum Images
    {
    }

    enum Buttons
    {
        CaseMenuButton,
        InvestigationMenuButton,
        DeductionMenuButton,
        EncyclopediaMenuButton,
        PointRealCriminalButton,
        SearchEvidenceButton,
        PauseButton
    }

    enum Texts
    {
        CaseCodeText,
        CaseNameText
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
        GetButton((int)Buttons.CaseMenuButton).gameObject.BindEvent(OnCaseMenuButton);
        GetButton((int)Buttons.InvestigationMenuButton).gameObject.BindEvent(OnInvestigationMenuButton);
        GetButton((int)Buttons.DeductionMenuButton).gameObject.BindEvent(OnDeductionMenuButton);
        GetButton((int)Buttons.EncyclopediaMenuButton).gameObject.BindEvent(OnEncyclopediaMenuButton);
        GetButton((int)Buttons.PointRealCriminalButton).gameObject.BindEvent(OnPointRealCriminalButton);
        GetButton((int)Buttons.SearchEvidenceButton).gameObject.BindEvent(OnSearchEvidenceButton);
        GetButton((int)Buttons.PauseButton).gameObject.BindEvent(OnPauseButton);
	}

    private void OnCaseMenuButton(PointerEventData data)
    {
        GameManager.UI.CloseAllPopupUI();
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupCaseMenu>("MainScene_PopupCaseMenu");
    }

    private void OnInvestigationMenuButton(PointerEventData data)
    {
        GameManager.UI.CloseAllPopupUI();
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupInvestigationMenu>("MainScene_PopupInvestigationMenu");
    }

    private void OnDeductionMenuButton(PointerEventData data)
    {
        GameManager.UI.CloseAllPopupUI();
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupDeductionMenu>("MainScene_PopupDeductionMenu");
    }

    private void OnEncyclopediaMenuButton(PointerEventData data)
    {
        GameManager.UI.CloseAllPopupUI();
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupEncyclopediaMenu>("MainScene_PopupEncyclopediaMenu");
    }

    private void OnPointRealCriminalButton(PointerEventData data)
    {
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupPointRealCriminalMenu>("MainScene_PopupPointRealCriminalMenu");
    }

    private void OnSearchEvidenceButton(PointerEventData data)
    {
        GameManager.Scene.LoadScene(Define.Scene.ARCaptureScene);
    }

    private void OnPauseButton(PointerEventData data)
    {
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupPauseMenu>("MainScene_PopupPauseMenu");
    }
}
