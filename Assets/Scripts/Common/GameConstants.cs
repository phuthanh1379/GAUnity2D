using UnityEngine;

namespace Common
{
    /// <summary>
    /// Class to store essential constants
    /// </summary>
    public static class GameConstants
    {
        // Animator parameters
        public static readonly int VelocityX = Animator.StringToHash("VelocityX");
        public static readonly int VelocityY = Animator.StringToHash("VelocityY");
        public static readonly int Grounded = Animator.StringToHash("Grounded");
        public static readonly int Hurt = Animator.StringToHash("Hurt");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Attack = Animator.StringToHash("Attack");
        
        // Scenes
        public static readonly string SceneMenu = "MenuScene";
        public static readonly string SceneMainGame = "MainGameScene";
    }
}