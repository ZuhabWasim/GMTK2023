using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protaganist : Person
{
    public LoveInterest currentLoveInterest;

    public void AddAffection(float affectionAmount)
    {
        currentLoveInterest.UpdateAffection(affectionAmount);
    }
}
