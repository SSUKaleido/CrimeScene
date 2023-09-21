using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_MainScene_PopupDeductionMenu : UI_MainSceneMenu
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
        List<SuspectInfo> _suspects = GameManager.Ingame.CaseData.GetSuspects();
        string[] suspectGenders = { _suspects[0].GetGender() == "Male" ? "남성" : "여성",
        _suspects[1].GetGender() == "Male" ? "남성" : "여성",
        _suspects[2].GetGender() == "Male" ? "남성" : "여성" };

        GetText((int)Texts.Suspect1NameText).text = _suspects[0].GetName();
        GetText((int)Texts.Suspect2NameText).text = _suspects[1].GetName();
        GetText((int)Texts.Suspect3NameText).text = _suspects[2].GetName();
        GetText((int)Texts.Suspect1GenderText).text = suspectGenders[0];
        GetText((int)Texts.Suspect2GenderText).text = suspectGenders[1];
        GetText((int)Texts.Suspect3GenderText).text = suspectGenders[2];
        GetText((int)Texts.Suspect1AgeText).text = _suspects[0].GetAge().ToString();
        GetText((int)Texts.Suspect2AgeText).text = _suspects[1].GetAge().ToString();
        GetText((int)Texts.Suspect3AgeText).text = _suspects[2].GetAge().ToString();
        GetText((int)Texts.Suspect1JopText).text = _suspects[0].GetJop();
        GetText((int)Texts.Suspect2JopText).text = _suspects[1].GetJop();
        GetText((int)Texts.Suspect3JopText).text = _suspects[2].GetJop();
        GetText((int)Texts.Suspect1RelationText).text = _suspects[0].GetRelationWithVictim();
        GetText((int)Texts.Suspect2RelationText).text = _suspects[1].GetRelationWithVictim();
        GetText((int)Texts.Suspect3RelationText).text = _suspects[2].GetRelationWithVictim();

        DeductionPrograssData presentPrograssData = GameManager.Ingame.PrograssData;
        presentPrograssData.SetSuitablityForDeduction(GetText((int)Texts.Suspect1SuitablityText), _suspects[0].GetSuspectCode());
        presentPrograssData.SetSuitablityForDeduction(GetText((int)Texts.Suspect2SuitablityText), _suspects[1].GetSuspectCode());
        presentPrograssData.SetSuitablityForDeduction(GetText((int)Texts.Suspect3SuitablityText), _suspects[2].GetSuspectCode());
        presentPrograssData.SetDetectionFingerprintForDeduction(GetText((int)Texts.Suspect1DetectiveFingerprintText), _suspects[0].GetSuspectCode());
        presentPrograssData.SetDetectionFingerprintForDeduction(GetText((int)Texts.Suspect2DetectiveFingerprintText), _suspects[1].GetSuspectCode());
        presentPrograssData.SetDetectionFingerprintForDeduction(GetText((int)Texts.Suspect3DetectiveFingerprintText), _suspects[2].GetSuspectCode());
        presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.Suspect1MotivationText), _suspects[0].GetSuspectCode());
        presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.Suspect2MotivationText), _suspects[1].GetSuspectCode());
        presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.Suspect3MotivationText), _suspects[2].GetSuspectCode());
    }

    public override void SpawnAnimation(int previewIndex)
    {

    }

    public override int DespawnAnimation(int nextIndex)
    {
        return 2;
    }
}
