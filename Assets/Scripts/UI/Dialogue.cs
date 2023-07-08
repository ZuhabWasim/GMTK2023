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

[System.Serializable]
public class Dialogue
{
    [Tooltip("Whether the dialogue should be the first, last, normal")]
    public DialogueType type;

    [Tooltip("What emotion this dialogue should be associated with.")]
    public Types.Emotion emotion;

    [Tooltip("Who will feel this emotion.")]
    public Person person;

    [Tooltip("How long to wait before going to the next dialogue (0 means wait indefinitely).")]
    public float waitTime = 0;

    [Tooltip("Where the dialogue is on the screen (1920x1080)")]
    public Vector3 position;

    [Tooltip("Write down the sentences you'd want to appear")]
    public List<Sentence> sentences;

    [Tooltip("How long you want to hang on this dialogue before another can play.")]
    public float hangTime = 0f;
}

[System.Serializable]
public class Sentence
{
    [Tooltip("If not a person, you can enter any generic name.")]
    public string name;

    [TextArea(3, 18)]
    public string text;
}
