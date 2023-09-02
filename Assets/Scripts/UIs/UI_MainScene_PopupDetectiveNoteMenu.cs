using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupDetectiveNoteMenu : UI_Popup
{
    private MainSceneInfo _mainSceneInfo;
    enum Images
    {
    }

    enum Buttons
    {
        CancleButton,
        CaseEntryButton,
        InvestigationEntryButton,
        DeductionEntryButton
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

        //AR 카메라 비활성화 시키는 용도로
        _mainSceneInfo = GameObject.FindWithTag("SceneInfo").GetComponent<MainSceneInfo>();

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
        GetButton((int)Buttons.CaseEntryButton).gameObject.BindEvent(OnCaseEntryButton);
        GetButton((int)Buttons.InvestigationEntryButton).gameObject.BindEvent(OnInvestigationEntryButton);
        GetButton((int)Buttons.DeductionEntryButton).gameObject.BindEvent(OnDeductionEntryButton);
	}

    new public void CloseThisPopupUI(PointerEventData data) {
        _mainSceneInfo.SetCameraOn();

        string currentPopupMenuName = GameManager.UI.CurrentTopUI().GetType().ToString();
        while (currentPopupMenuName != "PopupDetectiveNoteMenu") {
            GameManager.UI.ClosePopupUI();
            currentPopupMenuName = GameManager.UI.CurrentTopUI().GetType().ToString();
        }
        GameManager.UI.ClosePopupUI(this);
    }

    public void OnCaseEntryButton(PointerEventData data) {
        // 사건 항목 팝업 메뉴 띄우기
    }

    public void OnInvestigationEntryButton(PointerEventData data) {
        // 조사 항목 팝업 메뉴 띄우기
    }

    public void OnDeductionEntryButton(PointerEventData data) {
        // 추리 항목 팝업 메뉴 띄우기
    }
}
