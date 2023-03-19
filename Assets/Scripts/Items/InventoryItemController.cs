using Items;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script hiển thị UI cho item trong inventory
/// </summary>
public class InventoryItemController : MonoBehaviour
{
    public TMP_Text nameLabel;
    public TMP_Text quantityLabel;
    public GameObject quantityBG;
    public Image image;

    private Item _itemData;
    
    public void SetItemData(Item data, int quantity)
    {
        _itemData = data;
        nameLabel.text = data.itemName;

        if (quantity <= 1)
            quantityBG.SetActive(false);
        else
        {
            quantityBG.SetActive(true);
            quantityLabel.text = $"{quantity}";
        }

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
