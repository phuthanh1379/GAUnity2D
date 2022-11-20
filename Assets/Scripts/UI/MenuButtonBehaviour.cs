using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class MenuButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Sequence _pointerEnterSequence;

        private void Awake()
        {
            _pointerEnterSequence = DOTween.Sequence();
            _pointerEnterSequence
                .Append(transform.DOScale(Vector3.one * 1.1f, 0.5f))
                .Append(transform.DOScale(Vector3.one, 0.5f))
                .SetLoops(-1)
                .SetEase(Ease.InSine)
                .OnPause(() =>
                {
                    transform.DOScale(Vector3.one, 0f).Play();
                });
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _pointerEnterSequence.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _pointerEnterSequence.Pause();
        }
    }
}
