using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack")] 
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private int attackDamage;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float shootingRate;
    private float _currentCounter = 0f;
    
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
    }
    
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void AttackHit()
    {
        Debug.Log("Attack Hit");
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void AttackRange()
    {
        var p = Instantiate(projectile, attackPoint.position, attackPoint.rotation);
        p.GetComponent<BasicProjectileController>().Init(attackDamage, projectileSpeed, transform.localScale.x);
    }
}
