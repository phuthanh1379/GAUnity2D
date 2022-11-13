using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    private int _point;

    private System.Random _rnd;
    private Sequence _sequence;

    private void Awake()
    {
        _rnd = new System.Random();
        _point = _rnd.Next(-10, 10);

        if (_point < 0)
            spriteRenderer.color = Color.red;

        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOMoveY(4.5f, 0.5f))
            .Join(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f))
            // .Append(transform.DOMoveY(3.5f, 0.5f))
            .SetLoops(-1)
            .SetAutoKill(false)
            .Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerProfile>().Scoring(_point);
            Destroy(this.gameObject);
        }
    }
}
