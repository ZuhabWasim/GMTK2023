using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Types;

/*
Dialogue Implementation -
Credits to Darren Tran
*/
public class DialogueManager : MonoBehaviour
{
    [Header("Object References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public AudioSource playsound;

    private Queue<Sentence> sentences;

    public delegate void OnPersonEmoting(Person person, Emotion emotion);
    public event OnPersonEmoting PersonEmote;

    public delegate void OnGameOver();
    public event OnGameOver GameOver;

    // Check if the user was pressing skip from the previous Dialogue sentence.
    private bool skippingPrevious = false;

    public float timer;

    public DialogueTrigger currentTrigger;

    public void TriggerDialogueObject(string dialogueObjectName)
    {
        GameObject go = GameObject.Find(dialogueObjectName);
        DialogueTrigger trigger = (DialogueTrigger)go.GetComponent(typeof(DialogueTrigger));
        trigger.TriggerDialogue();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        // Destroy any previous interrupted dialogue.
        if (currentTrigger != null)
            DestroyTrigger();
        currentTrigger = trigger;

        // Update dialogue box.
        animator.SetBool("IsOpen", true);
        animator.gameObject.transform.parent.transform.position = dialogue.position;

        // Invoke event.
        if (dialogue.person)
            PersonEmote?.Invoke(dialogue.person, dialogue.emotion);

        // Build the sentences to be displayed.
        sentences = new Queue<Sentence>();
        sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(Dialogue dialogue)
    {
        if (sentences.Count == 0)
        {
            EndDialogue(dialogue);
            return;
        }

        Sentence sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, dialogue));
    }

    IEnumerator TypeSentence(Sentence sentence, Dialogue dialogue)
    {
        nameText.text = sentence.name;
        dialogueText.text = "";

        if (playsound)
            playsound.GetComponent<AudioSource>().Play();

        foreach (char letter in sentence.text.ToCharArray())
        {
            dialogueText.text += letter;
            if (!skippingPrevious && Input.GetButton("Skip"))
            {
                dialogueText.text = sentence.text;
                while (!Input.GetButtonUp("Skip"))
                {
                    yield return null;
                }
                break;
            }
            skippingPrevious = Input.GetButton("Skip");

            yield return new WaitForSeconds(0.02f);
        }

        skippingPrevious = true;
        timer = (dialogue.waitTime <= 0) ? Mathf.Infinity : dialogue.waitTime;

        while (!Input.GetButtonDown("Skip") && timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.02f);
        DisplayNextSentence(dialogue);
    }

    void EndDialogue(Dialogue dialogue)
    {
        StartCoroutine(DestroyTrigger());

        if (dialogue.type == DialogueType.Ending)
            GameOver?.Invoke();
    }

    IEnumerator DestroyTrigger()
    {
        animator.SetBool("IsOpen", false);
        yield return new WaitForSeconds(0.2f);
        currentTrigger.TriggerDialogueFinish();
        DialogueTrigger tempTrigger = currentTrigger;
        currentTrigger = null;
        Destroy(tempTrigger.gameObject);
    }
}
