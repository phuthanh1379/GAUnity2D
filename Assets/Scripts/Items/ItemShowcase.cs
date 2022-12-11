using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShowcase : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemValue;
    public TMP_Text itemDescription;
    public Image itemImage;

    public static ItemShowcase Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayItemData(Item item)
    {
        itemName.text = item.itemName;
        itemValue.text = item.itemValue.ToString();
        itemDescription.text = item.description;
        itemImage.sprite = item.itemSprite;
        gameObject.SetActive(true);
    }
}
