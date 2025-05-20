using System.Collections;
using UnityEngine;

public class FlashAttack : MonoBehaviour
{
    public GameObject flashPrefab;
    public float attackRange = 5f;
    public int damage = 1;
    public float attackDuration = 0.2f;
    public float flashOffset = 0.5f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PerformFlashAttack());
        }
    }

    IEnumerator PerformFlashAttack()
    {
        // Récupérer la direction depuis l'Animator
        float dirX = animator.GetFloat("LastInputX");
        float dirY = animator.GetFloat("LastInputY");
        Vector2 direction = new Vector2(dirX, dirY).normalized;

        if (direction == Vector2.zero)
        {
            direction = Vector2.up; // Par défaut vers le haut
        }

        Vector3 flashPosition = transform.position + (Vector3)(direction * flashOffset);
        GameObject flash = Instantiate(flashPrefab, flashPosition, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        flash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Collider2D flashCollider = flash.GetComponent<Collider2D>();
        flashCollider.enabled = false;

        FlashDamage flashDamage = flash.GetComponent<FlashDamage>();
        if (flashDamage != null)
        {
            flashDamage.Initialize(direction, damage);
        }

        yield return new WaitForSeconds(0.1f);
        flashCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);
        Destroy(flash);
    }
}
