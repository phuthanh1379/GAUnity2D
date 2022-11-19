using UnityEngine;

/// <summary>
/// Script to control other player's scripts
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMoveController playerMoveController;
    [SerializeField] private PlayerProfile playerProfile;

    private void OnEnable()
    {
        playerProfile.PlayerIsHurt += OnPlayerIsHurt;
    }

    private void OnDisable()
    {
        playerProfile.PlayerIsHurt -= OnPlayerIsHurt;
    }

    /// <summary>
    /// Event to call when player takes damage
    /// </summary>
    private void OnPlayerIsHurt()
    {
        playerMoveController.PlayerHurt();
    }
}