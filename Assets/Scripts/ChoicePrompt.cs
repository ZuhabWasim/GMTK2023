using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Events;

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

    public float verticalSpacing = 100f;
    public float choiceTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        ShowChoices();
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

            UnityEventTools.AddPersistentListener(button.onClick, choice.dialogue.TriggerDialogue);
            UnityEventTools.AddIntPersistentListener(button.onClick, UpdateAffection, i);
            UnityEventTools.AddPersistentListener(button.onClick, HideChoices);

            i += 1;
        }

        StartCoroutine(choicesTimer());
    }

    IEnumerator choicesTimer()
    {
        while (choiceTimer > 0)
        {
            choiceTimer -= Time.deltaTime;
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
    }

    public void UpdateAffection(int index)
    {
        Protaganist pro = FindObjectOfType<Protaganist>();
        if (pro)
            pro.AddAffection(choices[index].affectionScore);
    }

    public void HideChoices()
    {
        this.gameObject.SetActive(false);
    }
}
