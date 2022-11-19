using DG.Tweening;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text scoreLabel;
    
    private int _point;

    private System.Random _rnd;
    private Sequence _sequence;

    private void Awake()
    {
        _rnd = new System.Random();
        _point = _rnd.Next(-10, 10);

        scoreLabel.text = _point <= 0 ? $"{_point}" : $"+{_point}";

        if (_point < 0)
        {
            spriteRenderer.color = Color.red;
            scoreLabel.color = Color.red;
        }

        scoreLabel.DOFade(0f, 0f);

        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOMoveY(4.5f, 0.5f))
            .Join(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f))
            // .Append(transform.DOMoveY(3.5f, 0.5f))
            .SetEase(Ease.OutQuad)
            .SetLoops(-1)
            .SetAutoKill(false)
            .Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.GetComponent<PlayerProfile>().Scoring(_point);

        _sequence.Pause();
        scoreLabel.gameObject.SetActive(true);

        var seq = DOTween.Sequence();
        seq
            .Append(spriteRenderer
                .DOFade(0f, 0.5f)
                .SetEase(Ease.OutQuad))
            .Join(scoreLabel
                .DOFade(1f, 0.5f)
                .SetEase(Ease.OutQuad))
            .Join(scoreLabel.transform
                    .DOMoveY(0.3f, 0.5f)
                    .SetEase(Ease.OutQuad))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            })
            .Play()
            ;
        
    }
}
