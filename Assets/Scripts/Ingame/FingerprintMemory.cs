using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuspectCode = Define.SuspectCode;

public class FingerprintMemory : Evidence
{
    string fingerPrintCode;
    SuspectCode suspectCode;

    public FingerprintMemory(string inputName, string inputFingerprintCode, SuspectCode inputSuspectCode = SuspectCode.DoNotSuspect)
    : base(inputName)
    {
        fingerPrintCode = inputFingerprintCode;
        suspectCode = inputSuspectCode;
    }

    public string GetFingerprintCode()
    {
        return fingerPrintCode;
    }

    public SuspectCode GetSuspectCode()
    {
        return suspectCode;
    }
}
