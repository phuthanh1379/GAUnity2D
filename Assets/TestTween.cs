using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTween : MonoBehaviour
{
    [SerializeField] private Transform target1;

    private float d = 1f;
    private Ease e = Ease.OutQuint;

    private void Awake()
    {
        var sequence = DOTween.Sequence();

        var move1 = transform.DOMove(target1.position, d);

        sequence
            .Append(move1)
            .OnComplete(() => gameObject.SetActive(false))
            .SetAutoKill(false)
            .SetEase(e)
            .SetLoops(-1, LoopType.Restart)
            .Play();
    }
}
