using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float contactDamageCooldown = 1f;
    private bool canDamagePlayer = true;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private NavMeshAgent agent;
    [SerializeField] Transform target;

    [Header(" Bloqueurs à désactiver à la mort")]
    public GameObject[] objectsToDisableOnDeath; // ← ✅ Ceci doit apparaître dans l’inspecteur

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
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
        // ✅ Désactive tous les objets listés dans l’inspecteur
        foreach (GameObject obj in objectsToDisableOnDeath)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

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
