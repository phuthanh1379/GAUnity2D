using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public string playerName;
        public int initialScore;
        public int initialHp;
        public int highScore;
    }
}
