using UnityEngine;
using UnityEngine.UI;
using System;
using Common;

public class Enemy : MonoBehaviour
{
    private int _hitPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private Slider healthBar;

    [Header("Detect")]
    [SerializeField] private Transform detectPoint;
    [SerializeField] private float detectRadius;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;
    private bool _isAttackReady = true;

    public event Action EnemyDead;
    
    #region Unity Methods
    
    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

        if (detectPoint == null) return;
        Gizmos.DrawWireSphere(detectPoint.position, detectRadius);
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (DetectPlayer())
            Attack();
    }
    
    #endregion

    #region Action

    private void Init()
    {
        // Health settings
        _hitPoint = 100;
        healthBar.maxValue = _hitPoint;
        healthBar.value = _hitPoint;
        
        // Set animation
        animator.SetInteger(GameConstants.AnimState, (int) BanditAnimationState.Run);
    }
    
    private bool DetectPlayer()
    {
        return Physics2D.OverlapCircle
            (detectPoint.position, detectRadius, playerLayer);
    }
    
    private void Attack()
    {
        if (!_isAttackReady) return;
        animator.SetTrigger(GameConstants.Attack);
        _isAttackReady = false;
    }

    public void TakeDamage(int damageReceived)
    {
        animator.SetTrigger(GameConstants.Hurt);
        _hitPoint -= damageReceived;
        healthBar.value -= damageReceived;
        
        if (_hitPoint <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemyDead?.Invoke();
        animator.SetTrigger(GameConstants.Death);
        collider2d.enabled = false;
        enabled = false;
    }
    
    #endregion

    #region Events

    private void HitPlayer()
    {
        var hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        hitPlayer?.GetComponent<PlayerProfile>().PlayerHurt(damage);
    }

    private void AttackReady()
    {
        _isAttackReady = true;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    #endregion
}
