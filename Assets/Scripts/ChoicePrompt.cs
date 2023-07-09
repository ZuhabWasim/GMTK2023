using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public class Choice
{
    [SerializeField]
    public string choiceText;

    [SerializeField]
    public float affectionScore;

    [SerializeField]
    public DialogueTrigger dialogue;

    [SerializeField]
    public bool isDefaultChoice = false;
}

public class ChoicePrompt : MonoBehaviour
{
    public List<Choice> choices;

    public GameObject choiceTemplate;

    public DialogueTrigger trigger;

    public Timer timer;

    public float verticalSpacing = 100f;
    public float choiceTimerTotal = 5f;
    private float choiceTimer;

    [SerializeField]
    private Disdain _disdain;

    // Start is called before the first frame update
    void Start()
    {
        choiceTimer = choiceTimerTotal;
        if (trigger == null)
        {
            Debug.Log("No dialogue trigger associated with this tutorial.");
            return;
        }
        trigger.DialogueFinished += ShowChoices;
    }

    public void ShowChoices()
    {
        int i = 0;

        foreach (Choice choice in choices)
        {
            Vector3 position = new Vector3(
                transform.position.x,
                transform.position.y - ((i) * verticalSpacing),
                0
            );
            GameObject newChoice = Instantiate(
                choiceTemplate,
                position,
                transform.rotation,
                this.transform
            );

            newChoice.SetActive(true);
            newChoice.name = "Choice" + (i + 1);

            TextMeshProUGUI choiceTextBox = newChoice.GetComponentInChildren<TextMeshProUGUI>();

            choiceTextBox.text = choice.choiceText;

            Button button = newChoice.GetComponent<Button>();
            button.onClick.RemoveAllListeners();

            int tempChoiceIndex = i;
            if (choice.dialogue != null)
                button.onClick.AddListener(delegate { choice.dialogue.TriggerDialogue(); });
            button.onClick.AddListener(delegate { UpdateAffection(tempChoiceIndex); });
            button.onClick.AddListener(delegate { HideChoices(); });


            i += 1;
        }

        StartCoroutine(choicesTimer());
        timer.SetVisible(true);
    }

    IEnumerator choicesTimer()
    {
        while (choiceTimer > 0)
        {
            choiceTimer -= Time.deltaTime;
            timer.SetFill(choiceTimerTotal, choiceTimer);
            yield return null;
        }
        foreach (Choice choice in choices)
        {
            if (choice.isDefaultChoice)
            {
                choice.dialogue.TriggerDialogue();
                Protaganist pro = FindObjectOfType<Protaganist>();
                if (pro)
                    pro.AddAffection(choice.affectionScore);
                break;
            }
        }
        HideChoices();
        timer.SetVisible(false);
    }

    public void UpdateAffection(int index)
    {
        Protaganist pro = FindObjectOfType<Protaganist>();
        if (pro)
            pro.AddAffection(choices[index].affectionScore);

        Debug.Log(index);
        //TODO: remove this once we add characters just want to test it for now
        _disdain.UpdateDisdain(-choices[index].affectionScore);
    }

    public void HideChoices()
    {
        this.gameObject.SetActive(false);
        timer.SetVisible(false);
    }

    void OnDestroy()
    {
        try
        {
            trigger.DialogueFinished -= ShowChoices;
        }
        catch (NullReferenceException) { }
    }
}
