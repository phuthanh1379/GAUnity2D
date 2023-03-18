using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace GameLogic
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelSettings levelSettings;
        [SerializeField] private Transform obstaclesHolder;
        
        [SerializeField] private List<Obstacle> _obstacles;

        public event Action EndLevel;
        
        private System.Random _rnd;

        public void Init()
        {
            // Clear any previous instants
            if (_obstacles != null)
                foreach (var obstacle in _obstacles)
                {
                    if (obstacle != null)
                        Destroy(obstacle.gameObject);
                }

            // Initialize random seed
            _rnd = new System.Random();
            
            _obstacles =  new List<Obstacle>();
            foreach (var settings in levelSettings.ObstacleSettingsList)
            {
                var obstacle = Instantiate(settings.BasePrefab, obstaclesHolder).GetComponent<Obstacle>();
                obstacle.transform.position = settings.BasePosition;
                settings.Point = _rnd.Next(levelSettings.MinPoint, levelSettings.MaxPoint);
                obstacle.Init(settings);
                obstacle.ObstacleDestroy += OnObstacleDestroyed;
                
                _obstacles.Add(obstacle);
            }
        }

        private void OnObstacleDestroyed(int id)
        {
            foreach (var obstacle in _obstacles)
            {
                if (obstacle.id == id && obstacle != null)
                {
                    _obstacles.Remove(obstacle);
                    Destroy(obstacle.gameObject);
                    if (_obstacles.Count <= 0)
                        EndLevel?.Invoke();
                }
            }
        }
    }
}