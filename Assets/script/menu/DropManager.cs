using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance;

    public DropZone couteauZone;
    public DropZone fioleZone;

    public DialogueLine[] dialogueAprèsDépôt;
    public GameObject _canva;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _canva.SetActive(false);
    }

    public void CheckAllDeposited()
    {
        if (couteauZone.IsDeposited && fioleZone.IsDeposited)
        {
            DialogueSystem.Instance.OnDialogueEnd += ActivateCanvas;
            DialogueSystem.Instance.ShowDialogue(dialogueAprèsDépôt);
        }
    }

    private void ActivateCanvas()
    {
        _canva.SetActive(true);
        DialogueSystem.Instance.OnDialogueEnd -= ActivateCanvas; // Important pour éviter de le faire plusieurs fois
    }
}
