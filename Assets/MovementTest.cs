using UnityEngine;

public class MovementTest : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 baseScale;
    private float _horizontal;
    private float _vertical;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Flip();
    }

    private void FixedUpdate()
    {
        transform.position += 
            new Vector3(_horizontal * speed * Time.fixedDeltaTime, 
            _vertical * speed * Time.fixedDeltaTime, 
            0f);
    }

    private void Flip()
    {
        switch(_horizontal)
        {
            case > 0:
                transform.localScale = baseScale;
                break;
            case < 0:
                var newX = -1f * baseScale.x;
                transform.localScale = new Vector3(newX, baseScale.y, baseScale.z);
                break;
        }
    }
}
