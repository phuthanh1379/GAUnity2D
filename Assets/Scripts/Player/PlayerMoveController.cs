using Common;
using DG.Tweening;
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
    
    // Private
    private float _horizontal; // horizontal movement input
    private bool _isMovable;
    private static readonly Vector3 InitialPosition = new(-7f, 5f, 1f); // player's initial position

    #endregion

    #region Unity Methods

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

    #endregion

    #region Methods

    /// <summary>
    /// Initialize the controller
    /// </summary>
    public void Init()
    {
        _isMovable = true;
        transform.position = InitialPosition;
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
    /// Animation to run when player is hurt
    /// </summary>
    public void PlayerHurt()
    {
        animator.SetBool(GameConstants.Hurt, true);
        var tween = transform.DOScale(transform.localScale * 1.1f, 0.5f);
        tween.OnComplete(() => animator.SetBool(GameConstants.Hurt, false));
    }

    /// <summary>
    /// Flip player to correct direction
    /// </summary>
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

    /// <summary>
    /// Check if player is grounded (on the ground)
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    /// <summary>
    /// Set movement animations (run, jump) for player
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void SetAnimation(float x, float y)
    {
        if (x < 0) x *= -1;
        animator.SetFloat(GameConstants.VelocityX, x);
        animator.SetFloat(GameConstants.VelocityY, y);
        animator.SetBool(GameConstants.Grounded, IsGrounded());
    }

    #endregion
}
