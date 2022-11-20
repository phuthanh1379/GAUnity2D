using UnityEngine;

namespace GameLogic
{
    public class BackgroundParallax : MonoBehaviour
    {
        [SerializeField] private float leftBorder;
        [SerializeField] private float rightBorder;
        [SerializeField] private float speed;
    
        private void Update()
        {
            if (transform.position.x <= leftBorder)
            {
                transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
            }
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.left * (Time.fixedDeltaTime * speed));
        }
    }
}
