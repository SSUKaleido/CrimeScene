using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using SuspectCode = Define.SuspectCode;

public class UI_MainScene_PopupGameEndMenu : UI_Popup
{
    enum Images
    {
    }

    enum Buttons
    {
        ReturnStartMeneyButton
    }

    enum Texts
    {
        GuideText2,
        GuideText3,
        PlayerSuspect1NameText,
        PlayerSuspect2NameText,
        PlayerSuspect3NameText,
        PlayerPointWeaponText,
        PlayerSuspect1DetectiveFingerprintText,
        PlayerSuspect2DetectiveFingerprintText,
        PlayerSuspect3DetectiveFingerprintText,
        PlayerSuspect1MotivationText,
        PlayerSuspect2MotivationText,
        PlayerSuspect3MotivationText,
        GuideText4,
        AnswerSuspect1NameText,
        AnswerSuspect2NameText,
        AnswerSuspect3NameText,
        AnswerPointWeaponText,
        AnswerSuspect1DetectiveFingerprintText,
        AnswerSuspect2DetectiveFingerprintText,
        AnswerSuspect3DetectiveFingerprintText,
        AnswerSuspect1MotivationText,
        AnswerSuspect2MotivationText,
        AnswerSuspect3MotivationText,
        CheckText1,
        CheckText2,
        CheckText3,
        CheckText4,
        CheckText5,
        CheckText6,
        CheckText7,
        CheckPercentText
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

        GetButton((int)Buttons.ReturnStartMeneyButton).gameObject.BindEvent(OnReturnStartMeneyButton);

        InitTexts();
    }

