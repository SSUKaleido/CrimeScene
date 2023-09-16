using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class UI_MainScene_PopupCaseMenu : UI_Popup
{
    GameObject caseLayout = null;
    GameObject victimLayout = null;

    enum Images
    {
    }

    enum Buttons
    {
        CaseInfoButton,
        VictimInfoButton
    }

    enum Texts
    {
        CaseCodeText,
        CaseNameText,
        CaseExplainText1,
        CaseExplainText2,
        CaseExplainText3,
        VictimNameText,
        VictimAgeText,
        VictimGenderText,
        VictimJopText,
        VictimCauseOfDeathText,
        TestimonyText1,
        TestimonyText2,
        TestimonyText3,
        TestimonyText4,
        TestimonyText5
    }

    enum GameObjects
    {
        CaseLayout,
        VictimLayout
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

        caseLayout = GetObject((int)GameObjects.CaseLayout);
        victimLayout = GetObject((int)GameObjects.VictimLayout);

        GetButton((int)Buttons.CaseInfoButton).gameObject.BindEvent(OnCaseInfoButton);
        GetButton((int)Buttons.VictimInfoButton).gameObject.BindEvent(OnVictimInfoButton);

        victimLayout.SetActive(false);
        caseLayout.SetActive(true);
        
        InitTexts();
	}

    private void OnCaseInfoButton(PointerEventData data)
    {
        victimLayout.SetActive(false);
        caseLayout.SetActive(true);
    }

    private void OnVictimInfoButton(PointerEventData data)
    {
        caseLayout.SetActive(false);
        victimLayout.SetActive(true);
    }

    private void InitTexts()
    {
        GetText((int)Texts.CaseCodeText).text = "사건 코드: ()";
        GetText((int)Texts.CaseNameText).text = "사건명: ()";
        GetText((int)Texts.CaseExplainText1).text = "사건 설명()";
        GetText((int)Texts.CaseExplainText2).text = "사건 설명()";
        GetText((int)Texts.CaseExplainText3).text = "사건 설명()";
        GetText((int)Texts.VictimNameText).text = "이름: ()";
        GetText((int)Texts.VictimAgeText).text = "나이: ()";
        GetText((int)Texts.VictimGenderText).text = "성별: ()";
        GetText((int)Texts.VictimJopText).text = "직업: ()";
        GetText((int)Texts.VictimCauseOfDeathText).text = "사인: ()";
        GetText((int)Texts.TestimonyText1).text = "증언()";
    }
}
