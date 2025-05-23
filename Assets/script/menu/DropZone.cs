using UnityEngine;

public class DropZone : MonoBehaviour
{
    public enum RequiredItem { Couteau, Fiole }
    public RequiredItem requiredItem;

    public string successMessage = "Objet déposé.";
    public string failMessage = "Tu n'as pas l'objet nécessaire.";

    private bool playerInRange;
    public PlayerInventory PlayerInventory;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (HasRequiredItem())
            {
                RemoveItemFromInventory();
                MessageDisplay.Instance.ShowMessage(successMessage);
                gameObject.SetActive(false); // ou déclenche autre chose
            }
            else
            {
                MessageDisplay.Instance.ShowMessage(failMessage);
            }
        }
    }

    bool HasRequiredItem()
    {
        if (requiredItem == RequiredItem.Couteau)
            return PlayerInventory.hasKnife;
        if (requiredItem == RequiredItem.Fiole)
            return PlayerInventory.hasFiole;

        return false;
    }

    void RemoveItemFromInventory()
    {
        if (requiredItem == RequiredItem.Couteau)
            PlayerInventory.hasKnife = false;
        if (requiredItem == RequiredItem.Fiole)
            PlayerInventory.hasFiole = false;
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
