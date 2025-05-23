using UnityEngine;
using TMPro;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem Instance;

    public GameObject messagePanel;
    public TMP_Text messageText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        messagePanel.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        messagePanel.SetActive(true);
        messageText.text = message;
    }

    public void HideMessage()
    {
        messagePanel.SetActive(false);
    }
}
