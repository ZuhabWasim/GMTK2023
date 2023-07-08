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
        try
        {
            if (nextDialogue)
                nextDialogue.TriggerDialogue();
        }
        catch (System.NullReferenceException ex)
        {
            return;
            //Debug.Log("Caught referencing next Dialogue on game exit.");
        }
    }
}
