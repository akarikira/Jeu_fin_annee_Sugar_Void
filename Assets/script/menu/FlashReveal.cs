using UnityEngine;

public class FlashReveal : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // assigné depuis l’Inspector ou récupéré automatiquement
    public Sprite revealedSprite;         // le sprite qui apparaît après le flash
    public GameObject revealedObject;     // un objet à activer (facultatif)
    private bool isRevealed = false;

    public RevealManager revealManager;   // à assigner dans l’inspector

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (revealedObject != null)
        {
            revealedObject.SetActive(false); // on cache l’objet tant qu’il n’est pas révélé
        }
    }

    public void Reveal()
    {
        if (isRevealed) return;

        isRevealed = true;

        if (revealedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = revealedSprite;
        }

        if (revealedObject != null)
        {
            revealedObject.SetActive(true);
        }

        // on prévient le RevealManager
        if (revealManager != null)
        {
            revealManager.ObjectRevealed();
        }
    }
}
