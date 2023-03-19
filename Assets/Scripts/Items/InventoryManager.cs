using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        public Transform itemContent;

        //public List<Item> items = new();
        public Dictionary<Item, int> items = new();
        public GameObject inventoryItem;
        
        private void Awake()
        {
            // ----- SINGLETON -----
            // If there's an instance already and it's this, delete this
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void AddItem(Item item)
        {
            //items.Add(item);
            if (items.ContainsKey(item))
            {
                items[item] += 1;
            }
            else
            {
                items.Add(item, 1);
            }

            DisplayItems();
        }

        public void RemoveItem(Item item)
        {
            //items.Remove(item);
            if (!items.ContainsKey(item))
                return;

            if (items[item] > 1)
            {
                items[item] -= 1;
            }
            else
                items.Remove(item);
        }

        public void DisplayItems()
        {
            // Clear
            foreach (Transform child in itemContent)
                Destroy(child.gameObject);

            // Hiển thị tất cả item trong danh sách đã lưu
            foreach (var item in items)
            {
                var i = Instantiate(inventoryItem, itemContent);
                i.GetComponent<InventoryItemController>().SetItemData(item.Key, item.Value);
            }
        }
    }
}