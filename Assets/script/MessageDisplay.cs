using UnityEngine;
using TMPro;

public class MessageDisplay : MonoBehaviour
{
    public static MessageDisplay Instance;

    public GameObject panel;
    public TMP_Text messageText;
    public float displayDuration = 3f;

    private float timer;
    private bool isShowing = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        panel.SetActive(false);
    }

    void Update()
    {
        if (isShowing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                HideMessage();
            }
        }
    }

    public void ShowMessage(string message)
    {
        panel.SetActive(true);
        messageText.text = message;
        timer = displayDuration;
        isShowing = true;
    }

    public void HideMessage()
    {
        panel.SetActive(false);
        isShowing = false;
    }
}
