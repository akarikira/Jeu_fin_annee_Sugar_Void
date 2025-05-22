using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float speed = 2f;
    public float followRange = 6f;
    public float contactDamageCooldown = 1f;

    private bool canDamagePlayer = true;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= followRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

            // Animation de déplacement
            animator.SetBool("isMoving", true);
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

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
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDamagePlayer)
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
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
