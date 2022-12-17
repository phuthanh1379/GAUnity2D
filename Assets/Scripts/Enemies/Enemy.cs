using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private int _hitPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider;
    [SerializeField] private Slider healthBar;
 
    private void Awake()
    {
        _hitPoint = 100;
        healthBar.maxValue = _hitPoint;
        healthBar.value = _hitPoint;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        _hitPoint -= damage;
        healthBar.value -= damage;
        
        if (_hitPoint <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        collider.enabled = false;
        this.enabled = false;
    }
}
