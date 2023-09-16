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

    /** 인 게임에서 발견한 단서들의 프리펩을 저장해놓는 릭셔너리 **/
    private Dictionary<string, GameObject> EvidencePrefabs = new Dictionary<string, GameObject>();

    /** 현재 플레이어가 진짜 흉기로 의심 중인 무기 **/
    private Weapon suspectedWeapon = null;

    /** 각 흉기 후보들 간의 지문 검출 현황을 저장해놓는 딕셔너리 **/
    private Dictionary<Weapon, Dictionary<SuspectCode, bool>> IsDetectiveFingerprintFromWeapons = new Dictionary<Weapon, Dictionary<SuspectCode, bool>>();

    /** 각 용의자들의 범행 동기 추정 현황을 저장해놓는 딕셔너리 **/
    private Dictionary<SuspectCode, string> suspectedMotivation = new Dictionary<SuspectCode, string>();

    private static Dictionary<string, Color> colorCode= new Dictionary<string, Color> {
            { "Red", new Color(1f, 0.117f, 0f, 1f) },
            { "Green", new Color(0.117f, 1f, 0f, 1f) },
            { "Yellow", new Color(1f, 1f, 0.117f, 1f) },
             { "Grey", new Color(0.5f, 0.5f, 0.5f, 1f) },
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
}