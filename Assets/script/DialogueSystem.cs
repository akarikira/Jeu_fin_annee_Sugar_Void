using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public delegate void DialogueEvent();
    public event DialogueEvent OnDialogueStart;
    public event DialogueEvent OnDialogueEnd;

    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(3, 5)] public string text;
        public Sprite speakerSprite;
        public AudioClip soundEffect;
    }

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image speakerImage;

    private List<DialogueLine> currentLines;
    private int currentIndex = 0;
    public bool IsDialogueActive { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            dialoguePanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(List<DialogueLine> lines)
    {
        if (IsDialogueActive) return;

        currentLines = lines;
        currentIndex = 0;
        IsDialogueActive = true;
        dialoguePanel.SetActive(true);
        OnDialogueStart?.Invoke();
        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        DialogueLine line = currentLines[currentIndex];
        dialogueText.text = line.text;

        if (speakerImage != null)
        {
            speakerImage.sprite = line.speakerSprite;
            speakerImage.gameObject.SetActive(line.speakerSprite != null);
        }

        if (line.soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(line.soundEffect, Camera.main.transform.position);
        }
    }

    public void NextLine()
    {
        currentIndex++;
        if (currentIndex >= currentLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        IsDialogueActive = false;
        OnDialogueEnd?.Invoke();
    }

    public void OnDialogueNext(InputAction.CallbackContext context)
    {
        if (IsDialogueActive && context.performed)
        {
            NextLine();
        }
    }
}