using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using SuspectCode = Define.SuspectCode;
using ScenarioType = Define.ScenarioType;

/**
* 4개의 사건 시나리오 포맷 중 하나를 골라 로딩하는 클래스
*/
public class ScenarioLoader
{
    /** 시나리오 종류 리스트 */
    List<ScenarioType> _crimeCaseScenarioList = null;
    Dictionary<ScenarioType, List<string>> _victimGenderPerScenrario = null;
    Dictionary<ScenarioType, (int, int)> _victimAgeGapPerScenario = null;
    Dictionary<ScenarioType, List<string>> _victimJopPerScenario = null;
    Dictionary<ScenarioType, List<string>> _victimCauseOfDeathPerScenario = null;
    Dictionary<ScenarioType, List<SuspectCode>> _suspectCodePerScenario = null;
    Dictionary<ScenarioType, Dictionary<SuspectCode, List<string>>> _suspectGenderPerScenario = null;
    Dictionary<ScenarioType, Dictionary<SuspectCode, (int, int)>> _suspectAgeGapPerScenario = null;
    Dictionary<ScenarioType, Dictionary<SuspectCode, List<string>>> _suspectJopPerScenario = null;
    Dictionary<ScenarioType, Dictionary<SuspectCode, List<string>>> _suspectRelationPerScenrario = null;

