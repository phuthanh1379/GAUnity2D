using Common;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack")] 
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private int attackDamage;
    private bool _isAttackable = true;
    private float _attackCooldown = 0f;
    private const float AttackCooldown = 0.5f;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float shootingRate;
    private float _currentCounter = 0f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;

    [Header("Special")]
    [SerializeField] private GameObject specialProjectile;
    private float currentHoldTime = 0f;
    private const float TimeToHold = 2f;

    #region Unity Methods
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Awake()
    {
        _currentCounter = shootingRate;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        _currentCounter -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_currentCounter <= 0)
            {
                AttackRange();
                _currentCounter = shootingRate;
            }
        }

        if (Input.GetKey(KeyCode.R))
            SpecialAttack(false);

        if (Input.GetKeyUp(KeyCode.R))
            SpecialAttack(true);

        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("True!");
        }

        if (!_isAttackable)
        {
            if (_attackCooldown <= 0f)
                AttackReady();
            else
                _attackCooldown -= Time.deltaTime;
        }
    }
    
    #endregion

    #region Actions

    private void SpecialAttack(bool isEnded)
    {
        if (isEnded)
        {
            if (currentHoldTime >= TimeToHold)
            {
                DoSpecialAttack();
                currentHoldTime = 0f;
            }
        }
        else
        {
            currentHoldTime += Time.deltaTime;
        }
        Debug.Log($"{currentHoldTime}");
    }
    
    private void DoSpecialAttack()
    {
        var p = Instantiate(specialProjectile, attackPoint.position, attackPoint.rotation);
        p.GetComponent<BasicProjectileController>()
            .Init(attackDamage * 3, projectileSpeed, transform.localScale.x);
    }

    private void Attack()
    {
        if (!_isAttackable) return;
        animator.SetTrigger(GameConstants.Attack);
        _isAttackable = false;
    }

    private void AttackRange()
    {
        var p = Instantiate(projectile, attackPoint.position, attackPoint.rotation);
        p.GetComponent<BasicProjectileController>().Init(attackDamage, projectileSpeed, transform.localScale.x);
    }
    
    #endregion

    #region Events

    private void AttackHit()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void AttackReady()
    {
        _isAttackable = true;
        _attackCooldown = AttackCooldown;
    }

    #endregion
 }
