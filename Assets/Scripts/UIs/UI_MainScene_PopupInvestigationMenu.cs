using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupInvestigationMenu : UI_Popup
{
    private int examineEvidenceIndex = -1;

    private List<GameObject> slotRenderTextures = new List<GameObject>();
    private List<Image> slotImages = new List<Image>();

    private Button pointRealCriminalButton = null;
    private Button compareFingerprintButton = null;
    private GameObject examineEvidence = null;
    private TextMeshProUGUI evidenceNameText = null;
    private TextMeshProUGUI evidenceExplainText = null;
    private TextMeshProUGUI weaponWoundText = null;

    enum Images
    {
        Slot1_Image,
        Slot2_Image,
        Slot3_Image,
        Slot4_Image,
        Slot5_Image,
        Slot6_Image,
        Slot7_Image,
        Slot8_Image,
        Slot9_Image,
        Slot10_Image,
        Slot11_Image,
        Slot12_Image,
        Slot13_Image
    }

    enum Buttons
    {
        PointRealWeaponButton,
        CompareFingerprintButton,
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

    enum Texts
    {
        EvidenceNameText,
        EvidenceExplainText,
        WeaponWoundText
    }

    enum GameObjects
    {
        ExamineEvidence,
        Slot1_RenderTexture,
        Slot2_RenderTexture,
        Slot3_RenderTexture,
        Slot4_RenderTexture,
        Slot5_RenderTexture,
        Slot6_RenderTexture,
        Slot7_RenderTexture,
        Slot8_RenderTexture,
        Slot9_RenderTexture,
        Slot10_RenderTexture,
        Slot11_RenderTexture,
        Slot12_RenderTexture,
        Slot13_RenderTexture
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

        pointRealCriminalButton = GetButton((int)Buttons.PointRealWeaponButton);
        compareFingerprintButton = GetButton((int)Buttons.CompareFingerprintButton);
        examineEvidence = GetObject((int)GameObjects.ExamineEvidence);
        evidenceNameText = GetText((int)Texts.EvidenceNameText);
        evidenceExplainText = GetText((int)Texts.EvidenceExplainText);
        weaponWoundText = GetText((int)Texts.WeaponWoundText);

        LoadUIElements();
        LoadScrollView();
        RefreshMenu();
    }

    /** 상단 패널 3d 오브젝트, 단서 설명 문구들, 버튼 보이는지 여부 재확인 **/
    private void RefreshMenu()
    {
        if (examineEvidenceIndex == -1)
        {
            evidenceNameText.text = "";
            evidenceExplainText.text = "";
            weaponWoundText.text = "";
            pointRealCriminalButton.gameObject.SetActive(false);
            compareFingerprintButton.gameObject.SetActive(false);
            examineEvidence.SetActive(false);
            return;
        }

        Evidence temp = GameManager.Ingame.CaseData.GetEvidences()[examineEvidenceIndex];
        evidenceNameText.text = temp.GetName();
        evidenceExplainText.text = temp.GetflavorText();
        if (temp is Weapon)
        {
            evidenceNameText.text = temp.GetName() + " <size=28><color=#FF1E00>(흉기 후보)</color></size>";
            weaponWoundText.text = $"이 물건으로 입힐 수 있는 상해 유형은 {((Weapon)temp).GetWoundType()}입니다.";
        }
        else
        {
            evidenceNameText.text = temp.GetName();
            weaponWoundText.text = "";
        }

        examineEvidence.SetActive(true);
        changeExamineEvidence(examineEvidenceIndex);

        if (temp is Weapon)
        {
            pointRealCriminalButton.gameObject.SetActive(true);
        }
        else
        {
            pointRealCriminalButton.gameObject.SetActive(false);
        }

        if (temp is Weapon && (Weapon)temp == GameManager.Ingame.PrograssData.GetSuspectedWeapon())
        {
            compareFingerprintButton.gameObject.SetActive(true);
        }
        else
        {
            compareFingerprintButton.gameObject.SetActive(false);
        }
    }

    /** 스크롤뷰 쫘악 훑어봐서 단서 발견했으면 해당하는 칸 활성화 **/
    private void LoadScrollView()
    {
        for (int i = 0; i < 13; i++)
        {
            /** 어떤 단서를 이미 찾았을 때 **/
            if (GameManager.Ingame.PrograssData.CheckFindEvidences(i))
            {
                slotImages[i].gameObject.SetActive(false);
                slotRenderTextures[i].gameObject.SetActive(true);
            }
            else
            {
                slotImages[i].gameObject.SetActive(true);
                slotRenderTextures[i].gameObject.SetActive(false);
            }   
        }
    }

    /** 리스트 등에 UI 엘리먼트들을 적재 **/
    private void LoadUIElements()
    {
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot1_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot2_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot3_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot4_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot5_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot6_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot7_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot8_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot9_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot10_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot11_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot12_RenderTexture));
        slotRenderTextures.Add(GetObject((int)GameObjects.Slot13_RenderTexture));

        slotImages.Add(GetImage((int)Images.Slot1_Image));
        slotImages.Add(GetImage((int)Images.Slot2_Image));
        slotImages.Add(GetImage((int)Images.Slot3_Image));
        slotImages.Add(GetImage((int)Images.Slot4_Image));
        slotImages.Add(GetImage((int)Images.Slot5_Image));
        slotImages.Add(GetImage((int)Images.Slot6_Image));
        slotImages.Add(GetImage((int)Images.Slot7_Image));
        slotImages.Add(GetImage((int)Images.Slot8_Image));
        slotImages.Add(GetImage((int)Images.Slot9_Image));
        slotImages.Add(GetImage((int)Images.Slot10_Image));
        slotImages.Add(GetImage((int)Images.Slot11_Image));
        slotImages.Add(GetImage((int)Images.Slot12_Image));
        slotImages.Add(GetImage((int)Images.Slot13_Image));

        GetButton((int)Buttons.Slot1).gameObject.BindEvent(OnSlot1);
        GetButton((int)Buttons.Slot2).gameObject.BindEvent(OnSlot2);
        GetButton((int)Buttons.Slot3).gameObject.BindEvent(OnSlot3);
        GetButton((int)Buttons.Slot4).gameObject.BindEvent(OnSlot4);
        GetButton((int)Buttons.Slot5).gameObject.BindEvent(OnSlot5);
        GetButton((int)Buttons.Slot6).gameObject.BindEvent(OnSlot6);
        GetButton((int)Buttons.Slot7).gameObject.BindEvent(OnSlot7);
        GetButton((int)Buttons.Slot8).gameObject.BindEvent(OnSlot8);
        GetButton((int)Buttons.Slot9).gameObject.BindEvent(OnSlot9);
        GetButton((int)Buttons.Slot10).gameObject.BindEvent(OnSlot10);
        GetButton((int)Buttons.Slot11).gameObject.BindEvent(OnSlot11);
        GetButton((int)Buttons.Slot12).gameObject.BindEvent(OnSlot12);
        GetButton((int)Buttons.Slot13).gameObject.BindEvent(OnSlot13);
        GetButton((int)Buttons.PointRealWeaponButton).gameObject.BindEvent(OnPointRealWeaponButton);
        GetButton((int)Buttons.CompareFingerprintButton).gameObject.BindEvent(OnCompareFingerprintButton);
    }

    private void OnPointRealWeaponButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        Evidence suspectedWeapon = GameManager.Ingame.CaseData.GetEvidences()[examineEvidenceIndex];
        GameManager.Ingame.PrograssData.SetSuspectedWeapon(suspectedWeapon as Weapon);
        RefreshMenu();
    }

    private void OnCompareFingerprintButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupCompareFingerprintMenu>("MainScene_PopupCompareFingerprintMenu");
    }

    private void OnSlot1(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(0))
        {
            examineEvidenceIndex = 0;
            RefreshMenu();
        }
    }

    private void OnSlot2(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(1))
        {
            examineEvidenceIndex = 1;
            RefreshMenu();
        }
    }

    private void OnSlot3(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(2))
        {
            examineEvidenceIndex = 2;
            RefreshMenu();
        }
    }

    private void OnSlot4(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(3))
        {
            examineEvidenceIndex = 3;
            RefreshMenu();
        }
    }

    private void OnSlot5(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(4))
        {
            examineEvidenceIndex = 4;
            RefreshMenu();
        }
    }

    private void OnSlot6(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(5))
        {
            examineEvidenceIndex = 5;
            RefreshMenu();
        }
    }

    private void OnSlot7(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(6))
        {
            examineEvidenceIndex = 6;
            RefreshMenu();
        }
    }

    private void OnSlot8(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(7))
        {
            examineEvidenceIndex = 7;
            RefreshMenu();
        }
    }

    private void OnSlot9(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(8))
        {
            examineEvidenceIndex = 8;
            RefreshMenu();
        }
    }

    private void OnSlot10(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(9))
        {
            examineEvidenceIndex = 9;
            RefreshMenu();
        }
    }

    private void OnSlot11(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(10))
        {
            examineEvidenceIndex = 10;
            RefreshMenu();
        }
    }

    private void OnSlot12(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(11))
        {
            examineEvidenceIndex = 11;
            RefreshMenu();
        }
    }

    private void OnSlot13(PointerEventData data)
    {
        if (GameManager.Ingame.PrograssData.CheckFindEvidences(12))
        {
            examineEvidenceIndex = 12;
            RefreshMenu();
        }
    }

    private void changeExamineEvidence(int index)
    {
        string path = $"RenderTexture/Evidence{index + 1}RenderTexture";          
        examineEvidence.GetComponent<RawImage>().texture = GameManager.Resource.Load<RenderTexture>(path);
    }
}