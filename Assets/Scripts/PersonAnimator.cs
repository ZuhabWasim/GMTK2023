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

[System.Serializable]
public class AccentSprite
{
    [SerializeField]
    public Emotion emotion;

    [SerializeField]
    public Image sprite;
}

public class PersonAnimator : MonoBehaviour
{
    public Person person;

    public GameObject dialoguePosition;

    public AudioClip dialogueSound;

    [SerializeField]
    private List<CharacterSprite> sprites;

    [SerializeField]
    private List<AccentSprite> accentSprites;

    void Start()
    {
        foreach (CharacterSprite charSprite in sprites)
        {
            HideSprite(charSprite);
        }
        foreach (AccentSprite accentSprite in accentSprites)
        {
            HideSprite(accentSprite);
        }
    }

    public void UpdateSprite(Emotion emotion)
    {
        foreach (CharacterSprite charSprite in sprites)
        {
            if (charSprite.emotion == emotion)
                ShowSprite(charSprite);
            else
                HideSprite(charSprite);
        }
        foreach (AccentSprite accentSprite in accentSprites)
        {
            if (accentSprite.emotion == emotion)
                ShowSprite(accentSprite);
            else
                HideSprite(accentSprite);
        }
    }

    public void HideSprite(CharacterSprite charSprite)
    {
        charSprite.sprite.enabled = false;
    }

    public void HideSprite(AccentSprite accentSprite)
    {
        accentSprite.sprite.enabled = false;
    }

    public void ShowSprite(CharacterSprite charSprite)
    {
        charSprite.sprite.enabled = true;
    }

    public void ShowSprite(AccentSprite accentSprite)
    {
        accentSprite.sprite.enabled = true;
    }
}
