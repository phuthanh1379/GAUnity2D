using System;
using DG.Tweening;
using Settings;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// Obstacle behaviour script
    /// </summary>
    public class Obstacle : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TMP_Text scoreLabel;
        
        // Event
        public event Action<int> ObstacleDestroy;
        
        public int id;
    
        // Settings for default movement
        private float _duration;
        private float _startY;
        private float _endY;
        private Ease _easeType;

        // Settings for scoring event
        private float _scoringDuration;
        private Ease _scoringEaseType;
    
        private int _point;

        private Sequence _sequence;
        private Sequence _scoringSequence;

        public void Init(ObstacleSettings settings)
        {
            // Read from settings
            id = settings.ID;
            _duration = settings.Duration;
            _startY = settings.StartY;
            _endY = settings.EndY;
            _easeType = settings.EaseType;

            _scoringDuration = settings.ScoringDuration;
            _scoringEaseType = settings.ScoringEaseType;

            _point = settings.Point;

            // Change label content according to point value
            scoreLabel.text = _point <= 0 ? $"{_point}" : $"+{_point}";

            // Change label color according to point value
            if (_point < 0)
            {
                spriteRenderer.color = Color.red;
                scoreLabel.color = Color.red;
            }

            // Set label's transparency
            scoreLabel.alpha = 0;

        
            // Sequence for default movement
            if (!settings.IsMovable) return;
            _sequence = DOTween.Sequence();
            _sequence
                .Append(transform.DOMoveY(_endY, _duration))
                .Append(transform.DOMoveY(_startY, _duration))
                .SetEase(_easeType)
                .SetLoops(-1)
                .SetAutoKill(false)
                .Play();

            // Scoring sequence
            _scoringSequence = DOTween.Sequence();
            _scoringSequence
                .Append(spriteRenderer.DOFade(0f, _scoringDuration))
                .Join(scoreLabel.DOFade(1f, _scoringDuration))
                .Join(scoreLabel.transform.DOLocalMoveY(1f, _scoringDuration))
                .SetEase(_scoringEaseType)
                .OnComplete(DestroyObstacle)
                ;
        }

        /// <summary>
        /// Check if collide with Player, if true then initiate scoring
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            other.GetComponent<PlayerProfile>().Scoring(_point);

            _sequence.Pause();
            scoreLabel.gameObject.SetActive(true);

            _scoringSequence.Play();
        }

        /// <summary>
        /// When scoring completes, destroy obstacle objects
        /// </summary>
        private void DestroyObstacle()
        {
            _sequence.Kill();
            _scoringSequence.Kill();
            ObstacleDestroy?.Invoke(id);
            // Destroy(gameObject);
        }
    }
}
