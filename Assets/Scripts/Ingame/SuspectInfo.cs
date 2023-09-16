using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuspectCode = Define.SuspectCode;

public class SuspectInfo : PersonInfo
{
    bool isRealCriminal;
    SuspectCode suspectCode;
    string relationWithVictim;
    string fingerprintType;
    int fingerprintSpriteCode;

    public SuspectInfo(string inputName, string inputGender, int inputAge, string inputJop, SuspectCode inputSuspectCode, string inputRealation)
    : base(inputName, inputGender, inputAge, inputJop)
    {
        isRealCriminal = false;
        suspectCode = inputSuspectCode;
        relationWithVictim = inputRealation;
    }

    public void SetRealCriminal()
    {
        isRealCriminal = true;
    }

    public void SetFingerprintType(string inputFingerprintType)
    {
        fingerprintType = inputFingerprintType;
    }

    public void SetFingerprintSpriteCode(int inputFingerprintSpriteCode)
    {
        fingerprintSpriteCode = inputFingerprintSpriteCode;
    }

    public SuspectCode GetSuspectCode()
    {
        return suspectCode;
    }

    public string GetRelationWithVictim()
    {
        return relationWithVictim;
    }

    public string GetFingerprintType()
    {
        return fingerprintType;
    }

    public int GetFingerprintSpriteCode()
    {
        return fingerprintSpriteCode;
    }

    public bool GetIsRealCriminal()
    {
        return isRealCriminal;
    }
}