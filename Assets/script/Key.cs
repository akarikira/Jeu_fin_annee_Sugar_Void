using UnityEngine;

public class Key : MonoBehaviour
{
    public DoorManager doorManager;  // Référence au script qui gère la porte
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🗝️ Clé ramassée !");
            if (doorManager != null)
            {
                doorManager.UseKey(); // Utilise la clé pour ouvrir la porte
            }

            Destroy(gameObject); // Détruit l'objet clé
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
