using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SisterFollower : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private float followDistance = 1f;
    [SerializeField] private float followSpeed = 8f;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    private SpriteRenderer spriteRenderer;
    private Vector2 lastDirection = Vector2.down; 
    private bool isAligning = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color col = spriteRenderer.color;
        col.a = 1f;
        spriteRenderer.color = col;

        if (TryGetComponent<Collider2D>(out var col2D))
        {
            col2D.isTrigger = true;
            Physics2D.IgnoreCollision(col2D, player.GetComponent<Collider2D>());
        }

        Vector3 initialPos = player.position - (Vector3)(lastDirection * followDistance);
        initialPos.z = transform.position.z;
        transform.position = initialPos;
    }

    void Update()
    {
        UpdateDirection();
        AlignThenFollow();
        UpdateAnimation();
    }

    void UpdateDirection()
    {
        Vector2 input = new Vector2(
            Mathf.Round(Input.GetAxisRaw("Horizontal")),
            Mathf.Round(Input.GetAxisRaw("Vertical"))
        );

        if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
            lastDirection = input.normalized;
    }

    void AlignThenFollow()
    {
        Vector3 current = transform.position;
        Vector3 targetPos = player.position - (Vector3)(lastDirection * followDistance);
        targetPos.z = current.z;

        Vector3 newPosition = current;

        if (Mathf.Abs(current.x - targetPos.x) > 0.05f && Mathf.Abs(current.y - targetPos.y) > 0.05f)
        {
            // Alignement sur un seul axe d'abord
            if (Mathf.Abs(current.x - targetPos.x) > Mathf.Abs(current.y - targetPos.y))
            {
                // Bouge verticalement d'abord
                newPosition.y = Mathf.MoveTowards(current.y, targetPos.y, followSpeed * Time.deltaTime);
            }
            else
            {
                // Bouge horizontalement d'abord
                newPosition.x = Mathf.MoveTowards(current.x, targetPos.x, followSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Une fois alignée sur l’un des axes, avance vers la position cible
            if (Mathf.Abs(current.x - targetPos.x) > 0.05f)
                newPosition.x = Mathf.MoveTowards(current.x, targetPos.x, followSpeed * Time.deltaTime);
            if (Mathf.Abs(current.y - targetPos.y) > 0.05f)
                newPosition.y = Mathf.MoveTowards(current.y, targetPos.y, followSpeed * Time.deltaTime);
        }

        transform.position = newPosition;
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("MoveX", lastDirection.x);
        animator.SetFloat("MoveY", lastDirection.y);
        animator.SetBool("IsMoving", Vector3.Distance(transform.position, player.position) > 0.1f);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
#endif
}
