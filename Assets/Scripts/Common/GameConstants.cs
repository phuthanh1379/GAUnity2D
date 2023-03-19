using UnityEngine;

namespace Common
{
    /// <summary>
    /// Class to store essential constants
    /// </summary>
    public static class GameConstants
    {
        // Animator parameters
        public static readonly int AnimState = Animator.StringToHash("AnimState"); // enum BanditAnimationState
        public static readonly int Grounded = Animator.StringToHash("Grounded"); // bool
        public static readonly int Attack = Animator.StringToHash("Attack"); // trigger
        public static readonly int Recover = Animator.StringToHash("Recover"); // trigger
        public static readonly int Jump = Animator.StringToHash("Jump"); // trigger
        public static readonly int Hurt = Animator.StringToHash("Hurt"); // trigger
        public static readonly int Death = Animator.StringToHash("Death"); // trigger
        public static readonly float AirSpeed = Animator.StringToHash("AirSpeed"); // float

        // Scenes
        public static readonly string SceneMenu = "MenuScene";
        public static readonly string SceneMainGame = "MainGameScene";

        // PlayerPrefs keys
        public static readonly string CurrentSceneIndex = "CurrentSceneIndex";
    }
}