using DG.Tweening;
using UnityEngine;

namespace GameLogic
{
    public class SimpleAIPatrol : MonoBehaviour
    {
        public float startX;
        public float endX;
        public float duration;

        private Sequence _sequence;
    
        private void Awake()
        {
            _sequence = DOTween.Sequence();

            _sequence
                .Append(
                    transform.DOLocalMoveX(endX, duration)
                        .OnComplete(() =>
                        {
                            transform.DOScaleX(-0.2f, 0f);
                        })
                )
                .Append(
                    transform.DOLocalMoveX(startX, duration)
                        .OnComplete(() =>
                        {
                            transform.DOScaleX(0.2f, 0f);
                        })
                )
                .SetLoops(-1)
                .Play()
                ;
        }
    }
}
