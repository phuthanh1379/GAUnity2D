using System;
using DG.Tweening;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class ObstacleSettings
    {
        [SerializeField] private int id;
        public int ID => id;
        
        [SerializeField] private GameObject basePrefab;
        public GameObject BasePrefab => basePrefab;

        [SerializeField] private Vector3 basePosition;
        public Vector3 BasePosition => basePosition;

        // Default movement settings
        [SerializeField] private bool isMovable;
        public bool IsMovable => isMovable;
        
        [SerializeField] private float duration;
        public float Duration => duration;

        [SerializeField] private float startY;
        public float StartY => startY;

        [SerializeField] private float endY;
        public float EndY => endY;

        [SerializeField] private Ease easeType;
        public Ease EaseType => easeType;

        // Scoring action settings
        [SerializeField] private float scoringDuration;
        public float ScoringDuration => scoringDuration;

        [SerializeField] private Ease scoringEaseType;
        public Ease ScoringEaseType => scoringEaseType;

        // Point value
        // [SerializeField] private int point;
        // public int Point => point;
        public int Point { get; set; }
    }
}