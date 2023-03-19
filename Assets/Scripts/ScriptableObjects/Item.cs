using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public string description;
        public int itemValue;
        public Sprite itemSprite;

        // Override phương thức so sánh giữa 2 Item
        // Nếu ID khác nhau thì 2 Item khác nhau và ngược lại
        public override int GetHashCode()
        {
            return id;
        }
    }
}