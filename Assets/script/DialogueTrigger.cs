using UnityEngine;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    public List<DialogueSystem.DialogueLine> dialogueLines;
    public bool triggerOnEnter = true;
    public bool onlyOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerOnEnter && other.CompareTag("Player") && (!onlyOnce || !hasTriggered))
        {
            TriggerDialogue();
            hasTriggered = true;
        }
    }

    public void TriggerDialogue()
    {
        DialogueSystem.Instance.StartDialogue(dialogueLines);
    }
}