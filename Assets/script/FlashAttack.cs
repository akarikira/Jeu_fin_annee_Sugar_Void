using System.Collections;
using UnityEngine;

public class FlashAttack : MonoBehaviour
{
    public GameObject flashPrefab;  // Le prefab du flash
    public float attackRange = 5f;  // La port�e du flash
    public int damage = 1;  // Les d�g�ts du flash
    public float attackDuration = 0.2f;  // Dur�e de l'attaque
    public float flashOffset = 0.5f;  // D�calage du flash par rapport au joueur

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Le joueur appuie sur 'E' pour attaquer
        {
            StartCoroutine(PerformFlashAttack());
        }
    }

    IEnumerator PerformFlashAttack()
    {
        // Calculer la direction du flash vers le c�t� o� regarde le joueur
        Vector3 direction = transform.up.normalized; // Le "up" du joueur d�finit la direction du regard (on utilise l'axe Y en 2D)

        // Cr�er le flash � une position d�cal�e par rapport au joueur
        Vector3 flashPosition = transform.position + direction * flashOffset;
        GameObject flash = Instantiate(flashPrefab, flashPosition, Quaternion.identity);

        // Calculer l'angle de rotation bas� sur la direction du joueur
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Appliquer la rotation du flash pour l'orienter dans la m�me direction que le joueur
        flash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // D�sactiver temporairement le collider du flash pour �viter une d�tection imm�diate
        Collider2D flashCollider = flash.GetComponent<Collider2D>();
        flashCollider.enabled = false;

        // V�rifie les ennemis dans la port�e et inflige des d�g�ts
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        // Activer le collider apr�s un court d�lai pour permettre au flash de se d�placer
        yield return new WaitForSeconds(0.1f); // D�lai avant d'activer le collider

        flashCollider.enabled = true;

        // Attendre la dur�e de l'attaque avant de d�truire le flash
        yield return new WaitForSeconds(attackDuration);

        // D�truire le flash apr�s l'attaque
        Destroy(flash);
    }
}
