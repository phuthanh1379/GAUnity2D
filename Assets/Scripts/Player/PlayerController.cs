using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    
    public float runSpeed = 5f;
    private Vector3 playerMotion = Vector3.zero;

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        
        playerMotion.x = Input.GetAxisRaw("Horizontal") * runSpeed;
        playerMotion.y = Input.GetAxisRaw("Vertical") * runSpeed;
    }

    private void FixedUpdate()
    {
        controller.Move(playerMotion * Time.fixedDeltaTime);
    }
}
