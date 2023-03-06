using UnityEngine;

public class ColliderExample : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"On collision with {collision.collider.name}");
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"On trigger with {collision.name}");
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
