using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    public List<Vector2> LatestPath = new List<Vector2>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // S'abonner aux événements de dialogue
        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.OnDialogueStart += DisableMovement;
            DialogueSystem.Instance.OnDialogueEnd += EnableMovement;
        }
    }

    void OnDestroy()
    {
        // Se désabonner pour éviter les memory leaks
        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.OnDialogueStart -= DisableMovement;
            DialogueSystem.Instance.OnDialogueEnd -= EnableMovement;
        }
    }

    void Update()
    {
        UpdateAnimation();

        if (LatestPath.Count > 0)
        {
            Vector2 previousPosition = LatestPath[LatestPath.Count - 1];
            if (Vector2.Distance(previousPosition, transform.position) >= 0.01f)
            {
                LatestPath.Add(transform.position);
            }
        }
        else
        {
            LatestPath.Add(transform.position);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(InputValue value)
    {
        if (DialogueSystem.Instance != null && DialogueSystem.Instance.IsDialogueActive)
            return;

        Vector2 input = value.Get<Vector2>();

        // Bloquer les diagonales
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            input.y = 0;
            input.x = Mathf.Sign(input.x);
        }
        else if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
        {
            input.x = 0;
            input.y = Mathf.Sign(input.y) * 0.85f;
        }
        else
        {
            input = Vector2.zero;
        }

        moveInput = input;
    }

    private void UpdateAnimation()
    {
        bool isMoving = moveInput.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving);

        if (isMoving)
        {
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
    }

    private void DisableMovement()
    {
        moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        enabled = false; // Désactive complètement le script
    }

    private void EnableMovement()
    {
        enabled = true;
    }
}