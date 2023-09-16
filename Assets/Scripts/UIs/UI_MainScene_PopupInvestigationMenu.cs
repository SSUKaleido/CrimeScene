using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupInvestigationMenu : UI_Popup
{
    enum Images
    {
        EvidencePanel
    }

    enum Buttons
    {
        PointRealWeaponButton,
        CompareFingerprintButton
    }

    enum Texts
    {
        EvidenceNameText,
        EvidenceExplainText
    }

    enum GameObjects
    {
        Slot1,
        Slot2,
        Slot3,
        Slot4,
        Slot5,
        Slot6,
        Slot7,
        Slot8,
        Slot9,
        Slot10,
        Slot11,
        Slot12,
        Slot13
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
    }

    private void OnPointRealWeaponButton(PointerEventData data)
    {

    }

    private void OnCompareFingerprintButton(PointerEventData data)
    {
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupCompareFingerprintMenu>("MainScene_PopupCompareFingerprintMenu");
    }
}
