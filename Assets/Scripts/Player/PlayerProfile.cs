using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to store and handle player's stats
/// </summary>
public class PlayerProfile : MonoBehaviour
{
    #region Variables

    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator animator;
    
    // Settings
    public int Score { get; private set; }
    public int HitPoint { get; private set; }
    public string PlayerName { get; private set; }

    // Events
    public event Action<int> PlayerScoring;
    public event Action PlayerDead;
    public event Action PlayerIsHurt;

    #endregion

    #region Methods

    /// <summary>
    /// Initialize the script
    /// </summary>
    public void Init()
    {
        // Get info from PlayerSettings scriptable-object
        Score = playerSettings.initialScore;
        HitPoint = playerSettings.initialHp;

        healthBar.maxValue = HitPoint;
        healthBar.value = HitPoint;

        PlayerName = playerSettings.playerName;
    }

    /// <summary>
    /// Update player's score
    /// </summary>
    /// <param name="score"></param>
    public void Scoring(int score)
    {
        Score += score;

        // If score is less than zero, player takes damage
        if (score < 0)
            PlayerHurt(-score);
        
        // If player's hit-point reaches zero, player is dead
        if (HitPoint <= 0)
            PlayerDead?.Invoke();
        
        // Invoke the event to update player's score on UI
        PlayerScoring?.Invoke(Score);
    }

    /// <summary>
    /// Player takes damage
    /// </summary>
    /// <param name="dmg"></param>
    public void PlayerHurt(int dmg)
    {
        Debug.Log("Hurt");
        //animator.SetTrigger("Hurt");
        HitPoint -= dmg;
        healthBar.value -= dmg;
        if (HitPoint <= 0)
        {
            //animator.SetTrigger("Die");
            Debug.Log("Die");
        }
    }

    #endregion
}