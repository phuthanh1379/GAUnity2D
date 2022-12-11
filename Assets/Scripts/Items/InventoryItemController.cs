using System.Collections;
using System.Collections.Generic;
using Items;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public TMP_Text label;
    public Image image;
    private Item _itemData;
    
    public void SetItemData(Item data)
    {
        _itemData = data;
        label.text = data.itemName;
        image.sprite = data.itemSprite;
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(_itemData);
        Destroy(gameObject);
    }

    public void DisplayItem()
    {
        ItemShowcase.Instance.DisplayItemData(_itemData);
    }
}
