using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private AudioClip[] stepSounds; // 🎵 Deux sons de pas
    [SerializeField] private float stepInterval = 0.4f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private AudioSource audioSource;

    public List<Vector2> LatestPath = new List<Vector2>();

    private float stepTimer = 0f;
    private int stepIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.OnDialogueStart += DisableMovement;
            DialogueSystem.Instance.OnDialogueEnd += EnableMovement;
        }
    }

    void OnDestroy()
    {
        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.OnDialogueStart -= DisableMovement;
            DialogueSystem.Instance.OnDialogueEnd -= EnableMovement;
        }
    }

    void Update()
    {
        UpdateAnimation();
        HandleFootsteps();

        if (LatestPath.Count > 0)
        {
            Vector2 previousPosition = LatestPath[LatestPath.Count - 1];
            if (Vector2.Distance(previousPosition, transform.position) >= 0.01f)
                LatestPath.Add(transform.position);
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

    private void HandleFootsteps()
    {
        bool isMoving = moveInput.magnitude > 0.1f;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayStepSound();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    private void PlayStepSound()
    {
        if (stepSounds.Length == 0 || audioSource == null) return;

        audioSource.PlayOneShot(stepSounds[stepIndex]);
        stepIndex = (stepIndex + 1) % stepSounds.Length;
    }

    private void DisableMovement()
    {
        moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        enabled = false;
    }

    private void EnableMovement()
    {
        enabled = true;
    }
}
