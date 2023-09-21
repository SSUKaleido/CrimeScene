using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_MainScene_PopupCompareFingerprintMenu : UI_Popup
{
    Image suspectsFingerprint = null;
    Image weaponsFingerprint = null;
    TextMeshProUGUI suspectFingerprintExplainText = null;
    TextMeshProUGUI weaponFingerprintExplainText = null;
    TextMeshProUGUI detectionFingerprintText = null;

    int weaponFingerprintIndex = 0;
    int suspectFingerprintIndex = 0;

    enum Images
    {
        SuspectsFingerprint,
        WeaponsFingerprint
    }

    enum Buttons
    {
        BeforeSuspectFingerprintButton,
        NextSuspectFingerprintButton,
        BeforeWeaponFingerprintButton,
        NextWeaponFingerprintButton,
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
        PopupMenu
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

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
        GetButton((int)Buttons.BeforeSuspectFingerprintButton).gameObject.BindEvent(OnBeforeSuspectFingerprintButton);
        GetButton((int)Buttons.NextSuspectFingerprintButton).gameObject.BindEvent(OnNextSuspectFingerprintButton);
        GetButton((int)Buttons.BeforeWeaponFingerprintButton).gameObject.BindEvent(OnBeforeSWeaponFingerprintButton);
        GetButton((int)Buttons.NextWeaponFingerprintButton).gameObject.BindEvent(OnNextSWeaponFingerprintButton);
        GetButton((int)Buttons.ChangeDetectionFingerprintButton).gameObject.BindEvent(OnChangeDetectionFingerprintButton);

        suspectsFingerprint = GetImage((int)Images.SuspectsFingerprint);
        weaponsFingerprint = GetImage((int)Images.WeaponsFingerprint);
        suspectFingerprintExplainText = GetText((int)Texts.SuspectFingerprintExplainText);
        weaponFingerprintExplainText = GetText((int)Texts.WeaponFingerprintExplainText);
        detectionFingerprintText = GetText((int)Texts.DetectionFingerprintText);

        RefeshMenu();

        GetObject((int)GameObjects.PopupMenu).GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 0f), 0.5f, true);
	}

    /** 두 지문 이미지, 지문 설명, 지문 검출 여부도 재설정 **/
    private void RefeshMenu()
    {
        List<SuspectInfo> suspects = GameManager.Ingame.CaseData.GetSuspects();
        Sprite[] fingerPrintSprites = GameManager.Resource.LoadAll<Sprite>("Sprites/Fingerprint_Sprite");
        Sprite[] GUI2 = GameManager.Resource.LoadAll<Sprite>("Sprites/GUI2");

        /** 용의자 지문 불러오기 **/
        SuspectInfo currentSuspect = suspects[suspectFingerprintIndex];
        if (GameManager.Ingame.PrograssData.CheckDetectionSuspectsFingerprint(currentSuspect.GetSuspectCode()))
        {
            int spriteCode = currentSuspect.GetFingerprintSpriteCode();
            suspectsFingerprint.sprite = fingerPrintSprites[spriteCode];
        }
        else
        {
            suspectsFingerprint.sprite = GUI2[4];
        }

        /** 무기 지문 불러오기 **/
        List<int> currentWeaponFingerprintSpriteCode = GameManager.Ingame.PrograssData.GetSuspectedWeapon().GetLaidFingerprintSpriteCodes();
        weaponsFingerprint.sprite = fingerPrintSprites[currentWeaponFingerprintSpriteCode[weaponFingerprintIndex]];

        /** 용의자 지문 설명 불러오기 **/
        suspectFingerprintExplainText.text = $"용의자 {currentSuspect.GetName()}";

        /** 무기 지문 설명 불러오기 **/
        weaponFingerprintExplainText.text = $"{weaponFingerprintIndex + 1}번째 지문";

        /** 지문 검출 여부 텍스트 불러오기 **/
        GameManager.Ingame.PrograssData.SetDetectionFingerprintForDeduction(detectionFingerprintText, currentSuspect.GetSuspectCode());
    }

    private void OnBeforeSuspectFingerprintButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        if (suspectFingerprintIndex > 0)
        {
            suspectFingerprintIndex--;
            RefeshMenu();
        }
    }

    private void OnNextSuspectFingerprintButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        if (suspectFingerprintIndex < 2)
        {
            suspectFingerprintIndex++;
            RefeshMenu();
        }
    }
    
    private void OnBeforeSWeaponFingerprintButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        if (weaponFingerprintIndex > 0)
        {
            weaponFingerprintIndex--;
            RefeshMenu();
        }
    }

    private void OnNextSWeaponFingerprintButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        Weapon curWeapon = GameManager.Ingame.PrograssData.GetSuspectedWeapon();
        if (weaponFingerprintIndex < curWeapon.GetLaidFingerprints().Count - 1)
        {
            weaponFingerprintIndex++;
            RefeshMenu();
        }
    }

    private void OnChangeDetectionFingerprintButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        List<SuspectInfo> suspects = GameManager.Ingame.CaseData.GetSuspects();
        SuspectInfo currentSuspect = suspects[suspectFingerprintIndex];

        GameManager.Ingame.PrograssData.ChangeIsDetectiveFingerprintFromWeapons(currentSuspect.GetSuspectCode());
        RefeshMenu();
    }
}
