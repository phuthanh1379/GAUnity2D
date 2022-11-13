using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;

    [Header("Movement settings")] 
    [Tooltip("Player's movement speed")] public float speed = 8f;
    public float jumpPower = 16f;
    
    // Private
    private bool _isGrounded; // is the player in the ground?
    private float _horizontal; // horizontal movement input
    private bool _isMovable;
    private static readonly Vector3 InitialPosition = new Vector3(-7f, 5f, 1f);

    public void Init()
    {
        _isMovable = true;
        transform.position = InitialPosition;
    }
    
    private void Update()
    {
        if (!_isMovable) return;
        
        // Get horizontal movement input
        _horizontal = Input.GetAxisRaw("Horizontal");

        // If player is on the ground and press jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
        }

        // If player releases jump button while jumping, lower the jump height
        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
        }
        
        // Set animation
        SetAnimation(rb2d.velocity.x, rb2d.velocity.y);
        
        // Flip player
        Flip();
    }

    private void FixedUpdate()
    {
        // Update player's velocity
        rb2d.velocity = new Vector2(_horizontal * speed, rb2d.velocity.y);
    }

    private void Flip()
    {
        // Turn right
        if (_horizontal > 0)
        {
            transform.localScale = Vector3.one;
        }

        // Turn left
        if (_horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void SetAnimation(float x, float y)
    {
        if (x < 0) x *= -1;
        animator.SetFloat("velocityX", x);
        animator.SetFloat("velocityY", y);
        animator.SetBool("grounded", IsGrounded());
    }

    public void SetMovable(bool isMovable)
    {
        _isMovable = isMovable;
    }

    public void PlayerHurt()
    {
        animator.SetBool("hurt", true);
        animator.SetBool("hurt", false);
    }
}
