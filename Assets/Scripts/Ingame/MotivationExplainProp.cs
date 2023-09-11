using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuspectCode = Define.SuspectCode;

public class MotivationExplainProp : Evidence
{
    SuspectCode explaningSuspect;
    string motivation;

    public MotivationExplainProp(string inputname, SuspectCode inputExplaningSuspect, string inputMotivation)
    : base(inputname)
    {
        explaningSuspect = inputExplaningSuspect;
        motivation = inputMotivation;
    }

    public SuspectCode GetExplainingSuspect()
    {
        return explaningSuspect;
    }

    public string GetMotivation()
    {
        return motivation;
    }
}