using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class UI_MainScene_PopupCompareFingerprintMenu : UI_Popup
{
    Image suspectsFingerprint = null;
    Image weaponsFingerprint = null;

    enum Images
    {
        SuspectsFingerprint,
        WeaponsFingerprint
    }

    enum Buttons
    {
        BeforeSuspectFingerprintButton,
        NextSuspectFingerprintButton,
        BeforeSWeaponFingerprintButton,
        NextSWeaponFingerprintButton,
        ChangeDetectionFingerprintButton,
        CancleButton
    }

    enum Texts
    {
        SuspectFingerprintExplainText,
        WeaponFingerprintExplainText,
        DetectionFingerprintText
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

        suspectsFingerprint = GetImage((int)Images.SuspectsFingerprint);
        weaponsFingerprint = GetImage((int)Images.WeaponsFingerprint);

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
	}

    private void OnBeforeSuspectFingerprintButton(PointerEventData data)
    {

    }

    private void OnNextSuspectFingerprintButton(PointerEventData data)
    {

    }
    
    private void OnBeforeSWeaponFingerprintButton(PointerEventData data)
    {

    }

    private void OnNextSWeaponFingerprintButton(PointerEventData data)
    {

    }

    private void OnChangeDetectionFingerprintButton(PointerEventData data)
    {

    }
}
