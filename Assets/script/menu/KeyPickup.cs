using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public int keyIndex; // L'index de la cle (0, 1 ou 2)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<KeyManager>().CollectKey(keyIndex);
            Destroy(gameObject);
        }
    }
}
