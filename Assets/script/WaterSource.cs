using UnityEngine;

public class WaterSource : MonoBehaviour
{
    public string successMessage = "Tu as rempli la fiole.";
    public string failMessage = "Tu n’as pas de fiole vide.";

    private bool playerInRange = false;
    private bool alreadyUsed = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !alreadyUsed)
        {
            if (PlayerInventory.hasVial)
            {
                MessageDisplay.Instance.ShowMessage(successMessage);
                alreadyUsed = true;

                // Ici, tu peux activer une animation, changer l’état de la fiole, etc.
            }
            else
            {
                MessageDisplay.Instance.ShowMessage(failMessage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
