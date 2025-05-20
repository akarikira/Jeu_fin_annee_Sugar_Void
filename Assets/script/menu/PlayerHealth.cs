using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Autres objets à détruire à la mort")]
    public GameObject secondCharacter; // 👈 Ton autre perso à détruire

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage! Current HP: " + currentHealth);

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player is dead!");
        Destroy(gameObject); // Détruit le joueur

        if (secondCharacter != null)
        {
            Destroy(secondCharacter); // 👈 Détruit l’autre personnage aussi
        }
    }

    private System.Collections.IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
