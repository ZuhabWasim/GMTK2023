using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoicePrompt : MonoBehaviour
{
    
    private bool choicePresented;

    public string choice1Text;
    public string choice2Text;
    public string choice3Text;

    public TMP_Text choice1;
    public TMP_Text choice2;
    public TMP_Text choice3;

    public Transform char1;
    public Transform char2;

    public Transform[] char1Anchors;
    public Transform[] char2Anchors;

    
    // Start is called before the first frame update
    void Start()
    {
        choicePresented = false;
    }

    public void SwitchChoice() {
        if (choicePresented) {
            HideChoice();
        } else {
            ShowChoice();
        }
    }

    void SetText() {
        choice1.text = choice1Text;
        choice2.text = choice1Text;
        choice3.text = choice1Text;
    }

    void ShowChoice() {
        gameObject.SetActive(true);
        char1.position = char1Anchors[1].position;
        char2.position = char2Anchors[1].position;
        choicePresented = true;
    }

    void HideChoice() {
        gameObject.SetActive(false);
        char1.position = char1Anchors[0].position;
        char2.position = char2Anchors[0].position;
        choicePresented = false;
    }
}
