using UnityEngine;

/// <summary>
/// Script to control other player's scripts
/// </summary>
[RequireComponent(typeof(PlayerMoveController), typeof(PlayerProfile))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMoveController playerMoveController;
    [SerializeField] private PlayerProfile playerProfile;

    private void Awake()
    {
        //Init();
    }

    public void Init()
    {
        playerProfile.Init();
        playerMoveController.Init();
    }
}