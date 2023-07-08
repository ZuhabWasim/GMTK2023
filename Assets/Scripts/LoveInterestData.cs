using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoveInterestData
{
    public string characterName;
    public float affection;
    public int emotion; // has to be a primitive

    public LoveInterestData(LoveInterest partner)
    {
        // if classes other than LoveInterest inherit Person, then
        // move characterName and affection to another function
        characterName = partner.characterName;
        affection = partner.affection;
        emotion = (int)partner.emotion;
    }
}
