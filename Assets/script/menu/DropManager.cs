using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance;

    public DropZone couteauZone;
    public DropZone fioleZone;

    public DialogueLine[] dialogueApr�sD�p�t;
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
            DialogueSystem.Instance.ShowDialogue(dialogueApr�sD�p�t);
        }
    }

    private void ActivateCanvas()
    {
        _canva.SetActive(true);
        DialogueSystem.Instance.OnDialogueEnd -= ActivateCanvas; // Important pour �viter de le faire plusieurs fois
    }
}
