using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence
{
    string fileName;
    string flavorText;
    string name;

    public Evidence(string inputName)
    {
        name = inputName;
    }

    public void SetFileName(string inputFilename)
    {
        fileName = inputFilename;
    }

    public void SetFlavorText(string inputFlavorText)
    {
        flavorText = inputFlavorText;
    }

    public string GetFilename()
    {
        return fileName;
    }

    public string GetflavorText()
    {
        return flavorText;
    }

    public string GetName()
    {
        return name;
    }
}