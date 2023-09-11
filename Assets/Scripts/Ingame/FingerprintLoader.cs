using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FingerprintLoader
{
    List<string> fingerprints = null;
    Dictionary<string, List<string>> fingerprintSpriteTable = null;

    public FingerprintLoader()
    {
        fingerprintSpriteTable = new Dictionary<string, List<string>> {
            { "보통궁상문", new List<string> { "1-1", "1-2", "1-3", "1-4", "1-5"} },
            { "돌기궁상문", new List<string> { "2-1", "2-2", "2-3", "2-4", "2-5"} },
            { "와상문", new List<string> { "3-1", "3-2", "3-3", "3-4", "3-5"} },
            { "좌제상문", new List<string> { "4-1", "4-2", "4-3", "4-4", "4-5"} },
            { "우제상문", new List<string> { "5-1", "5-2", "5-3", "5-4", "5-5"} },
            { "변태문", new List<string> { "6-1", "6-2", "6-3", "6-4" } }
        };
    }

    /**
    * 생성할 수 있는 지문 타입을 초기화, 용의자들이나 한 흉기에 지문 전부 할당했으면 호출
    */
    private void InitFingerprintTypeList()
    {
       fingerprints = new List<string> {
            "보통궁상문", "돌기궁상문", "와상문", "좌제상문", "우제상문", "변태문"
        }; 
    }

    /**
    * 용의자들이 하나씩 가질 지문과 한 흉기에 묻어 있는 지문들 생성, 각 세트 안에는 중복 지문이 없음?
    * @param IsAllowRedundancy 지문 세트 안에서 배제할 지문이 있는지, 기본 값 false
    * @param setSize 한 세트가 몇 개의 지문을 반환하는지, 기본 값은 3
    * @param excluedFingerprintTypes 이번에 생성하는 셋에서 포함되지 말아야 할 지문 종류들. 기본 값은 변태문 하나.
    * @param incluedFingerprintTypes 이번에 생성하는 셋에서 무조건 포함되어야 할 지문 종류들. 기본 값은 없음.
    **/
    public List<(string, string)> CreateFingerprintSet (
    bool IsAllowRedundancy = false,
    int setSize = 3,
    List<string> excluedFingerprintTypes = null,
    List<string> incluedFingerprintTypes = null)
    {
        /** 두 지문 목록 리스트가 null일 경우 초기화 **/
        if (excluedFingerprintTypes == null)
            excluedFingerprintTypes = new List<string>() { "변태문" };
        if (incluedFingerprintTypes == null)
            incluedFingerprintTypes = new List<string>();
        
        /** 하나의 지문 세트를 생성하기 전에 초기화 **/
        InitFingerprintTypeList();

        List<(string, string)> newFingerprintInfo = new List<(string, string)>();

        /** 생성해야 하는 사이즈 만큼 for문 돌리기 **/
        for (int createdFingerprint = 0; createdFingerprint < setSize; createdFingerprint++)
        {
            string newFingerprintType;

            /** 포함해야 하는 지문이 있으면 그 뒤에서 가져오기, 그렇지 않으면 랜덤으로 하나 생성 **/
            if (incluedFingerprintTypes.Count > 0)
            {
                newFingerprintType = incluedFingerprintTypes[Random.Range(0, incluedFingerprintTypes.Count)];
                incluedFingerprintTypes.Remove(newFingerprintType);
            }
            else
            {
                newFingerprintType = getPossibleFingerprintType(excluedFingerprintTypes);
            }

            /** 지문 코드를 얻음 **/
            List<string> fingerprintSpriteList = fingerprintSpriteTable[newFingerprintType];
            string newFingerprintSpriteCode = fingerprintSpriteList[Random.Range(0, fingerprintSpriteList.Count)];

            /** 중복을 허용하지 않을 경우 생성 가능한 목록 리스트에서 제거, 스프라이트 코드는 무조건 제거 **/
            if (!IsAllowRedundancy)
                fingerprints.Remove(newFingerprintType);
            fingerprintSpriteList.Remove(newFingerprintSpriteCode);

            newFingerprintInfo.Add((newFingerprintType, newFingerprintSpriteCode));
        }

        return newFingerprintInfo;
    }

    /**
    * 스프라이트가 남아있는 가능한 지문 종류 리턴
    * @param excluedFingerprintTypes 이번에 생성하는 셋에서 포함되지 말아야 할 지문 종류들.
    **/
    private string getPossibleFingerprintType(List<string> excluedFingerprintTypes)
    {
        int diceNum = 0;
        string newFingerprintType;

        /** fingerprintSpriteTable에 남은 스프라이트 코드가 있는 지문 종류를 찾을 때까지 랜덤 다이스 **/
        while (true)
        {
            diceNum = Random.Range(0, fingerprints.Count);
            newFingerprintType = fingerprints[diceNum];

            if (!excluedFingerprintTypes.Contains(newFingerprintType) && fingerprintSpriteTable[newFingerprintType].Count > 0)
                break;
        }

        return newFingerprintType;
    }
}