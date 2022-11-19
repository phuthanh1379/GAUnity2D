using UnityEngine;

namespace Common
{
    /// <summary>
    /// Class to store essential constants
    /// </summary>
    public static class GameConstants
    {
        // Animator parameters
        public static readonly int VelocityX = Animator.StringToHash("velocityX");
        public static readonly int VelocityY = Animator.StringToHash("velocityY");
        public static readonly int Grounded = Animator.StringToHash("grounded");
        public static readonly int Hurt = Animator.StringToHash("hurt");
    }
}