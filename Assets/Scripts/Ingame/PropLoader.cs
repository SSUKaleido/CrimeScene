using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using SuspectCode = Define.SuspectCode;
using ScenarioType = Define.ScenarioType;

public class PropLoader
{
    Dictionary<ScenarioType, Dictionary<SuspectCode, (string, string)>> _motivationExplainPropsPerScenario;
    Dictionary<ScenarioType, string> _motivationDispellingPropPerScenario;
    Dictionary<ScenarioType, List<string>> _propsPerScenario;

    const string strongCrimeMotivation = "강한 범행 동기";
    const string weakCrimeMotivation = "약한 범행 동기";
    const string dispelledCrimeMotivation = "범행 의혹 불식";

    public PropLoader()
    {
        _motivationExplainPropsPerScenario = new Dictionary<ScenarioType, Dictionary<SuspectCode, (string, string)>> {
            { ScenarioType.ConsertMurderCrime, new Dictionary<SuspectCode, (string, string)> {
                { SuspectCode.Suspect1, ("부서진 음반", strongCrimeMotivation) },
                { SuspectCode.Suspect2, ("구겨진 악보", strongCrimeMotivation) },
                { SuspectCode.Suspect3, ("반쯤 빈 향수병", strongCrimeMotivation) },
                { SuspectCode.Suspect4, ("다시 포장한 선물상자", weakCrimeMotivation) }
            } },
            { ScenarioType.HospitalMurderCrime, new Dictionary<SuspectCode, (string, string)> {
                { SuspectCode.Suspect1, ("선물 받은 안경집", weakCrimeMotivation) },
                { SuspectCode.Suspect2, ("미발표 논문", strongCrimeMotivation) },
                { SuspectCode.Suspect3, ("간호일지 수첩", weakCrimeMotivation) },
                { SuspectCode.Suspect4, ("연구비로 결제한 영수증", strongCrimeMotivation) },
                { SuspectCode.Suspect5, ("낡은 고무장갑", weakCrimeMotivation) }
            } },
            { ScenarioType.HighSchoolMurderCrime, new Dictionary<SuspectCode, (string, string)> {
                { SuspectCode.Suspect1, ("경구 투여 항불안제", strongCrimeMotivation) },
                { SuspectCode.Suspect2, ("수상한 커플 반지", strongCrimeMotivation) },
                { SuspectCode.Suspect3, ("파산한 법인 통장", strongCrimeMotivation) },
                { SuspectCode.Suspect4, ("공증 없는 차용증", weakCrimeMotivation) }
            } },
            { ScenarioType.WeddingMurderCrime, new Dictionary<SuspectCode, (string, string)> {
                { SuspectCode.Suspect1, ("채무조회 서류", strongCrimeMotivation) },
                { SuspectCode.Suspect2, ("오래된 일기장", weakCrimeMotivation) },
                { SuspectCode.Suspect3, ("저주 부적", strongCrimeMotivation) },
                { SuspectCode.Suspect4, ("오래된 커플 반지", weakCrimeMotivation) }
            } }
        };

        _motivationDispellingPropPerScenario = new Dictionary<ScenarioType, string> {
            { ScenarioType.ConsertMurderCrime, "콧소리가 녹음된 MP3" },
            { ScenarioType.HospitalMurderCrime, "눌러 쓴 손편지" },
            { ScenarioType.HighSchoolMurderCrime, "자산 관리 위탁 계약서" },
            { ScenarioType.WeddingMurderCrime, "축가용 마이크" }
        };

        _propsPerScenario = new Dictionary<ScenarioType, List<string>> {
            { ScenarioType.ConsertMurderCrime, new List<string> { "무대 마이크", "메이크업 파우치" } },
            { ScenarioType.HospitalMurderCrime, new List<string> { "혈액팩", "수술용 마스크" } },
            { ScenarioType.HighSchoolMurderCrime, new List<string> { "칠판 지우개", "외제차 차키" } },
            { ScenarioType.WeddingMurderCrime, new List<string> { "꽃다발", "드레스 장갑" } }
        };
    }

    public List<MotivationExplainProp> LoadMotivationExpainProps(
        ScenarioType scenarioType,
        List<SuspectInfo> suspects,
        SuspectInfo excludingFakeSuspect)
    {
        int suspectsNum = suspects.Count;
        List<MotivationExplainProp> newMotivationExplainProps = new List<MotivationExplainProp>();

        for (int i = 0; i < suspectsNum; i++)
        {
            SuspectInfo eachSuspect = suspects[i];
            if (eachSuspect != excludingFakeSuspect)
            {
                (string, string) propInfo = _motivationExplainPropsPerScenario[scenarioType][eachSuspect.GetSuspectCode()];
                MotivationExplainProp newProp = new MotivationExplainProp(propInfo.Item1, eachSuspect.GetSuspectCode(), propInfo.Item2);  
                newMotivationExplainProps.Add(newProp);
            }
            else
            {
                string propName = _motivationDispellingPropPerScenario[scenarioType];
                MotivationExplainProp newProp = new MotivationExplainProp(propName, eachSuspect.GetSuspectCode(), dispelledCrimeMotivation);
                newMotivationExplainProps.Add(newProp);
            }
        }

        return newMotivationExplainProps;
    }

    public List<Evidence> LoadProps(ScenarioType scenarioType)
    {
        List<Evidence> newProps = new List<Evidence>();
        List<string> propsList = _propsPerScenario[scenarioType];
        int propsNum = propsList.Count;
        
        for (int i = 0; i < propsNum; i++)
        {
            Evidence newProp = new Evidence(propsList[i]);
            newProps.Add(newProp);
        }

        return newProps;
    }
}
