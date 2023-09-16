using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupDeductionMenu : UI_Popup
{
    enum Images
    {
    }

    enum Buttons
    {
    }

    enum Texts
    {
        Suspect1NameText,
        Suspect2NameText,
        Suspect3NameText,
        Suspect1GenderText,
        Suspect2GenderText,
        Suspect3GenderText,
        Suspect1AgeText,
        Suspect2AgeText,
        Suspect3AgeText,
        Suspect1JopText,
        Suspect2JopText,
        Suspect3JopText,
        Suspect1RelationText,
        Suspect2RelationText,
        Suspect3RelationText,
        Suspect1SuitablityText,
        Suspect2SuitablityText,
        Suspect3SuitablityText,
        Suspect1DetectiveFingerprintText,
        Suspect2DetectiveFingerprintText,
        Suspect3DetectiveFingerprintText,
        Suspect1MotivationText,
        Suspect2MotivationText,
        Suspect3MotivationText
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

        InitTexts();
    }

    private void InitTexts()
    {
        GetText((int)Texts.Suspect1NameText).text = "이름()";
        GetText((int)Texts.Suspect2NameText).text = "이름()";
        GetText((int)Texts.Suspect3NameText).text = "이름()";
        GetText((int)Texts.Suspect1GenderText).text = "성별()";
        GetText((int)Texts.Suspect2GenderText).text = "성별()";
        GetText((int)Texts.Suspect3GenderText).text = "성별()";
        GetText((int)Texts.Suspect1AgeText).text = "나이()";
        GetText((int)Texts.Suspect2AgeText).text = "나이()";
        GetText((int)Texts.Suspect3AgeText).text = "나이()";
        GetText((int)Texts.Suspect1JopText).text = "직업()";
        GetText((int)Texts.Suspect2JopText).text = "직업()";
        GetText((int)Texts.Suspect3JopText).text = "직업()";
        GetText((int)Texts.Suspect1RelationText).text = "관계()";
        GetText((int)Texts.Suspect2RelationText).text = "관계()";
        GetText((int)Texts.Suspect3RelationText).text = "관계()";
        GetText((int)Texts.Suspect1SuitablityText).text = "적합성()";
        GetText((int)Texts.Suspect2SuitablityText).text = "적합성()";
        GetText((int)Texts.Suspect3SuitablityText).text = "적합성()";
        GetText((int)Texts.Suspect1DetectiveFingerprintText).text = "지문()";
        GetText((int)Texts.Suspect2DetectiveFingerprintText).text = "지문()";
        GetText((int)Texts.Suspect3DetectiveFingerprintText).text = "지문()";
        GetText((int)Texts.Suspect1MotivationText).text = "동기()";
        GetText((int)Texts.Suspect2MotivationText).text = "동기()";
        GetText((int)Texts.Suspect3MotivationText).text = "동기()";
    }
}
