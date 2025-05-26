using UnityEngine;

public class DropZone : MonoBehaviour
{
    public enum RequiredItem { Couteau, Fiole }
    public RequiredItem requiredItem;

    public string successMessage = "Objet déposé !";
    public string failMessage = "Tu n’as pas l’objet requis.";

    private bool playerInRange = false;
    private bool hasDeposited = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !hasDeposited)
        {
            bool hasItem = false;

            switch (requiredItem)
            {
                case RequiredItem.Couteau:
                    hasItem = PlayerInventory.hasKnife;
                    if (hasItem) PlayerInventory.hasKnife = false;
                    break;
                case RequiredItem.Fiole:
                    hasItem = PlayerInventory.hasVial;
                    if (hasItem) PlayerInventory.hasVial = false;
                    break;
            }

            if (hasItem)
            {
                hasDeposited = true;
                MessageDisplay.Instance.ShowMessage(successMessage);
                // Tu peux aussi faire apparaître un effet ici
                DropManager.Instance.CheckAllDeposited();
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

    public bool IsDeposited => hasDeposited;
}
