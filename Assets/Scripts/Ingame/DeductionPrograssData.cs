using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using ScenarioType = Define.ScenarioType;
using SuspectCode = Define.SuspectCode;

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

    /** 각 흉기 후보들 간의 지문 검출 현황을 저장해놓는 딕셔너리 **/
    private Dictionary<string, Dictionary<SuspectCode, bool>> IsDetectiveFingerprintFromWeapons = null;

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

            IsDetectiveFingerprintFromWeapons.Add(eachWepon.GetName(), temp);
        }
    }
}