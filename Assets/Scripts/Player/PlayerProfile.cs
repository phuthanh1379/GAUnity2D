using System;
using UnityEngine;

/// <summary>
/// Script to store and handle player's stats
/// </summary>
public class PlayerProfile : MonoBehaviour
{
    #region Variables

    [SerializeField] private PlayerSettings playerSettings;
    
    // Settings
    public int Score { get; private set; }
    public int HitPoint { get; private set; }
    public string PlayerName { get; private set; }

    // Events
    public event Action<int> PlayerScoring;
    public event Action PlayerDead;
    public event Action PlayerIsHurt;

    #endregion

    #region Unity Methods

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            PlayerHurt(1);
    }

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
    private void PlayerHurt(int dmg)
    {
        HitPoint -= dmg;
        PlayerIsHurt?.Invoke();
    }

    #endregion
}