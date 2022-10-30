using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyImageBehaviour : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    public Canvas canvas;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter!");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag!");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag!");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
