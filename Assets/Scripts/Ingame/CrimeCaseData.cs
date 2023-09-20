using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using ScenarioType = Define.ScenarioType;
using SuspectCode = Define.SuspectCode;
using System.IO;

/**
* 사건 로딩에 필요한 데이터들을 모아농은 클래스.
* 사건 코드에 따라 사건 내부 정보를 알고리즘에 따라 산출할 때 사용.
*/
public class CrimeCaseData
{
    private string caseCode;
    private string caseName;

    ScenarioType crimeCaseScenario;
    VictimInfo victim;
    List<SuspectInfo> suspects = new List<SuspectInfo>(3);
    SuspectInfo realCriminal;
    List<SuspectInfo> fakeSuspects = new List<SuspectInfo>(2);
    List<Weapon> weapons = new List<Weapon>(4);
    Weapon realWeapon;
    List<FingerprintMemory> fingerprintMemories = new List<FingerprintMemory>(4);
    List<MotivationExplainProp> motivationExplainProps = new List<MotivationExplainProp>(3);
    List<Evidence> props = new List<Evidence>(2);
    List<Evidence> evidences = new List<Evidence>(13);

    /** 마지막 지정한 진범 **/
    SuspectInfo finalPointedSuspect;

    public void LoadCrimeCase(string inputedCaseCode)
    {
        /** 사건 번호에 따라 해당 값을 랜덤 시드로 결정 **/
        caseCode = inputedCaseCode;
        Random.InitState(caseCode.GetHashCode());

        /** 새로운 시나리오 로더를 인스턴스 **/
        ScenarioLoader _scenarioLoader = new ScenarioLoader();

        /** 시나리오 종류와 피해자 정보 로딩, 사건 이름 결정 **/
        crimeCaseScenario = _scenarioLoader.LoadCrimeCaseScenrario();
        victim = _scenarioLoader.LoadVictim(crimeCaseScenario);
        SetCaseName();

        /** 새로운 지문 로더를 인스턴스 **/
        FingerprintLoader _fingerprintLoader = new FingerprintLoader();

        /** 용의자 정보와 용의자의 지문 저장 **/
        for (int i = 0; i < 3; i++)
        {
            suspects.Add(_scenarioLoader.LoadSuspect(crimeCaseScenario));
        }
        List<(string, int)> suspectsFingerpints = _fingerprintLoader.CreateFingerprintSet();
        for (int i = 0; i < 3; i++)
        {
            suspects[i].SetFingerprintType(suspectsFingerpints[i].Item1);
            suspects[i].SetFingerprintSpriteCode(suspectsFingerpints[i].Item2);
        }

        /** 진범과 가짜 용의자를 따로 구분, CrimeCaseData와 SuspectInfo에 모두 저장 **/
        realCriminal = suspects[Random.Range(0, 3)];
        realCriminal.SetRealCriminal();
        for (int i = 0; i < 3; i++)
        {
            if (suspects[i] != realCriminal)
                fakeSuspects.Add(suspects[i]);
        }
        
        /** 50% 확률로 가짜 용의자 순서를 뒤집음, 흉기 지문 범행동기 물품 단계에서 용의선상 배제 하는데 사용 **/
        if (Random.Range(0, 2) == 1)
        {
            SuspectInfo temp = fakeSuspects[0];
            fakeSuspects.Remove(temp);
            fakeSuspects.Add(temp);
        }

        /** 새로운 무기 로더를 인스턴스 **/
        WeaponLoader _weaponLoader = new WeaponLoader();

        /** 무기 생성 **/
        for (int i = 0; i < 4; i++)
        {
            weapons.Add(_weaponLoader.LoadWeapon(crimeCaseScenario));
        }

        /** 흉기 지정 후 무기에 지문 붙이는 순서 결정 흉기 -> 흉기랑 상처 타입이 같은 무기 -> 다른 무기 순 **/
        realWeapon = weapons[Random.Range(0, 4)];
        victim.SetCauseofDeath(realWeapon.GetWoundType());
        Queue<Weapon> fingerprintLayOnWeaponOrder = new Queue<Weapon>();
        fingerprintLayOnWeaponOrder.Enqueue(realWeapon);
        for (int i = 0; i < 4; i++)
        {
            if (weapons[i] != realWeapon && weapons[i].GetWoundType() == realWeapon.GetWoundType())
                fingerprintLayOnWeaponOrder.Enqueue(weapons[i]);
        }
        for (int i = 0; i < 4; i++)
        {
            if (!fingerprintLayOnWeaponOrder.Contains(weapons[i]))
                fingerprintLayOnWeaponOrder.Enqueue(weapons[i]);
        }

        /** 흉기에 지문 붙이기 **/
        foreach (Weapon eachWeapon in fingerprintLayOnWeaponOrder)
        {
            int laidFingerprintNum = Random.Range(2, 4);
            List<string> excluedFingerprintTypes = new List<string>();
            List<string> incluedFingerprintTypes = new List<string>();
            
            if (eachWeapon == realWeapon)
            {
                excluedFingerprintTypes.Add(fakeSuspects[0].GetFingerprintType()); //첫번째 가짜 용의자 지문서 배제
                incluedFingerprintTypes.Add(fakeSuspects[1].GetFingerprintType());
                incluedFingerprintTypes.Add(realCriminal.GetFingerprintType());
            }
            else if (eachWeapon != realWeapon && eachWeapon.GetWoundType() == realWeapon.GetWoundType())
            {
                excluedFingerprintTypes.Add(realCriminal.GetFingerprintType());
                excluedFingerprintTypes.Add(fakeSuspects[0].GetFingerprintType());
                /** 추후 범행동기 설명 소품에서 배제될 가짜 용의자의 지문을 무조건 포함 **/
                incluedFingerprintTypes.Add(fakeSuspects[1].GetFingerprintType());
            }

            List<(string, int)> laidFingerprintInfo
                = _fingerprintLoader.CreateFingerprintSet(true, laidFingerprintNum, excluedFingerprintTypes, incluedFingerprintTypes);
            eachWeapon.SetFingerprint(laidFingerprintInfo);
        }

        /** 지문 메모리 로딩 **/
        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                List<(string, int)> victimsFingerprintInfoList = _fingerprintLoader.CreateFingerprintSet(true, 1);
                (string, int) victimsFingerprintInfo = victimsFingerprintInfoList[0];
                FingerprintMemory newFingerprintMemory = new FingerprintMemory("지문#4", victimsFingerprintInfo.Item2);

                fingerprintMemories.Add(newFingerprintMemory);
            }
            else
            {
                SuspectInfo eachSuspect = suspects[i];
                FingerprintMemory newFingerprintMemory = new FingerprintMemory($"지문#{i + 1}", eachSuspect.GetFingerprintSpriteCode(), eachSuspect.GetSuspectCode());

                fingerprintMemories.Add(newFingerprintMemory);
            }
        }
        
        /** 새 소품 로더 인스턴스 **/
        PropLoader _propLoader = new PropLoader();

        /** 동기 부여 소품 로딩 **/
        motivationExplainProps = _propLoader.LoadMotivationExpainProps(crimeCaseScenario, suspects, fakeSuspects[1]);

        /** 평범한 소품 로딩 **/
        props = _propLoader.LoadProps(crimeCaseScenario);

        /** 생성한 무기, 지문 메모리, 소품 들을 모아서 evidences에 할당 **/
        evidences.AddRange(weapons);
        evidences.AddRange(fingerprintMemories);
        evidences.AddRange(motivationExplainProps);
        evidences.AddRange(props);

        /** 새 파일 이름 로더를 인스턴스 **/
        FilenameLoader _filenameLoader = new FilenameLoader();

        /** 13개의 단서들의 파일 이름이랑 플레이버 텍스트 할당 **/
        for (int i = 0; i < 13; i++)
        {
            evidences[i].SetFileName(_filenameLoader.GetFilename(evidences[i].GetName()));
            evidences[i].SetFlavorText(_filenameLoader.GetFlavorText(evidences[i], suspects));
        }
    }

    private void SetCaseName()
    {
        /** "이종은 군" 식으로 포맷팅 */
        string victimCallName = victim.GetName() + ((victim.GetGender() == "Male") ? " 군" : " 양");
        switch (crimeCaseScenario)
        {
            case ScenarioType.ConsertMurderCrime:
                caseName = $"뮤지션 {victimCallName} 콘서트 살인 사건";
                break;
            case ScenarioType.HospitalMurderCrime:
                caseName = $"해부실습 {victimCallName} 살인 사건";
                break;
            case ScenarioType.HighSchoolMurderCrime:
                caseName = $"화경고 동창회 {victimCallName} 살인 사건";
                break;
            case ScenarioType.WeddingMurderCrime:
                caseName = $"결혼식 신부 {victimCallName} 살인 사건";
                break;
            default :
                caseName = $"{victimCallName} 살인 사건";
                break;
        }
    }

    public string GetCaseCode()
    {
        return caseCode;
    }

    public string GetCaseName()
    {
        return caseName;
    }

    public ScenarioType GetScenarioType()
    {
        return crimeCaseScenario;
    }

    public VictimInfo GetVictim()
    {
        return victim;
    }

    public List<SuspectInfo> GetSuspects()
    {
        return suspects;
    }

    public SuspectInfo GetRealCriminal()
    {
        return realCriminal;
    }

    public List<Weapon> GetWeapons()
    {
        return weapons;
    }

    public Weapon GetRealWeapon()
    {
        return realWeapon;
    }

    public List<FingerprintMemory> GetFingerprintMemories()
    {
        return fingerprintMemories;
    }

    public List<MotivationExplainProp> GetMotivationExplainProps()
    {
        return motivationExplainProps;
    }

    public List<Evidence> GetProps()
    {
        return props;
    }

    public List<Evidence> GetEvidences()
    {
        return evidences;
    }

    public void SetFinalPointedSuspect(SuspectInfo input)
    {
        finalPointedSuspect = input;
    }

    public SuspectInfo GetFinalPointedSuspect()
    {
        return finalPointedSuspect;
    }
}