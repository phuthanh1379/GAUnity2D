using System;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public PlayerSettings playerSettings;
    
    // Settings
    public int Score { get; private set; }
    public int HitPoint { get; private set; }

    // Events
    public event Action<int> PlayerScoring;
    public event Action PlayerDead;
    public event Action PlayerIsHurt;

    public void Init()
    {
        Score = playerSettings.initialScore;
        HitPoint = playerSettings.initialHp;
    }

    public void Scoring(int score)
    {
        Score += score;

        if (score < 0)
            PlayerHurt(-score); 
        
        if (HitPoint <= 0)
            PlayerDead?.Invoke();
        
        PlayerScoring?.Invoke(Score);
    }

    private void PlayerHurt(int dmg)
    {
        HitPoint -= dmg;
        PlayerIsHurt?.Invoke();
    }
}