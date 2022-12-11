using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public TMP_Text label;
    public Image image;

    public void SetItemData(string itemName, Sprite sprite)
    {
        label.text = itemName;
        image.sprite = sprite;
    }
}
