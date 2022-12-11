using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        public Transform itemContent;

        public List<Item> items = new();
        public GameObject inventoryItem;
        
        private void Awake()
        {
            Instance = this;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            DisplayItems();
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void DisplayItems()
        {
            // Clear
            foreach (Transform child in itemContent)
                Destroy(child.gameObject);
            
            foreach (var item in items)
            {
                var i = Instantiate(inventoryItem, itemContent);
                i.GetComponent<InventoryItemController>().SetItemData(item.itemName, item.itemSprite);
            }
        }
    }
}