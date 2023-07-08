using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using UnityEngine.UI;

[System.Serializable]
public class CharacterSprite
{
    [SerializeField]
    public Emotion emotion;

    [SerializeField]
    public Image sprite;
}

public class PersonAnimator : MonoBehaviour
{
    public Person person;

    public Vector3 dialoguePosition;

    public AudioClip dialogueSound;

    [SerializeField]
    private List<CharacterSprite> sprites;

    void Start()
    {
        foreach (CharacterSprite charSprite in sprites)
        {
            HideSprite(charSprite);
        }
    }

    public void UpdateSprite(Emotion emotion)
    {
        foreach (CharacterSprite charSprite in sprites)
        {
            Debug.Log(charSprite.emotion == emotion);
            if (charSprite.emotion == emotion)
                ShowSprite(charSprite);
            else
                HideSprite(charSprite);
        }
    }

    public void HideSprite(CharacterSprite charSprite)
    {
        charSprite.sprite.enabled = false;
    }

    public void ShowSprite(CharacterSprite charSprite)
    {
        charSprite.sprite.enabled = true;
    }
}
