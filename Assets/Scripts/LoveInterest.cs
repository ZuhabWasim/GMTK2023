using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class LoveInterest : Person
{
    public float affection;
    public Animator LoveInterestAnimator;

    public void UpdateAffection(float affectionAmount)
    {
        this.affection += affectionAmount;
    }

    public void SaveLoveInterest()
    {
        SaveSystem.SaveLoveInterest(this);
    }

    public void LoadLoveInterest()
    {
        LoveInterestData data = SaveSystem.LoadLoveInterest(this);

        this.characterName = data.characterName;
        this.affection = data.affection;
        this.emotion = (Emotion)data.emotion;
    }

    public void Reset()
    {
        SaveSystem.ClearPersistentData();
    }
}
