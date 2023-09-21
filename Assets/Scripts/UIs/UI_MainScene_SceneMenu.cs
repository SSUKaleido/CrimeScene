using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_SceneMenu : UI_Scene
{
    int currnetMenuButtonIndex = 3;
    List<Image> MenuButtons = new List<Image>();
    Sprite[] MenuButtonSprites;

    enum Images
    {
        CaseMenuButton,
        InvestigationMenuButton,
        DeductionMenuButton,
        EncyclopediaMenuButton
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

        MenuButtons.Add(GetImage((int)Images.CaseMenuButton));
        MenuButtons.Add(GetImage((int)Images.InvestigationMenuButton));
        MenuButtons.Add(GetImage((int)Images.DeductionMenuButton));
        MenuButtons.Add(GetImage((int)Images.EncyclopediaMenuButton));
        MenuButtonSprites = GameManager.Resource.LoadAll<Sprite>("Sprites/GUI3");

        InitTexts();
	}

    private void OnCaseMenuButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect2");
        int previewIndex = DespawnCurrentMenu(0);
        GameManager.UI.CloseAllPopupUI();
        UI_MainSceneMenu newMenu = GameManager.UI.ShowPopupUI<UI_MainScene_PopupCaseMenu>("MainScene_PopupCaseMenu") as UI_MainSceneMenu;
        SpawnCurrentMenu(newMenu, previewIndex);
        ChangeMenuImage(0);
    }

    private void OnInvestigationMenuButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect2");
        int previewIndex = DespawnCurrentMenu(1);
        GameManager.UI.CloseAllPopupUI();
        UI_MainSceneMenu newMenu = GameManager.UI.ShowPopupUI<UI_MainScene_PopupInvestigationMenu>("MainScene_PopupInvestigationMenu") as UI_MainSceneMenu;
        SpawnCurrentMenu(newMenu, previewIndex);
        ChangeMenuImage(1);
    }

    private void OnDeductionMenuButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect2");
        int previewIndex = DespawnCurrentMenu(2);
        GameManager.UI.CloseAllPopupUI();
        UI_MainSceneMenu newMenu = GameManager.UI.ShowPopupUI<UI_MainScene_PopupDeductionMenu>("MainScene_PopupDeductionMenu") as UI_MainSceneMenu;
        SpawnCurrentMenu(newMenu, previewIndex);
        ChangeMenuImage(2);
    }

    private void OnEncyclopediaMenuButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect2");
        int previewIndex = DespawnCurrentMenu(3);
        GameManager.UI.CloseAllPopupUI();
        UI_MainSceneMenu newMenu = GameManager.UI.ShowPopupUI<UI_MainScene_PopupEncyclopediaMenu>("MainScene_PopupEncyclopediaMenu") as UI_MainSceneMenu;
        SpawnCurrentMenu(newMenu, previewIndex);
        ChangeMenuImage(3);
    }

    private void ChangeMenuImage(int nextMenuButtonIndex)
    {
        switch (currnetMenuButtonIndex)
        {
            case 0 : MenuButtons[0].sprite = MenuButtonSprites[0]; break;
            case 1 : MenuButtons[1].sprite = MenuButtonSprites[3]; break;
            case 2 : MenuButtons[2].sprite = MenuButtonSprites[1]; break;
            case 3 : MenuButtons[3].sprite = MenuButtonSprites[2]; break;
        }
        switch (nextMenuButtonIndex)
        {
            case 0 : MenuButtons[0].sprite = MenuButtonSprites[4]; break;
            case 1 : MenuButtons[1].sprite = MenuButtonSprites[7]; break;
            case 2 : MenuButtons[2].sprite = MenuButtonSprites[5]; break;
            case 3 : MenuButtons[3].sprite = MenuButtonSprites[6]; break;
        }
        currnetMenuButtonIndex = nextMenuButtonIndex;
    }

    private int DespawnCurrentMenu(int nextIndex)
    {
        UI_Base curUI = GameManager.UI.CurrentTopUI();
        int privewIndex = 0;
        
        if (curUI is UI_MainSceneMenu)
        {
            UI_MainSceneMenu curMenu = curUI as UI_MainSceneMenu;
            privewIndex = curMenu.DespawnAnimation(nextIndex);
        }

        return privewIndex;
    }

    private void SpawnCurrentMenu(UI_MainSceneMenu curMenu, int previewIndex)
    {
        curMenu.SpawnAnimation(previewIndex);
    }

    private void OnPointRealCriminalButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIPopupMenuEffect");
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupPointRealCriminalMenu>("MainScene_PopupPointRealCriminalMenu");
    }

    private void OnSearchEvidenceButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIPopupMenuEffect");
        GameManager.Scene.LoadScene(Define.Scene.ARCaptureScene);
    }

    private void OnPauseButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIPopupMenuEffect");
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupPauseMenu>("MainScene_PopupPauseMenu");
    }

    private void InitTexts()
    {
        GetText((int)Texts.CaseCodeText).text = "사건 코드: " + GameManager.Ingame.CaseData.GetCaseCode();
        GetText((int)Texts.CaseNameText).text = "사건명: " + GameManager.Ingame.CaseData.GetCaseName();
    }
}
