using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, speed));
    }
}
