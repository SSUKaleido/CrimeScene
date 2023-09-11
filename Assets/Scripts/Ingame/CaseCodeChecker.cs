using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class CaseCodeChecker
{
    public static string GenerateNewCaseCode()
    {
        string newCaseCode = "23";
        Random.InitState((int)System.DateTime.Now.Ticks);
        
        string alphabets = "가나다라마바사아자차카타파하";
        string hexadecimals = "0123456789ABCDEF";

        newCaseCode += alphabets[Random.Range(0, 14)];
        for (int i = 0; i < 4; i++)
            newCaseCode += hexadecimals[Random.Range(0, 16)];
        
        return newCaseCode;
    }

    public static bool CheckRightCaseCode(string inputCaseCode)
    {
        Regex regex = new Regex(@"23[가,나,다,라,마,바,사,아,자,차,카,타,파,하][0-F]{4}$");
        if (regex.IsMatch(inputCaseCode))
            return true;
        return false;
    }
}