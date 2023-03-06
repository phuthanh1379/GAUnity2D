using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "GemData", menuName = "TowerDefense/GemData")]
    public class GemData : ScriptableObject
    {
        [SerializeField] private int id;
        public int ID => id;

        [SerializeField] private string gemName;
        public string GemName => gemName;

        [SerializeField] private List<Sprite> sprites = new();
        public List<Sprite> Sprites => sprites;
    }
}