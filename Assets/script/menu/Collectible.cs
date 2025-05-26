using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemName = "Objet";
    public string successMessage = "Objet ramassé !";
    public bool destroyOnCollect = true;

    private bool playerInRange = false;
    public GameObject PORTE;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            MessageDisplay.Instance.ShowMessage(successMessage);
            Debug.Log($"{itemName} ramassé !");

            // Ajouter à l'inventaire
            if (itemName == "Clé") 
            { 
                PlayerInventory.hasKey = true;
                PORTE.SetActive(false);
            }
            if (itemName == "Couteau") PlayerInventory.hasKnife = true;
            if (itemName == "Fiole") PlayerInventory.hasVial = true;

            if (destroyOnCollect)
                Destroy(gameObject);
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
