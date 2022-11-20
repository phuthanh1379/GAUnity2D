using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [SerializeField] private int minPoint;
        public int MinPoint => minPoint;

        [SerializeField] private int maxPoint;
        public int MaxPoint => maxPoint;

        [SerializeField] private List<ObstacleSettings> obstacleSettingsList;
        public List<ObstacleSettings> ObstacleSettingsList => obstacleSettingsList;
    }
}
