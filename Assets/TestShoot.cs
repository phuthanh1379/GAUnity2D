using UnityEngine;

public class TestShoot : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TestProjectile projectile;
    [SerializeField] private float speed;

    private float rate = 0f;
    private const float rateThreshold = 2f;

    private void Update()
    {
        if (rate > 0f)
            rate -= Time.deltaTime;
        else
        {
            Shoot();
            rate = rateThreshold;
        }
    }

    private void Shoot()
    {
        var go = Instantiate(projectile, transform.position, transform.rotation);
        var pos = target.position - transform.position;

        // Curve
        var t = 2f;
        var vx = pos.x / t;
        var vy = (pos.y - 0.5f * Physics.gravity.y * t * t) / t;

        go.Init(transform.position, t, vx, vy);

        // Linear
        //go.velocity = new Vector2(pos.x, pos.y) * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
