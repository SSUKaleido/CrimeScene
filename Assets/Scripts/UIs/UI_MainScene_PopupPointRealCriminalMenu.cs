using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupPointRealCriminalMenu : UI_Popup
{
    enum Images
    {
        Suspect1Cursor,
        Suspect2Cursor,
        Suspect3Cursor
    }

    enum Buttons
    {
        CancleButton,
        ConfirmPointButton
    }

    enum Texts
    {
        Suspect1NameText,
        Suspect2NameText,
        Suspect3NameText,
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
        Suspect3MotivationText,
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

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
        GetButton((int)Buttons.ConfirmPointButton).gameObject.BindEvent(OnConfirmPointButton);

        InitTexts();
    }

    private void OnConfirmPointButton(PointerEventData data)
    {

    }

    private void InitTexts()
    {
        List<SuspectInfo> _suspects = GameManager.Ingame.CaseData.GetSuspects();

        GetText((int)Texts.Suspect1NameText).text = _suspects[0].GetName();
        GetText((int)Texts.Suspect2NameText).text = _suspects[1].GetName();
        GetText((int)Texts.Suspect3NameText).text = _suspects[2].GetName();
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
}
