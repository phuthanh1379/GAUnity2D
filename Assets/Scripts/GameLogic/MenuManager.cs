using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_Text titleLabel;
    public TMP_Text titleLabel2;

    public GameObject popup;
    
    private Sequence _sequence;
    private Sequence _popupSequence;

    // public AudioSource buttonAudio1;
    
    private void Awake()
    {
        // _sequence = DOTween.Sequence();
        // _sequence
        //     .Append(
        //     titleLabel.transform.DOScale(Vector3.one * 1.2f, 0.5f)
        //         .SetEase(Ease.InQuad)
        // )
        //     .Join(
        //         titleLabel2.transform.DOScale(Vector3.one * 1.2f, 0.5f)
        //         )
        //     .Join(
        //         titleLabel.DOColor(Color.red, 0.5f)
        //         )
        //     .OnComplete(() =>
        //     {
        //         titleLabel.transform.DOScale(Vector3.one, 0f);
        //         Debug.Log("Done tween!");
        //     })
        //     // .SetLoops(-1)
        //     ;
        //
        // _sequence.Play();
        
        // popup.SetActive(false);
        //
        // _popupSequence = DOTween.Sequence();
        // _popupSequence
        //     .Append(
        //         popup.transform.DOScale(Vector3.one * 1.2f, 5f)
        //     )
        //     .Join(
        //         popup.GetComponent<Image>().DOFade(1f, 5f))
        //     .SetEase(Ease.OutQuad)
        //     .OnStart(() =>
        //     {
        //         popup.GetComponent<Image>().DOFade(0f, 0f);
        //         popup.transform.DOScale(Vector3.zero, 0f);
        //     })
        //     // .SetLoops(-1)
        //     ;
        
        _sequence = DOTween.Sequence();
        _sequence
            .Append(
                titleLabel.transform.DOLocalMoveX(455f, 0.5f)
            )
            // .OnComplete(() =>
            // {
            //     titleLabel.transform.DOMoveX(-455f, 0f);
            // })
            .SetLoops(-1)
            .Play()
            ;
    }

    private void OnCompleteTween()
    {
        
    }

    public void OnClickAbout()
    {
        // buttonAudio1.Play();
        
        popup.SetActive(true);
        _popupSequence.Play();
    }
}
