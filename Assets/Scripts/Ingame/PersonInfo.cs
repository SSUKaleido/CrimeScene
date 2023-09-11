using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInfo
{
    string name;
    string gender;
    int age;
    string jop;

    public PersonInfo(string inputName, string inputGender, int inputAge, string inputJop)
    {
        name = inputName;
        gender = inputGender;
        age = inputAge;
        jop = inputJop;
    }

    public string GetName()
    {
        return name;
    }

    public string GetGender()
    {
        return gender;
    }

    public int GetAge()
    {
        return age;
    }

    public string GetJop()
    {
        return jop;
    }
}