using System.Collections;
using UnityEngine;

public class FlashAttack : MonoBehaviour
{
    public GameObject flashPrefab;  // Le prefab du flash
    public float attackRange = 5f;  // La portée du flash
    public int damage = 1;  // Les dégâts du flash
    public float attackDuration = 0.2f;  // Durée de l'attaque
    public float flashOffset = 0.5f;  // Décalage du flash par rapport au joueur

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Le joueur appuie sur 'E' pour attaquer
        {
            StartCoroutine(PerformFlashAttack());
        }
    }

    IEnumerator PerformFlashAttack()
    {
        // Calculer la direction du flash vers le côté où regarde le joueur
        Vector3 direction = transform.up.normalized; // Le "up" du joueur définit la direction du regard (on utilise l'axe Y en 2D)

        // Créer le flash à une position décalée par rapport au joueur
        Vector3 flashPosition = transform.position + direction * flashOffset;
        GameObject flash = Instantiate(flashPrefab, flashPosition, Quaternion.identity);

        // Calculer l'angle de rotation basé sur la direction du joueur
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Appliquer la rotation du flash pour l'orienter dans la même direction que le joueur
        flash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Désactiver temporairement le collider du flash pour éviter une détection immédiate
        Collider2D flashCollider = flash.GetComponent<Collider2D>();
        flashCollider.enabled = false;

        // Vérifie les ennemis dans la portée et inflige des dégâts
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        // Activer le collider après un court délai pour permettre au flash de se déplacer
        yield return new WaitForSeconds(0.1f); // Délai avant d'activer le collider

        flashCollider.enabled = true;

        // Attendre la durée de l'attaque avant de détruire le flash
        yield return new WaitForSeconds(attackDuration);

        // Détruire le flash après l'attaque
        Destroy(flash);
    }
}
