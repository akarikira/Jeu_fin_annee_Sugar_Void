using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    private Vector2 attackDirection;
    private int damage;

    public void Initialize(Vector2 direction, int dmg)
    {
        attackDirection = direction.normalized;
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Vérifier si l'ennemi est dans la bonne direction
            Vector2 toEnemy = (collision.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(attackDirection, toEnemy);

            if (dot > 0.5f) // L'ennemi est dans la bonne direction (ajuste ce seuil au besoin)
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
