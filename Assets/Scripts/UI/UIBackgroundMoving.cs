using UnityEngine;

namespace UI
{
    public class UIBackgroundMoving : MonoBehaviour
    {
        [SerializeField] private float startValue;
        [SerializeField] private float endValue;
        [SerializeField] private float speed;

        private void Update()
        {
            if (transform.localPosition.x <= endValue)
            {
                transform.localPosition = new Vector3(startValue, 0f, 0f);
            }
        }

        private void FixedUpdate()
        {
            transform
                .Translate(new Vector3(-1f, 0f, 0f) * (Time.fixedDeltaTime * speed));
        }
    }
} 
