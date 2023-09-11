using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimInfo : PersonInfo
{
    string causeOfDeath;

    
    public VictimInfo(string inputName, string inputGender, int inputAge, string inputJop)
    : base(inputName, inputGender, inputAge, inputJop)
    {
    }

    public void SetCauseofDeath(string inputCauseOfDeath)
    {
        causeOfDeath = inputCauseOfDeath;
    }

    public string GetCauseOfDeath()
    {
        return causeOfDeath;
    }
}
