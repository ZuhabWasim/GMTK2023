using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class LoveInterest : Person
{
    public int affection;
    public Animator LoveInterestAnimator;

    public void SaveLoveInterest() {
        SaveSystem.SaveLoveInterest(this);
    }

    public void LoadLoveInterest(string name) {
        LoveInterestData data = SaveSystem.LoadLoveInterest(name);

        this.characterName = data.characterName;
        this.affection = data.affection;
        this.emotion = (Emotion) data.emotion; 
    }

    public void Reset() {
        SaveSystem.ClearPersistentData();
    }
}
