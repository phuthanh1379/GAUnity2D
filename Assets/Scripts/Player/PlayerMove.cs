using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 0.5f;

    private Vector3 _playerMotion = Vector3.zero;
    public Vector3 baseScale;
    public Transform body;

    private void Update()
    {
        // Turn right
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            body.localScale = baseScale;
        }
        
        // Turn left
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            body.localScale = new Vector3(-1 * baseScale.x, baseScale.y, baseScale.z);
        }
        
        _playerMotion.x = Input.GetAxisRaw("Horizontal") * speed;
        _playerMotion.y = Input.GetAxisRaw("Vertical") * speed;
    }

    private void FixedUpdate()
    {
        controller.Move(_playerMotion * Time.fixedDeltaTime);
    }
}
