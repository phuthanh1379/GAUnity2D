using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
    
    private void Update()
    {
        // var currentVelocity = Vector3.zero;
        // transform.position = Vector3.SmoothDamp(
        //     transform.position, 
        //     target.position, 
        //     ref currentVelocity, 
        //     speed);
        //
        // transform.position = Vector3.Lerp(transform.position, target.position, speed);
    }
}
