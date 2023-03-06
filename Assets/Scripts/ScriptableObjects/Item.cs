using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public string description;
        public int itemValue;
        public Sprite itemSprite;
    }
}