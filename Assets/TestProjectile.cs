using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    private Vector3 _rootPos;
    private float _t;

    public void Init(Vector3 rootPos, float t, float vx, float vy)
    {
        _rootPos = rootPos;
        _t = t;

        rb2d.velocity = new Vector2(vx, vy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var pos = _rootPos - transform.position;

            // Curve
            var vx = pos.x / _t;
            var vy = (pos.y - 0.5f * Physics.gravity.y * _t * _t) / _t;

            Debug.Log($"vx={vx}, vy={vy}");
            rb2d.velocity = new Vector2(vx, vy);
        }
    }
}
