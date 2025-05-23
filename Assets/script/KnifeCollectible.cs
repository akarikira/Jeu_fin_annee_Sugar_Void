using UnityEngine;

public class KnifeCollectible : MonoBehaviour
{
    private bool playerInRange = false;
    private GameObject player;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            PlayerInventory inventory = player?.GetComponent<PlayerInventory>();
            if (inventory == null) return;

            Debug.Log("🔪 Couteau ramassé !");
            inventory.hasKnife = true;
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
