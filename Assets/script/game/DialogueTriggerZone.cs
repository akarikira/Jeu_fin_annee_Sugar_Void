using UnityEngine;

public class DialogueTriggerZone : MonoBehaviour
{
    public DialogueLine[] lines; // Pas de string ici !

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player") && DialogueSystem.Instance != null && lines.Length > 0)
        {
            triggered = true;
            DialogueSystem.Instance.ShowDialogue(lines);
        }
    }
}
