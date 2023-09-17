using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using ScenarioType = Define.ScenarioType;
using SuspectCode = Define.SuspectCode;
using TMPro;

/**
* 플레이어의 게임 진행 상황을 저장할 클래스
* 이 클래스가 JSON으로 저장됨
*/
public class DeductionPrograssData
{
    /* 사건코드, 사건코드가 있으면 모든 게임 진행 상황의 로드할 수 있음 */
    private string caseCode;

    /** 현재 플레이어가 단서들을 발견했는지 여부 **/
    private List<byte> IsFindEvidences = new List<byte>(13) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    /** 현재 플레이어가 진짜 흉기로 의심 중인 무기 **/
    private Weapon suspectedWeapon = null;
    /** 지문을 찾아낸 용의자들을 기록하는 딕셔너리 **/
    private List<SuspectCode> SuspectsFoundFingerprints = new List<SuspectCode>();
    /** 각 흉기 후보들 간의 지문 검출 현황을 저장해놓는 딕셔너리 **/
    private Dictionary<Weapon, Dictionary<SuspectCode, bool>> IsDetectiveFingerprintFromWeapons = new Dictionary<Weapon, Dictionary<SuspectCode, bool>>();
    /** 각 용의자들의 범행 동기 추정 현황을 저장해놓는 딕셔너리 **/
    private Dictionary<SuspectCode, string> suspectedMotivation = new Dictionary<SuspectCode, string>();

    private static Dictionary<string, Color> colorCode= new Dictionary<string, Color> {
            { "Red", new Color(1f, 0.117f, 0f, 1f) },
            { "Green", new Color(0.117f, 1f, 0f, 1f) },
            { "Yellow", new Color(1f, 1f, 0.117f, 1f) },
             { "Grey", new Color(0.5f, 0.5f, 0.5f, 1f) }
        };

    public void InitDeductotionData(CrimeCaseData _crimeCaseData)
    {
        caseCode = _crimeCaseData.GetCaseCode();

        /** 지문 검출 현황 저장 딕셔너리 초기화 **/
        List<SuspectInfo> suspects = _crimeCaseData.GetSuspects();
        List<Weapon> weapons = _crimeCaseData.GetWeapons();
        foreach (Weapon eachWepon in weapons)
        {
            Dictionary<SuspectCode, bool> temp = new Dictionary<SuspectCode, bool>();
            
            foreach (SuspectInfo eachSuspect in suspects)
            {
                temp.Add(eachSuspect.GetSuspectCode(), false);
            }

            IsDetectiveFingerprintFromWeapons.Add(eachWepon, temp);
        }
    }

    public void FindNewEvidence(Evidence newEvidence)
    {
        List<Evidence> evidences = GameManager.Ingame.CaseData.GetEvidences();
 
        for (int i = 0; i < 13; i++)
        {
            if (evidences[i] == newEvidence)
            {
                IsFindEvidences[i] = 255;
                break;
            }
        }

        if (newEvidence is MotivationExplainProp)
        {
            MotivationExplainProp newMotivationProp = newEvidence as MotivationExplainProp;
            SuspectCode explainSuspect = ((MotivationExplainProp)newEvidence).GetExplainingSuspect();
            string explainMotivation = ((MotivationExplainProp)newEvidence).GetMotivation();

            if (!suspectedMotivation.ContainsKey(explainSuspect))
            {
                suspectedMotivation.Add(explainSuspect, explainMotivation);
            }
        }
        else if (newEvidence is FingerprintMemory)
        {
            FingerprintMemory newFingeprintMemory = newEvidence as FingerprintMemory;
            SuspectCode masterSuspect = newFingeprintMemory.GetSuspectCode();

            if (!SuspectsFoundFingerprints.Contains(masterSuspect))
            {
                SuspectsFoundFingerprints.Add(masterSuspect);
            }
        }
    }

    public void SetSuitablityForDeduction(TextMeshProUGUI targetText, SuspectCode suspect)
    {
        if (suspectedWeapon == null)
        {
            targetText.text = "흉기 미지정";
            targetText.color = colorCode["Grey"];
            return;
        }
        
        string answer = suspectedWeapon.GetSuitabilityForSuspect(suspect);
        targetText.text = answer;
        switch (answer)
        {
            case Weapon.strongSuitability :
                targetText.color = colorCode["Red"];
                break;
            case Weapon.weakSuitability :
                targetText.color = colorCode["Yellow"];
                break;
        }

        return;
    }

    public void SetDetectionFingerprintForDeduction(TextMeshProUGUI targetText, SuspectCode suspect)
    {
        if (suspectedWeapon == null)
        {
            targetText.text = "흉기 미지정";
            targetText.color = colorCode["Grey"];
            return;
        }

        bool isDetectiveFingerprint = IsDetectiveFingerprintFromWeapons[suspectedWeapon][suspect];
        switch (isDetectiveFingerprint)
        {
            case true:
                targetText.text = "지문 검출됨";
                targetText.color = colorCode["Red"];
                break;
            case false:
                targetText.text = "검출되지 않음";
                targetText.color = colorCode["Green"];
                break;
        }

        return;
    }

    public void SetMotivationForDeduction(TextMeshProUGUI targetText, SuspectCode suspect)
    {
        if (!suspectedMotivation.ContainsKey(suspect))
        {
            targetText.text = "범행 동기 불명";
            targetText.color = colorCode["Grey"];
            return;
        }

        string answer = suspectedMotivation[suspect];
        targetText.text = answer;
        switch (answer)
        {
            case PropLoader.strongCrimeMotivation :
                targetText.color = colorCode["Red"];
                break;
            case PropLoader.weakCrimeMotivation :
                targetText.color = colorCode["Yellow"];
                break;
            case PropLoader.dispelledCrimeMotivation :
                targetText.color = colorCode["Green"];
                break;
        }
    }

    public void SetSuspectedWeapon(Weapon inputWeapon)
    {
        suspectedWeapon = inputWeapon;
    }

    public Weapon GetSuspectedWeapon()
    {
        return suspectedWeapon;
    }

    public bool CheckFindEvidences(int index)
    {
        if (IsFindEvidences[index] == 255)
            return true;
        return false;
    }

    public bool CheckDetectionSuspectsFingerprint(SuspectCode suspect)
    {
        return SuspectsFoundFingerprints.Contains(suspect);
    }

    public void ChangeIsDetectiveFingerprintFromWeapons(SuspectCode suspect)
    {
        bool temp = IsDetectiveFingerprintFromWeapons[suspectedWeapon][suspect];
        IsDetectiveFingerprintFromWeapons[suspectedWeapon][suspect] = !temp;
    }
}