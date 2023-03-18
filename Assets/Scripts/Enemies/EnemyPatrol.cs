using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour
{
    public float startX;
    public float endX;
    public float duration;

    private Sequence _sequence;

    public void StopPatrol()
    {
        _sequence.Kill();
    }

    private void Start()
    {
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform
                .DOMoveX(endX, duration)
                .OnComplete(Flip)
            )
            .Append(transform
                .DOMoveX(startX, duration)
                .OnComplete(Flip)
            )
            ;

        _sequence.SetLoops(-1)
            .Play();
    }

    private void Flip()
    {
        var scale = transform.localScale;
        transform.localScale = new Vector3(-scale.x, scale.y);
    }
}
