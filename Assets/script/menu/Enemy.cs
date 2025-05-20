using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;  // Vie de l'ennemi
    private SpriteRenderer spriteRenderer;  // Référence au SpriteRenderer de l'ennemi
    private Color originalColor;  // Couleur originale de l'ennemi

    public float contactDamageCooldown = 1f;  // Temps entre deux dégâts infligés au joueur
    private bool canDamagePlayer = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Appelé lorsqu'un ennemi reçoit des dégâts
    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);  // Détruire l'ennemi
    }

    // Coroutine pour faire clignoter l'ennemi en rouge lorsqu'il prend des dégâts
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    // Quand l'ennemi touche le joueur
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDamagePlayer)
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);  // Le joueur prend 1 dégât
                StartCoroutine(ContactDamageCooldown());
            }
        }
    }

    private IEnumerator ContactDamageCooldown()
    {
        canDamagePlayer = false;
        yield return new WaitForSeconds(contactDamageCooldown);
        canDamagePlayer = true;
    }
}
