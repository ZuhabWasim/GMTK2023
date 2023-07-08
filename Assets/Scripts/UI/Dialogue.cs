using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Dialogue Implementation -
Credits to Darren Tran
*/
public enum DialogueType // your custom enumeration
{
    Regular,
    Starting,
    Ending,
    Interjecting,
};

public enum Emotion
{
    Neutral,
    Angry,
    Happy,
    Disgust
}

[System.Serializable]
public class Dialogue
{
    [Tooltip("")]
    public DialogueType type;

    public Emotion emotion;

    public float waitTime = 0;

    public Vector3 position;

    public List<Sentence> sentences;
}

[System.Serializable]
public class Sentence
{
    public string name;

    [TextArea(3, 18)]
    public string text;
}
