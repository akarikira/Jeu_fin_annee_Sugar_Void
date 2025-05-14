using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    public List<Vector2> LatestPath=new List<Vector2>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimation();

        //Si ya deja qlq chose dans la liste on verifie si on a bougé depuis si on a bougé on ajoute notre pos actuelle dans la liste

        if (LatestPath.Count > 0)
        {
            Vector2 previousPosition = LatestPath[LatestPath.Count - 1];
            float distanceToLatestPoint = Vector2.Distance(previousPosition, transform.position);
            if (distanceToLatestPoint >= 0.01f)
            {
                LatestPath.Add(transform.position);
            }
        }
        else
        {
            LatestPath.Add (transform.position);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        // Bloquer les diagonales : ne permettre qu'un seul axe à la fois
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            input.y = 0;
            input.x = Mathf.Sign(input.x);
        }
        else if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
        {
            input.x = 0;
            input.y = Mathf.Sign(input.y) * 0.85f; // <-- Ralentissement léger sur l'axe Y
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
}
