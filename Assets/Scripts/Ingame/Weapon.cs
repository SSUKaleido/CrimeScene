using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuspectCode = Define.SuspectCode;

public class Weapon : Evidence
{
    string woundType;
    List<string> laidFingerprints = new List<string>();
    List<string> laidFingerPrintSpriteCodes = new List<string>();
    Dictionary<SuspectCode, string> suitabilityForSuspects = new Dictionary<SuspectCode, string>();

    const string strongSuitability = "매우 적합함";
    const string weakSuitability = "약간 적합함";

    public Weapon(string inputName, string inputWoundType, List<SuspectCode> inputStrongsuitabilitySuspects)
    : base(inputName)
    {
        woundType = inputWoundType;

        List<SuspectCode> tempSuspectCodes = new List<SuspectCode> {
            SuspectCode.Suspect1, SuspectCode.Suspect2, SuspectCode.Suspect3, SuspectCode.Suspect4, SuspectCode.Suspect5
        };
        foreach (SuspectCode eachSuspectCode in tempSuspectCodes)
        {
            if (inputStrongsuitabilitySuspects.Contains(eachSuspectCode))
            {
                suitabilityForSuspects.Add(eachSuspectCode, strongSuitability);
            }
            else
            {
                suitabilityForSuspects.Add(eachSuspectCode, weakSuitability);
            }
        }
    }

    public void SetFingerprint(List<(string, string)> fingerprintInfo)
    {
        int fingerprintInfoCount = fingerprintInfo.Count;

        for (int i = 0; i < fingerprintInfoCount; i++)
        {
            laidFingerprints.Add(fingerprintInfo[i].Item1);
            laidFingerPrintSpriteCodes.Add(fingerprintInfo[i].Item2);
        }
    }
    public string GetWoundType()
    {
        return woundType;
    }

    public List<string> GetLaidFingerprints()
    {
        return laidFingerprints;
    }

    public List<string> GetLaidFingerprintSpriteCodes()
    {
        return laidFingerPrintSpriteCodes;
    }

    public string GetSuitabilityForSuspect(SuspectCode inputSuspect)
    {
        return suitabilityForSuspects[inputSuspect];
    }
}