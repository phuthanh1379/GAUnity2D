using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotweenExample : MonoBehaviour
{
    [SerializeField] private Image popup;
    [SerializeField] private Ease easeType;

    private Sequence _sequence;

    private void Awake()
    {
        _sequence = DOTween.Sequence();
        //var _sequence2 = DOTween.Sequence();

        var duration = 1f;
        var fadeTween = popup.DOFade(1f, duration);
        var scaleTween = popup.transform.DOScale(Vector3.one, duration);

        _sequence
            .PrependCallback(() =>
            {
                Debug.Log("Run");
                popup.DOFade(0f, 0f);
                popup.transform.DOScale(Vector3.zero, 0f);
            })
            .AppendCallback(MyFunction)
            .Append(scaleTween)
            .SetEase(easeType)
            .Join(fadeTween)
            .SetAutoKill(false)
            ;
    }

    private void MyFunction()
    {
        // Do something
        Debug.Log("Do something");
    }

    public void OnclickButton()
    {
        _sequence.Restart();
    }
}
