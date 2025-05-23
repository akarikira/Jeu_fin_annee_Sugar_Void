using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance;

    public bool knifeDropped = false;
    public bool fioleDropped = false; // chang� ici

    public DialogueLine[] dialogueApr�sD�p�t;

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
            DialogueSystem.Instance.ShowDialogue(dialogueApr�sD�p�t);

            // Quand le dialogue du rituel est fini, on lance la sc�ne finale
            DialogueSystem.Instance.OnDialogueEnd += LancerFinalScene;
        }
    }

    private void LancerFinalScene()
    {
        DialogueSystem.Instance.OnDialogueEnd -= LancerFinalScene;
        FindObjectOfType<FinalSceneManager>().StartFinalSequence();
    }
}
