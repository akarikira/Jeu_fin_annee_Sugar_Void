using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance;

    public bool knifeDropped = false;
    public bool fioleDropped = false; // changé ici

    public DialogueLine[] dialogueAprèsDépôt;

    private bool dialogueStarted = false;

    void Awake()
    {
        Instance = this;
    }

    public void CheckForCompletion()
    {
        if (knifeDropped && fioleDropped && !dialogueStarted)
        {
            dialogueStarted = true;
            DialogueSystem.Instance.ShowDialogue(dialogueAprèsDépôt);

            // Quand le dialogue du rituel est fini, on lance la scène finale
            DialogueSystem.Instance.OnDialogueEnd += LancerFinalScene;
        }
    }

    private void LancerFinalScene()
    {
        DialogueSystem.Instance.OnDialogueEnd -= LancerFinalScene;
        FindObjectOfType<FinalSceneManager>().StartFinalSequence();
    }
}
