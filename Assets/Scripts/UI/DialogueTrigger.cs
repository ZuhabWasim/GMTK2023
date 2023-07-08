using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Dialogue Implementation -
Credits to Darren Tran
*/

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool triggered = false;

    public DialogueTrigger nextDialogue = null;

    public delegate void OnDialogueFinish();
    public event OnDialogueFinish DialogueFinished;

    void Start()
    {
        if (dialogue.type == DialogueType.Starting)
            TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        triggered = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);

        if (dialogue.type == DialogueType.Ending)
        {
            // TODO
        }
    }

    public void TriggerDialogueFinish()
    {
        DialogueFinished?.Invoke();
    }

    void OnDestroy()
    {
        if (nextDialogue)
            nextDialogue.TriggerDialogue();
    }
}
