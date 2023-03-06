using UnityEngine;

public class MovementExample : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform frontCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpPower = 16f;
    [SerializeField] private float wallSlidingSpeed = 4f;
    [SerializeField] private float xWallJumpForce = 10f;
    [SerializeField] private float yWallJumpForce = 15f;
    [SerializeField] private float checkRadius = 0.2f;

    private Vector3 _baseScale;
    private float _horizontal;
    private bool _canDoubleJump = false;
    private bool _canWallSliding = false; // Can player slide wall

    private void Awake()
    {
        _baseScale = transform.localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(frontCheck.position, checkRadius);
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            // Jump from wall
            if (_canWallSliding)
            {
                JumpFromWall();
            }

            // If player is on the ground (1st jump)
            if (IsGrounded())
            {
                Jump();
                _canDoubleJump = true;
            }
            else
            {
                // 2nd jump
                if (_canDoubleJump)
                {
                    _canDoubleJump = false;
                    Jump();
                }
            }
        }

        // If player releases jump button while jumping, lower the jump height
        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
        }

        // Wall-sliding
        if (IsWalled() && !IsGrounded() && _horizontal != 0)
            _canWallSliding = true;
        else
            _canWallSliding = false;

        SetAnimation();
        Flip();
    }

    private void FixedUpdate()
    {
        // Move horizontally
        rb2d.velocity = new Vector2(_horizontal * speed, rb2d.velocity.y);

        // Slide wall
        if (_canWallSliding)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,
                Mathf.Clamp(rb2d.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundLayer);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void JumpFromWall()
    {
        rb2d.velocity = new Vector2(xWallJumpForce * -_horizontal, yWallJumpForce);
    }

    private void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
    }

    private void SetAnimation()
    {
        var movement = Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y);
        animator.SetFloat("movement", movement);
    }

    private void Flip()
    {
        switch (_horizontal)
        {
            case > 0:
                transform.localScale = _baseScale;
                break;
            case < 0:
                var newX = -1f * _baseScale.x;
                transform.localScale = new Vector3(newX, _baseScale.y, _baseScale.z);
                break;
        }
    }
}
