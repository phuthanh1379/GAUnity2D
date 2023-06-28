using UnityEngine;
using Managers;

namespace BringerOfDeath
{
    public class SpellBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void CastSpell()
        {
            animator.Play("Spell");
            AudioManager.Instance.PlayAudioClip("RockBreak");
        }

        public void EndOfSpell()
        {
            Destroy(gameObject);
        }
    }
}