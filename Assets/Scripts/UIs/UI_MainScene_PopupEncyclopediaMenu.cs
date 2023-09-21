using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_MainScene_PopupEncyclopediaMenu : UI_MainSceneMenu
{
    GameObject investigationTutorialScrollView = null;
    GameObject weaponTutorialScrollView = null;
    GameObject fingerprintTutorialScrollView = null;
    GameObject motivationTutorialScrollView = null;
    GameObject currentLayout = null;

    enum Images
    {
    }

    enum Buttons
    {
        InvestigationTutorialButton,
        WeaponTutorialButton,
        FingerprintTutorialButton,
        MotivationTutorialButton
    }

    enum Texts
    {
    }

    enum GameObjects
    {
        InvestigationTutorialScrollView,
        WeaponTutorialScrollView,
        FingerprintTutorialScrollView,
        MotivationTutorialScrollView
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

        GetButton((int)Buttons.InvestigationTutorialButton).gameObject.BindEvent(OnInvestigationTutorialButton);
        GetButton((int)Buttons.WeaponTutorialButton).gameObject.BindEvent(OnWeaponTutorialButton);
        GetButton((int)Buttons.FingerprintTutorialButton).gameObject.BindEvent(OnFingerprintTutorialButton);
        GetButton((int)Buttons.MotivationTutorialButton).gameObject.BindEvent(OnMotivationTutorialButton);

        InitLayouts();
    }

    private void OnInvestigationTutorialButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        if (currentLayout == investigationTutorialScrollView)
            return;
        
        changeLayout(investigationTutorialScrollView);
    }

    private void OnWeaponTutorialButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        if (currentLayout == weaponTutorialScrollView)
            return;
        
        changeLayout(weaponTutorialScrollView);
    }

    private void OnFingerprintTutorialButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        if (currentLayout == fingerprintTutorialScrollView)
            return;

        changeLayout(fingerprintTutorialScrollView);
    }

    private void OnMotivationTutorialButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        if (currentLayout == motivationTutorialScrollView)
            return;
        
        changeLayout(motivationTutorialScrollView);
    }

    private void InitLayouts()
    {
        investigationTutorialScrollView = GetObject((int)GameObjects.InvestigationTutorialScrollView);
        weaponTutorialScrollView = GetObject((int)GameObjects.WeaponTutorialScrollView);
        fingerprintTutorialScrollView = GetObject((int)GameObjects.FingerprintTutorialScrollView);
        motivationTutorialScrollView = GetObject((int)GameObjects.MotivationTutorialScrollView);

        currentLayout = investigationTutorialScrollView;
        weaponTutorialScrollView.SetActive(false);
        fingerprintTutorialScrollView.SetActive(false);
        motivationTutorialScrollView.SetActive(false);
        changeLayout(investigationTutorialScrollView);
    }

    private void changeLayout(GameObject newLayout)
    {
        currentLayout.SetActive(false);
        newLayout.SetActive(true);
        currentLayout = newLayout;
    }

    public override void SpawnAnimation(int previewIndex)
    {

    }

    public override int DespawnAnimation(int nextIndex)
    {
        return 3;
    }
}
