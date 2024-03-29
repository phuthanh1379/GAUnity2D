using Common;
using UnityEngine;

/// <summary>
/// Script to control player's movement and animations
/// </summary>
public class PlayerMoveController : MonoBehaviour
{
    #region Variables

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;
    [Tooltip("Pseudo transform to act as player's point of contact with ground layer")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    [Header("Movement settings")] 
    [Tooltip("Player's movement speed")] 
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpPower = 16f;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private Vector3 baseScale;

    [Header("Wall Sliding")] 
    [SerializeField] private Transform frontCheck;
    [SerializeField] private float wallSlidingSpeed;
    private bool _isTouchingFront;
    private bool _isWallSliding;

    [Header("Wall Jumping")] 
    [SerializeField] private float xWallForce;
    [SerializeField] private float yWallForce;
    [SerializeField] private float wallJumpTime;
    private bool _isWallJumping;

    // Private
    private float _horizontal; // horizontal movement input
    private bool _isMovable;
    private static readonly Vector3 InitialPosition = new(0.5f, 5f, 1f); // player's initial position

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        ConversationManager.Instance.ConversationStart += OnConversationStart;
        ConversationManager.Instance.ConversationEnd += OnConversationEnd;
    }

    private void OnDisable()
    {
        ConversationManager.Instance.ConversationStart -= OnConversationStart;
        ConversationManager.Instance.ConversationEnd -= OnConversationEnd;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(frontCheck.position, checkRadius);
    }

    private void Update()
    {
        // If player is not movable, do not run the rest
        if (!_isMovable) return;
        
        // Get horizontal movement input
        _horizontal = Input.GetAxisRaw("Horizontal");

        // If player is on the ground and press jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
            animator.SetTrigger(GameConstants.Jump);
        }

        // If player releases jump button while jumping, lower the jump height
        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            animator.SetTrigger(GameConstants.Jump);
        }
        
        // Wall sliding
        _isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundLayer);
        if (_isTouchingFront && !IsGrounded() && _horizontal != 0)
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }

        if (Input.GetButtonDown("Jump") && _isWallSliding)
        {
            _isWallJumping = true;
            Invoke(nameof(SetWallJumpingToFalse), wallJumpTime);
        }

        if (_isWallJumping)
        {
            rb2d.velocity = new Vector2(xWallForce * -_horizontal, yWallForce);
        }

        // Set animation
        SetAnimation(_horizontal);
        
        // Flip player
        Flip();
    }

    private void FixedUpdate()
    {
        // Update player's velocity
        rb2d.velocity = new Vector2(_horizontal * speed, rb2d.velocity.y);
        
        if (_isWallSliding)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,
                Mathf.Clamp(rb2d.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initialize the controller
    /// </summary>
    public void Init()
    {
        _isMovable = true;
        transform.position = InitialPosition;

        // foreach (var param in animator.parameters)
        // {
        //     Debug.Log(param.name);
        // }
    }

    private void SetWallJumpingToFalse()
    {
        _isWallJumping = false;
    }
    
    /// <summary>
    /// Set if player can move or not
    /// </summary>
    /// <param name="isMovable"></param>
    public void SetMovable(bool isMovable)
    {
        _isMovable = isMovable;
    }

    /// <summary>
    /// Flip player to correct direction
    /// </summary>
    private void Flip()
    {
        switch (_horizontal)
        {
            // Turn right
            case > 0:
                transform.localScale = baseScale;
                break;
            // Turn left
            case < 0:
                transform.localScale = new Vector3(-1 * baseScale.x, baseScale.y, baseScale.z);
                break;
        }
    }

    /// <summary>
    /// Check if player is grounded (on the ground)
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
    
    /// <summary>
    /// Set movement animations (run, jump) for player
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void SetAnimation(float x)
    {
        if (x > 0.000001 || x < -0.000001) 
            animator.SetInteger(GameConstants.AnimState, (int) BanditAnimationState.Run);
        else
            animator.SetInteger(GameConstants.AnimState, (int) BanditAnimationState.Idle);
        
        //if (y > 0)
        //    animator.SetTrigger(GameConstants.Jump);
        
        animator.SetBool(GameConstants.Grounded, IsGrounded());
    }

    #endregion

    #region Events

    private void OnConversationStart(ConversationData data)
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        // Disable player's movement
        SetMovable(false);

        animator.SetInteger(GameConstants.AnimState, (int)BanditAnimationState.Idle);
    }

    private void OnConversationEnd()
    {
        SetMovable(true);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
        
    #endregion
}
