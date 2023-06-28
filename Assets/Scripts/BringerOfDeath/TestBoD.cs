using UnityEngine;
using Common;
using Managers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

namespace BringerOfDeath
{
    public class TestBoD : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float speed;

        [Header("Spell")]
        [SerializeField] private SpellBehaviour spellSpawn;
        private Vector3 SpellOffset = new Vector3(-2f, 1f, 0f);

        [Header("Dash")]
        [SerializeField] private GameObject afterImagePrefab;
        private List<GameObject> _afterImages = new();

        private Vector3 _baseScale = new();
        private float _horizontal;
        private int direction = 1; // 1 = left, -1 = right

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(spellSpawn.transform.position, 0.2f);
        }

        private void Awake()
        {
            _baseScale = transform.localScale;
        }

        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetMouseButtonDown(0))
                Attack();

            if (Input.GetKeyDown(KeyCode.E))
                CastSpell();

            if (Input.GetMouseButtonDown(1))
                Dash();

            Flip();
            SetAnimation();
        }

        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(_horizontal * speed, rb2d.velocity.y);
        }

        private void SetAnimation()
        {
            if (_horizontal != 0)
                animator.SetInteger(DeathBringerAnim.AnimState, 1);
            else
                animator.SetInteger(DeathBringerAnim.AnimState, 0);
        }

        private void Flip()
        {
            // Left
            if (_horizontal < 0)
            {
                transform.localScale = _baseScale;
                direction = 1;
            }
            // Right
            else if (_horizontal > 0)
            {
                transform.localScale = new Vector3(-_baseScale.x, _baseScale.y, _baseScale.z);
                direction = -1;
            }
        }

        private void Attack()
        {
            AudioManager.Instance.PlayAudioClip("Water");
            animator.SetTrigger(DeathBringerAnim.Attack);
        }

        private void Hurt()
        {
            animator.SetTrigger(DeathBringerAnim.Hurt);
        }
        private void Dead()
        {
            animator.SetTrigger(DeathBringerAnim.Dead);
        }

        private void CastSpell()
        {
            animator.SetTrigger(DeathBringerAnim.Cast);
        }

        private void SpawnSpell()
        {
            var pos = transform.position + new Vector3(direction * SpellOffset.x, SpellOffset.y, SpellOffset.z);
            var spell = Instantiate(spellSpawn, pos, Quaternion.identity);
            spell.CastSpell();
        }

        private void Dash()
        {
            transform.DOMoveX(direction * -1.5f + transform.position.x, 0.12f)
                .OnStart(() => StartCoroutine(SpawnAfterImages(0.12f, 6)))
                .OnComplete(() =>
                {
                    foreach (var ai in _afterImages)
                        Destroy(ai);

                    _afterImages.Clear();
                })
                .Play()
                ;
        }

        private IEnumerator SpawnAfterImages(float t, int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                //Debug.Log($"{transform.position.x}, {transform.position.y}, {transform.position.z}");
                var ai = Instantiate(afterImagePrefab, transform.position, Quaternion.identity);
                ai.transform.localScale = transform.localScale;
                //ai.transform.SetParent(transform);
                _afterImages.Add(ai);
                yield return new WaitForSeconds(t / quantity);
            }

            yield return null;
        }
    }
}