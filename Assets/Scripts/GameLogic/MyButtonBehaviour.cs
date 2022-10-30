using System;
using UnityEngine;

public class MyButtonBehaviour : MonoBehaviour
{
    public event Action OnMyButtonClicked;
    public string color;

    public void OnClick()
    {
        OnMyButtonClicked?.Invoke();
        Destroy(gameObject);
    }
}
