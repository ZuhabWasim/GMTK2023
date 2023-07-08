using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using System;

public class Person : MonoBehaviour
{
    public string characterName;
    public Emotion emotion;

    [SerializeField]
    private PersonAnimator personAnimator;

    void Start()
    {
        DialogueManager dm = FindObjectOfType<DialogueManager>();
        if (dm)
            dm.PersonEmote += CatchEmote;
    }

    private void CatchEmote(Person person, Emotion emotion)
    {
        if (person != this)
            return;

        this.emotion = emotion;
        OnEmote();
    }

    public virtual void OnEmote()
    {
        personAnimator.UpdateSprite(emotion);
    }
}
