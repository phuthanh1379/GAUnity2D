using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to manage the game
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Variables

    [Header("In-game UI")]
    public TMP_Text playerNameText;
    public TMP_Text playerHPText;
    public TMP_Text scoreText;

    [Header("Pause Game panel")]
    public GameObject pauseGamePanel;
    public TMP_Text pauseScoreText;
    public TMP_Text pausePlayerNameText;

    [Header("End Game panel")]
    public GameObject endGamePanel;
    public TMP_Text endgamePlayerNameText;
    public TMP_Text endgameScoreText;

    [Header("Player scripts")]
    public PlayerProfile playerProfile;
    public PlayerMoveController playerMoveController;
    
    // Private
    private bool _isPause;
    private bool _isGameOver;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        playerProfile.PlayerScoring += OnPlayerScoring;
        playerProfile.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        playerProfile.PlayerScoring -= OnPlayerScoring;
        playerProfile.PlayerDead -= OnPlayerDead;
    }

    private void Awake()
    {
        playerNameText.text = playerProfile.PlayerName;
        playerHPText.text = playerProfile.HitPoint.ToString();
        scoreText.text = playerProfile.Score.ToString();

        Init();
    }

    private void Update()
    {
        // If game is already over, disable the pause function
        if (Input.GetKeyDown(KeyCode.Escape) && !_isGameOver)
            PauseGame();
    }

    #endregion

    #region Button Methods

    public void RestartGame()
    {
        Init();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Events

    /// <summary>
    /// Event to call when player scores
    /// </summary>
    /// <param name="score"></param>
    private void OnPlayerScoring(int score)
    {
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Event to call when player is dead
    /// </summary>
    private void OnPlayerDead()
    {
        EndGame();
    }

    #endregion

    #region Methods
    
    /// <summary>
    /// Initialize the script
    /// </summary>
    private void Init()
    {
        // Init player's scripts
        playerProfile.Init();
        playerMoveController.Init();
        
        // Reset the boolean variables
        _isPause = false;
        _isGameOver = false;
        
        // Hide the panels
        pauseGamePanel.SetActive(false);
        endGamePanel.SetActive(false);
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    private void PauseGame()
    {
        _isPause = !_isPause;

        // Pause the game
        if (_isPause)
        {
            // Disable player's movement
            playerMoveController.SetMovable(false);
            
            // Display the pause-game panel
            pauseGamePanel.SetActive(true);
            
            // Display stats for the pause-game panel
            pausePlayerNameText.text = playerProfile.PlayerName;
            pauseScoreText.text = playerProfile.Score.ToString();
        }
        // Unpause the game
        else
        {
            // Hide pause-game panel
            pauseGamePanel.SetActive(false);
            
            // Enable player's movement
            playerMoveController.SetMovable(true);
        }
    }

    /// <summary>
    /// End the game
    /// </summary>
    private void EndGame()
    {
        // Disable player's movement
        playerMoveController.SetMovable(false);
        
        // Turn on the state game-over
        _isGameOver = true;
        
        // Display the end-game panel
        endGamePanel.SetActive(true);
        
        // Display stats for the end-game panel
        endgameScoreText.text = playerProfile.Score.ToString();
        endgamePlayerNameText.text = playerProfile.PlayerName;
    }

    #endregion
}
