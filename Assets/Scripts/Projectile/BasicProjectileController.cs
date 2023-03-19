using UnityEngine;

public class BasicProjectileController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private GameObject projectileImpactFx;
    private int _damage;
    private float _timeToLive = 2f;

    private void Update()
    {
        // Countdown to self-destruction
        if (_timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        else
            _timeToLive -= Time.deltaTime;
    }

    public void Init(int damage, float speed, float direction)
    {
        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
        _damage = damage;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            // Tạo hiệu ứng
            Instantiate(projectileImpactFx, transform.position, transform.rotation);
            other.collider.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
