using System;
using UnityEngine;

public class MyButtonBehaviour : MonoBehaviour
{
    public event Action OnMyButtonClicked;

    public void OnClick()
    {
        OnMyButtonClicked?.Invoke();
        Destroy(gameObject);
    }
}