    PersonNameLoader _personNameLoader = null;

    
    public ScenarioLoader()
    {
        /**_crimeCaseScenarioList = new List<ScenarioType>() {
        ScenarioType.ConsertMurderCrime,
        ScenarioType.HospitalMurderCrime,
        ScenarioType.HighSchoolMurderCrime,
        ScenarioType.WeddingMurderCrime
        }; **/

        _crimeCaseScenarioList = new List<ScenarioType>() {
        ScenarioType.ConsertMurderCrime
        };

        _victimGenderPerScenrario = new Dictionary<ScenarioType, List<string>> {
        { ScenarioType.ConsertMurderCrime, new List<string> { "Male", "Female" } },
        { ScenarioType.HospitalMurderCrime, new List<string> { "Male", "Female" } },
        { ScenarioType.HighSchoolMurderCrime, new List<string> { "Male" } },
        { ScenarioType.WeddingMurderCrime, new List<string> { "Female" } }
        };
        
        _victimAgeGapPerScenario = new Dictionary<ScenarioType, (int, int)> {
        { ScenarioType.ConsertMurderCrime, (31, 48) },
        { ScenarioType.HospitalMurderCrime, (26, 29) },
        { ScenarioType.HighSchoolMurderCrime, (27, 27) },
        { ScenarioType.WeddingMurderCrime, (28, 34) }
        };

        _victimJopPerScenario = new Dictionary<ScenarioType, List<string>> {
        { ScenarioType.ConsertMurderCrime, new List<string> { "뮤지션" } },
        { ScenarioType.HospitalMurderCrime, new List<string> { "레지던트" } },
        { ScenarioType.HighSchoolMurderCrime, new List<string> { "대학생", "취준생" } },
        { ScenarioType.WeddingMurderCrime, new List<string> { "교사", "프로그래머", "주부", "회사원" } }
        };

        _victimCauseOfDeathPerScenario = new Dictionary<ScenarioType, List<string>> {
        { ScenarioType.ConsertMurderCrime, new List<string> { "창살", "교살", "타살" } },
        { ScenarioType.HospitalMurderCrime, new List<string> { "자상", "창살", "독살" } },
        { ScenarioType.HighSchoolMurderCrime, new List<string> { "자상", "교살", "타살" } },
        { ScenarioType.WeddingMurderCrime, new List<string> { "자상", "타살", "독살" } }
        };

        _suspectCodePerScenario = new Dictionary<ScenarioType, List<SuspectCode>> {
            { ScenarioType.ConsertMurderCrime, new List<SuspectCode> {
                SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4
            } },
            { ScenarioType.HospitalMurderCrime, new List<SuspectCode> {
                SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4, SuspectCode.Suspect5
            } },
            { ScenarioType.HighSchoolMurderCrime, new List<SuspectCode> {
                SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4
            } },
            { ScenarioType.WeddingMurderCrime, new List<SuspectCode> {
                SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4
            } },
        };

        _suspectGenderPerScenario = new Dictionary<ScenarioType, Dictionary<SuspectCode, List<string>>> {
            { ScenarioType.ConsertMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect2, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect3, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect4, new List<string> { "Female" } }
            } },
            { ScenarioType.HospitalMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect2, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect3, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect4, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect5, new List<string> { "Male", "Female" } },
            } },
            { ScenarioType.HighSchoolMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "Male", "Female" } },
                { SuspectCode.Suspect2, new List<string> { "Female" } },
                { SuspectCode.Suspect3, new List<string> { "Male" } },
                { SuspectCode.Suspect4, new List<string> { "Male", "Female" } }
            } },
            { ScenarioType.WeddingMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "Male" } },
                { SuspectCode.Suspect2, new List<string> { "Female" } },
                { SuspectCode.Suspect3, new List<string> { "Female" } },
                { SuspectCode.Suspect4, new List<string> { "Female" } },
            } }
        };

        _suspectAgeGapPerScenario = new Dictionary<ScenarioType, Dictionary<SuspectCode, (int, int)>> {
            { ScenarioType.ConsertMurderCrime, new Dictionary<SuspectCode, (int, int)> {
                { SuspectCode.Suspect1, (20, 58) },
                { SuspectCode.Suspect2, (30, 60) },
                { SuspectCode.Suspect3, (26, 40) },
                { SuspectCode.Suspect4, (18, 24) }
            } },
            { ScenarioType.HospitalMurderCrime, new Dictionary<SuspectCode, (int, int)> {
                { SuspectCode.Suspect1, (26, 29) },
                { SuspectCode.Suspect2, (26, 34) },
                { SuspectCode.Suspect3, (26, 34) },
                { SuspectCode.Suspect4, (40, 60) },
                { SuspectCode.Suspect5, (30, 60) }
            } },
            { ScenarioType.HighSchoolMurderCrime, new Dictionary<SuspectCode, (int, int)> {
                { SuspectCode.Suspect1, (27, 27) },
                { SuspectCode.Suspect2, (34, 37) },
                { SuspectCode.Suspect3, (27, 27) },
                { SuspectCode.Suspect4, (27, 27) }
            } },
            { ScenarioType.WeddingMurderCrime, new Dictionary<SuspectCode, (int, int)> {
                { SuspectCode.Suspect1, (30, 34) },
                { SuspectCode.Suspect2, (28, 34) },
                { SuspectCode.Suspect3, (50, 70) },
                { SuspectCode.Suspect4, (30, 36) }
            } }
        };

        _suspectJopPerScenario = new Dictionary<ScenarioType, Dictionary<SuspectCode, List<string>>> {
            { ScenarioType.ConsertMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "싱어송라이터" } },
                { SuspectCode.Suspect2, new List<string> { "작곡가" } },
                { SuspectCode.Suspect3, new List<string> { "매니저", "스타일리스트" } },
                { SuspectCode.Suspect4, new List<string> { "홈페이지 마스터" } },
            } },
            { ScenarioType.HospitalMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "레지던트" } },
                { SuspectCode.Suspect2, new List<string> { "레지던트" } },
                { SuspectCode.Suspect3, new List<string> { "간호사" } },
                { SuspectCode.Suspect4, new List<string> { "펠로우" } },
                { SuspectCode.Suspect5, new List<string> { "경비원" } },
            } },
            { ScenarioType.HighSchoolMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "대학생", "취준생" } },
                { SuspectCode.Suspect2, new List<string> { "교사" } },
                { SuspectCode.Suspect3, new List<string> { "취준생" } },
                { SuspectCode.Suspect4, new List<string> { "대학생", "회사원" } }
            } },
            { ScenarioType.WeddingMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "경영인" } },
                { SuspectCode.Suspect2, new List<string> { "회사원" } },
                { SuspectCode.Suspect3, new List<string> { "주부" } },
                { SuspectCode.Suspect4, new List<string> { "회사원" } }
            } }
        };

        _suspectRelationPerScenrario = new Dictionary<ScenarioType, Dictionary<SuspectCode, List<string>>> {
            { ScenarioType.ConsertMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "지인" } },
                { SuspectCode.Suspect2, new List<string> { "지인" } },
                { SuspectCode.Suspect3, new List<string> { "직장 동료" } },
                { SuspectCode.Suspect4, new List<string> { "사생팬" } },
            } },
            { ScenarioType.HospitalMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "친구" } },
                { SuspectCode.Suspect2, new List<string> { "직장 동료" } },
                { SuspectCode.Suspect3, new List<string> { "직장 동료" } },
                { SuspectCode.Suspect4, new List<string> { "교수자" } },
                { SuspectCode.Suspect5, new List<string> { "지인" } },
            } },
            { ScenarioType.HighSchoolMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "동창" } },
                { SuspectCode.Suspect2, new List<string> { "스승" } },
                { SuspectCode.Suspect3, new List<string> { "친구" } },
                { SuspectCode.Suspect4, new List<string> { "동창" } },
            } },
            { ScenarioType.WeddingMurderCrime, new Dictionary<SuspectCode, List<string>> {
                { SuspectCode.Suspect1, new List<string> { "신랑" } },
                { SuspectCode.Suspect2, new List<string> { "신랑 친구", "친구" } },
                { SuspectCode.Suspect3, new List<string> { "예비 시어머니" } },
                { SuspectCode.Suspect4, new List<string> { "신랑의 전애인" } },
            } }
        };
    }

    /** 랜덤으로 하나의 시나리오를 결정 */
    public ScenarioType LoadCrimeCaseScenrario()
    {
        return _crimeCaseScenarioList[Random.Range(0,_crimeCaseScenarioList.Count)];
    }

    /** 피해자 정보를 로딩 **/
    public VictimInfo LoadVictim(ScenarioType scenarioType)
    {
        /**
        * _persionNameLoader가 없을 경우 하나 생성.
        * 인스턴스된 PersonNameLoader는 내부적으로 중복 이름이 리턴되지 않도록 관리
        **/
        if (_personNameLoader == null)
            _personNameLoader = new PersonNameLoader();
        
        /** 성별, 나이를 위 딕셔너리에 따라 랜덤 결정하고 그에 맞춰 이름까지 로딩 **/
        string newVictimGedner = _personNameLoader.CreateGender(_victimGenderPerScenrario[scenarioType]);
        int newVictimeAge = _personNameLoader.CreateAge(_victimAgeGapPerScenario[scenarioType]);
        string newVictimName = _personNameLoader.LoadName(newVictimGedner, newVictimeAge);
        /** 위 딕셔너리에 따라 직업과 사망 원인도 랜덤 결정 **/
        string newVictimJop = LoadJop(scenarioType, _victimJopPerScenario[scenarioType]);

        /** 중복 이름 제거는 PersonNameLoader에서 자동 수행 **/

        return new VictimInfo(newVictimName, newVictimGedner, newVictimeAge, newVictimJop);
    }

    /** 용의자 정보를 로딩 **/
    public SuspectInfo LoadSuspect(ScenarioType scenarioType)
    {
        /**
        * _persionNameLoader가 없을 경우 하나 생성.
        * 인스턴스된 PersonNameLoader는 내부적으로 중복 이름이 리턴되지 않도록 관리
        **/
        if (_personNameLoader == null)
            _personNameLoader = new PersonNameLoader();

        /** 4~5명의 용의자 중 랜덤으로 용의자 코드 로딩 **/
        int newSuspectCodeIndex = Random.Range(0, _suspectCodePerScenario[scenarioType].Count);
        SuspectCode newSuspectCode = _suspectCodePerScenario[scenarioType][newSuspectCodeIndex];

        /** 성별, 나이를 위 딕셔너리에 따라 랜덤 결정하고 그에 맞춰 이름까지 로딩 **/
        string newSuspectGedner = _personNameLoader.CreateGender(_suspectGenderPerScenario[scenarioType][newSuspectCode]);
        int newSuspectAge = _personNameLoader.CreateAge(_suspectAgeGapPerScenario[scenarioType][newSuspectCode]);
        string newSuspectName = _personNameLoader.LoadName(newSuspectGedner, newSuspectAge);
        /** 위 딕셔너리에 따라 직업과 피해자와의 관계도 랜덤 결정 **/
        string newVictimJop = LoadJop(scenarioType, _suspectJopPerScenario[scenarioType][newSuspectCode]);
        string newRelationWithVictim = LoadRelationWithVictim(_suspectRelationPerScenrario[scenarioType][newSuspectCode]);

        /** 중복 용의자 코드 딕셔너리에서 제거 수행 **/
        _suspectCodePerScenario[scenarioType].RemoveAt(newSuspectCodeIndex);

        return new SuspectInfo(newSuspectName, newSuspectGedner, newSuspectAge, newVictimJop, newSuspectCode, newRelationWithVictim);
    }

    private string LoadJop(ScenarioType scenarioType, List<string> possibleJopList)
    {
        return possibleJopList[Random.Range(0, possibleJopList.Count)];
    }

    private string LoadCauseOfDeath(List<string> candidateWounds)
    {
        return candidateWounds[Random.Range(0, candidateWounds.Count)];
    }

    private string LoadRelationWithVictim(List<string> candidateRelations)
    {
        return candidateRelations[Random.Range(0, candidateRelations.Count)];
    }
}