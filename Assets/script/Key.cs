using UnityEngine;

public class Key : MonoBehaviour
{
    private bool playerInRange = false;
    private GameObject player;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            PlayerInventory inventory = player?.GetComponent<PlayerInventory>();
            if (inventory == null) return;

            if (!inventory.hasKnife)
            {
                MessageDisplay.Instance.ShowMessage("❌ Tu as besoin du couteau pour prendre cette clé.");
                return;
            }

            Debug.Log("🗝️ Clé ramassée !");
            inventory.hasKey = true; // ← tu peux stocker ici ou dans un manager
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
}