    private void OnReturnStartMeneyButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        GameManager.Scene.LoadScene(Define.Scene.StartScene);
    }

    private void InitTexts()
    {
        SuspectInfo pointedSuspect = GameManager.Ingame.CaseData.GetFinalPointedSuspect();

        /** 추리가 맞았는 지 틀렸는 지 **/
        if (pointedSuspect == GameManager.Ingame.CaseData.GetRealCriminal())
        {
            GetText((int)Texts.GuideText2).text = "<color=#1EFF00>맞았습니다!</color>";
        }
        else
        {
            GetText((int)Texts.GuideText2).text = "<color=#FF1E00>틀렸습니다</color>";
        }


        DeductionPrograssData presentPrograssData = GameManager.Ingame.PrograssData;
        List<SuspectInfo> suspects = GameManager.Ingame.CaseData.GetSuspects();
        Weapon pointedWeapon = GameManager.Ingame.PrograssData.GetSuspectedWeapon();
        Dictionary<SuspectCode, string> suspectedMotivation = presentPrograssData.GetSuspectedMotivation();
        {
            GetText((int)Texts.GuideText3).text = $"당신의 추리: 용의자 {pointedSuspect.GetName()}";
            GetText((int)Texts.PlayerSuspect1NameText).text = suspects[0].GetName();
            GetText((int)Texts.PlayerSuspect2NameText).text = suspects[1].GetName();
            GetText((int)Texts.PlayerSuspect3NameText).text = suspects[2].GetName();
            GetText((int)Texts.PlayerPointWeaponText).text = $"<color=#FFFFE1>{pointedWeapon.GetName()}</color>";
            presentPrograssData.SetDetectionFingerprintForDeduction(GetText((int)Texts.PlayerSuspect1DetectiveFingerprintText), suspects[0].GetSuspectCode());
            presentPrograssData.SetDetectionFingerprintForDeduction(GetText((int)Texts.PlayerSuspect2DetectiveFingerprintText), suspects[1].GetSuspectCode());
            presentPrograssData.SetDetectionFingerprintForDeduction(GetText((int)Texts.PlayerSuspect3DetectiveFingerprintText), suspects[2].GetSuspectCode());
            presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.PlayerSuspect1MotivationText), suspects[0].GetSuspectCode());
            presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.PlayerSuspect2MotivationText), suspects[1].GetSuspectCode());
            presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.PlayerSuspect3MotivationText), suspects[2].GetSuspectCode());
        }

        Weapon realWeapon = GameManager.Ingame.CaseData.GetRealWeapon();
        List<string> laidFingerprints = realWeapon.GetLaidFingerprints();
        {
            GetText((int)Texts.GuideText4).text = $"사건의 진상: 범인 {GameManager.Ingame.CaseData.GetRealCriminal().GetName()}";
            GetText((int)Texts.AnswerSuspect1NameText).text = suspects[0].GetName();
            GetText((int)Texts.AnswerSuspect2NameText).text = suspects[1].GetName();
            GetText((int)Texts.AnswerSuspect3NameText).text = suspects[2].GetName();
            GetText((int)Texts.AnswerPointWeaponText).text = $"<color=#FFFFE1>{realWeapon.GetName()}</color>";

            if (laidFingerprints.Contains(suspects[0].GetFingerprintType()))
            {
                GetText((int)Texts.AnswerSuspect1DetectiveFingerprintText).text = $"<color=#FF1E00>지문 검출됨</color>";
            }
            else
            {
                GetText((int)Texts.AnswerSuspect1DetectiveFingerprintText).text = $"<color=#1EFF00>검출되지 않음</color>";
            }
            if (laidFingerprints.Contains(suspects[1].GetFingerprintType()))
            {
                GetText((int)Texts.AnswerSuspect2DetectiveFingerprintText).text = $"<color=#FF1E00>지문 검출됨</color>";
            }
            else
            {
                GetText((int)Texts.AnswerSuspect2DetectiveFingerprintText).text = $"<color=#1EFF00>검출되지 않음</color>";
            }
            if (laidFingerprints.Contains(suspects[2].GetFingerprintType()))
            {
                GetText((int)Texts.AnswerSuspect3DetectiveFingerprintText).text = $"<color=#FF1E00>지문 검출됨</color>";
            }
            else
            {
                GetText((int)Texts.AnswerSuspect3DetectiveFingerprintText).text = $"<color=#1EFF00>검출되지 않음</color>";
            }

            List<MotivationExplainProp> motivationExplainProps = GameManager.Ingame.CaseData.GetMotivationExplainProps();
            foreach (MotivationExplainProp eachProp in motivationExplainProps)
            {
                GameManager.Ingame.PrograssData.FindNewEvidence(eachProp as Evidence);
            }
            presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.AnswerSuspect1MotivationText), suspects[0].GetSuspectCode());
            presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.AnswerSuspect2MotivationText), suspects[1].GetSuspectCode());
            presentPrograssData.SetMotivationForDeduction(GetText((int)Texts.AnswerSuspect3MotivationText), suspects[2].GetSuspectCode());
        }

        int sucess = 0;
        if (pointedWeapon == realWeapon)
        {
            sucess++;
            GetText((int)Texts.CheckText1).text = "<color=#1EFF00>성공</color>";
        }
        else
        {
            GetText((int)Texts.CheckText1).text = "<color=#FF1E00>실패</color>";
        }

        if (presentPrograssData.IsDetectionFingerprintCorrect(suspects[0]))
        {
            sucess++;
            GetText((int)Texts.CheckText2).text = "<color=#1EFF00>성공</color>";
        }
        else
        {
            GetText((int)Texts.CheckText2).text = "<color=#FF1E00>실패</color>";
        }
        if (presentPrograssData.IsDetectionFingerprintCorrect(suspects[1]))
        {
            sucess++;
            GetText((int)Texts.CheckText3).text = "<color=#1EFF00>성공</color>";
        }
        else
        {
            GetText((int)Texts.CheckText3).text = "<color=#FF1E00>실패</color>";
        }
        if (presentPrograssData.IsDetectionFingerprintCorrect(suspects[2]))
        {
            sucess++;
            GetText((int)Texts.CheckText4).text = "<color=#1EFF00>성공</color>";
        }
        else
        {
            GetText((int)Texts.CheckText4).text = "<color=#FF1E00>실패</color>";
        }

        if (suspectedMotivation[suspects[0].GetSuspectCode()] == presentPrograssData.CheckSuspectedMotivation(suspects[0].GetSuspectCode()))
        {
            sucess++;
            GetText((int)Texts.CheckText5).text = "<color=#1EFF00>성공</color>";
        }
        else
        {
            GetText((int)Texts.CheckText5).text = "<color=#FF1E00>실패</color>";
        }

        if (suspectedMotivation[suspects[1].GetSuspectCode()] == presentPrograssData.CheckSuspectedMotivation(suspects[1].GetSuspectCode()))
        {
            sucess++;
            GetText((int)Texts.CheckText6).text = "<color=#1EFF00>성공</color>";
        }
        else
        {
            GetText((int)Texts.CheckText6).text = "<color=#FF1E00>실패</color>";
        }

        if (suspectedMotivation[suspects[2].GetSuspectCode()] == presentPrograssData.CheckSuspectedMotivation(suspects[2].GetSuspectCode()))
        {
            sucess++;
            GetText((int)Texts.CheckText7).text = "<color=#1EFF00>성공</color>";
        }
        else
        {
            GetText((int)Texts.CheckText7).text = "<color=#FF1E00>실패</color>";
        }

        int percent = (sucess * 100) / 7;
        GetText((int)Texts.CheckPercentText).text = $"{percent}%";
    }
}
