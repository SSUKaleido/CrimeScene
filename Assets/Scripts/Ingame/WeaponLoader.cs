using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using ScenarioType = Define.ScenarioType;
using SuspectCode = Define.SuspectCode;

public class WeaponLoader
{
    Dictionary<ScenarioType, List<(string, string, List<SuspectCode>)>> _weaponPerScenrario = null;

    public WeaponLoader()
    {
        _weaponPerScenrario = new Dictionary<ScenarioType, List<(string, string, List<SuspectCode>)>> {
            { ScenarioType.ConsertMurderCrime, new List<(string, string, List<SuspectCode>)> {
                ("창상", "마이크 스탠드",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect3 } ),
                ("창상", "가건물 골조",
                new List<SuspectCode>() ),
                ("교살", "기타 줄",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect4 } ),
                ("교살", "전선",
                new List<SuspectCode> { SuspectCode.Suspect3 } ),
                ("타살", "스포트라이트",
                new List<SuspectCode>() ),
                ("타살", "단속봉",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4 } )
            } },
            { ScenarioType.HospitalMurderCrime, new List<(string, string, List<SuspectCode>)> {
                ("자상", "메스",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect4 } ),
                ("자상", "송곳",
                new List<SuspectCode> { SuspectCode.Suspect3, SuspectCode.Suspect5 } ),
                ("창상", "원형 전기톱",
                new List<SuspectCode> { SuspectCode.Suspect3, SuspectCode.Suspect4 } ),
                ("창상", "양날칼",
                new List<SuspectCode> { SuspectCode.Suspect4 } ),
                ("독살", "주사기",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3 } ),
                ("독살", "포르말린",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect4, SuspectCode.Suspect5 } )
            } },
            { ScenarioType.HighSchoolMurderCrime, new List<(string, string, List<SuspectCode>)> {
                ("자상", "송곳",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4 } ),
                ("자상", "낫",
                new List<SuspectCode>() ),
                ("교살", "와이어",
                new List<SuspectCode> { SuspectCode.Suspect2 } ),
                ("교살", "목장갑",
                new List<SuspectCode>() ),
                ("타살", "술병",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect3, SuspectCode.Suspect4 }) ,
                ("타살", "야구 배트",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect3 } )
            } },
            { ScenarioType.WeddingMurderCrime, new List<(string, string, List<SuspectCode>)> {
                ("자상", "와인 오프너",
                new List<SuspectCode> { SuspectCode.Suspect2 } ),
                ("자상", "촛대",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect4 } ),
                ("타살", "꽃병",
                new List<SuspectCode> { SuspectCode.Suspect2, SuspectCode.Suspect3 } ),
                ("타살", "와인병",
                new List<SuspectCode> { SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4 } ),
                ("독살", "안약",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect3 } ),
                ("독살", "다이어트 약",
                new List<SuspectCode> { SuspectCode.Suspect1, SuspectCode.Suspect3 })
            } }
        };
    }

    public Weapon LoadWeapon(ScenarioType scenarioType)
    {
        List<(string, string, List<SuspectCode>)> weaponCandidates = _weaponPerScenrario[scenarioType];
        (string, string, List<SuspectCode>) newWeaponInfo = weaponCandidates[Random.Range(0, weaponCandidates.Count)];
        weaponCandidates.Remove(newWeaponInfo);

        // 나중에 첫번째 파라미터 파일이름으로 딕셔너리 만들어서 보내기
        return new Weapon(newWeaponInfo.Item2, newWeaponInfo.Item1, newWeaponInfo.Item3);
    }
}