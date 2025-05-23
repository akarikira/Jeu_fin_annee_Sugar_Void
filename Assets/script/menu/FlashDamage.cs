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
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
              enemy.TakeDamage(damage);
            }
            
        }

        FlashReveal revealable = collision.GetComponent<FlashReveal>();
       
        if (revealable != null)
       
        {
             revealable.Reveal();
        }

    }
}