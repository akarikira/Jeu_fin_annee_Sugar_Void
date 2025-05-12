using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;  // Vie de l'ennemi
    private SpriteRenderer spriteRenderer;  // R�f�rence au SpriteRenderer de l'ennemi
    private Color originalColor;  // Couleur originale de l'ennemi

    private void Start()
    {
        // R�cup�rer le SpriteRenderer de l'ennemi
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Sauvegarder la couleur d'origine de l'ennemi
        originalColor = spriteRenderer.color;
    }

    // Appel� lorsqu'un ennemi re�oit des d�g�ts
    public void TakeDamage(int damage)
    {
        health -= damage;
        // Afficher l'ennemi en rouge lorsqu'il prend des d�g�ts
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // L'ennemi meurt
        Destroy(gameObject);  // D�truire l'ennemi
    }

    // Coroutine pour faire clignoter l'ennemi en rouge lorsqu'il prend des d�g�ts
    private IEnumerator FlashRed()
    {
        // Changer la couleur de l'ennemi en rouge
        spriteRenderer.color = Color.red;

        // Attendre une fraction de seconde (par exemple, 0.1s)
        yield return new WaitForSeconds(0.1f);

        // Revenir � la couleur d'origine
        spriteRenderer.color = originalColor;
    }
}
