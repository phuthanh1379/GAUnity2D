using ScriptableObjects;
using UnityEngine;
using Items;

public class ItemController : MonoBehaviour
{
    public Item item;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        ItemPickup();
    }

    private void ItemPickup()
    {
        InventoryManager.Instance.AddItem(item);
        Destroy(gameObject);
    }
}
