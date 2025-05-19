using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;  // Vie de l'ennemi
    private SpriteRenderer spriteRenderer;  // Référence au SpriteRenderer de l'ennemi
    private Color originalColor;  // Couleur originale de l'ennemi

    private void Start()
    {
        // Récupérer le SpriteRenderer de l'ennemi
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Sauvegarder la couleur d'origine de l'ennemi
        originalColor = spriteRenderer.color;
    }

    // Appelé lorsqu'un ennemi reçoit des dégâts
    public void TakeDamage(int damage)
    {
        health -= damage;
        // Afficher l'ennemi en rouge lorsqu'il prend des dégâts
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // L'ennemi meurt
        Destroy(gameObject);  // Détruire l'ennemi
    }

    // Coroutine pour faire clignoter l'ennemi en rouge lorsqu'il prend des dégâts
    private IEnumerator FlashRed()
    {
        // Changer la couleur de l'ennemi en rouge
        spriteRenderer.color = Color.red;

        // Attendre une fraction de seconde (par exemple, 0.1s)
        yield return new WaitForSeconds(0.1f);

        // Revenir à la couleur d'origine
        spriteRenderer.color = originalColor;
    }
}