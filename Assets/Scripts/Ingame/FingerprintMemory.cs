using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuspectCode = Define.SuspectCode;

public class FingerprintMemory : Evidence
{
    int fingerPrintCode;
    SuspectCode suspectCode;

    public FingerprintMemory(string inputName, int inputFingerprintCode, SuspectCode inputSuspectCode = SuspectCode.DoNotSuspect)
    : base(inputName)
    {
        fingerPrintCode = inputFingerprintCode;
        suspectCode = inputSuspectCode;
    }

    public int GetFingerprintCode()
    {
        return fingerPrintCode;
    }

    public SuspectCode GetSuspectCode()
    {
        return suspectCode;
    }
}
