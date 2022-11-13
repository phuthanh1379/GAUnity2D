using System;
using UnityEngine;

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

    private void OnPlayerIsHurt()
    {
        playerMoveController.PlayerHurt();
    }
}