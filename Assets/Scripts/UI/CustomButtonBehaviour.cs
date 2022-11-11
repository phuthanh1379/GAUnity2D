using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    public event Action OnCustomMouseEnterEvent;
    public Action OnCustomMouseExitEvent;
    public ParticleSystem particleSystem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isHover", true);
        OnCustomMouseEnterEvent?.Invoke();
        particleSystem.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isHover", false);
        OnCustomMouseExitEvent?.Invoke();
        particleSystem.Stop();
    }

    public int ReturnInt(int a)
    {
        return a;
    }
}
