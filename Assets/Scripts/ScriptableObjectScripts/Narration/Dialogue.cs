using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    
    [TextArea(3,10)]
    public string[] lines;

    public bool hasText()
    {
        if (lines.Length <= 0)
        {
            return false;
        }

        return true;
    }
}
