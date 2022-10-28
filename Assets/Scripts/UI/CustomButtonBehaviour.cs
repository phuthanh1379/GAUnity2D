using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.enabled = false;
    }
}
