using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public GameObject panelGyaru;
    public GameObject panelGoth;

    public TMP_Text textGyaru;
    public TMP_Text textGoth;

    private DialogueLine[] dialogueLines;
    private int currentIndex = 0;
    private bool isDialogueActive = false;

    public bool IsDialogueActive => isDialogueActive;

    public event System.Action OnDialogueStart;
    public event System.Action OnDialogueEnd;

    void Awake()
    {
        Instance = this;
        panelGyaru.SetActive(false);
        panelGoth.SetActive(false);
    }

    void Update()
    {
        if (!isDialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            currentIndex++;
            if (currentIndex < dialogueLines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void ShowDialogue(DialogueLine[] lines)
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("DialogueSystem: dialogueLines est vide !");
            return;
        }

        dialogueLines = lines;
        currentIndex = 0;
        isDialogueActive = true;
        OnDialogueStart?.Invoke();
        ShowLine();
    }

    private void ShowLine()
    {
        string who = dialogueLines[currentIndex].characterName;
        string what = dialogueLines[currentIndex].text;

        if (who == "Gyaru")
        {
            panelGyaru.SetActive(true);
            panelGoth.SetActive(false);
            textGyaru.text = what;
        }
        else if (who == "Goth")
        {
            panelGyaru.SetActive(false);
            panelGoth.SetActive(true);
            textGoth.text = what;
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        panelGyaru.SetActive(false);
        panelGoth.SetActive(false);
        OnDialogueEnd?.Invoke();
    }
}
