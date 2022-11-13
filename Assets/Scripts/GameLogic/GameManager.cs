using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        playerNameText.text = playerProfile.playerSettings.playerName;
        playerHPText.text = playerProfile.playerSettings.initialHp.ToString();
        scoreText.text = playerProfile.playerSettings.initialScore.ToString();

        Init();
    }

    private void Update()
    {
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

    private void OnPlayerScoring(int score)
    {
        scoreText.text = score.ToString();
    }

    private void OnPlayerDead()
    {
        EndGame();
    }

    #endregion

    #region Methods

    private void PauseGame()
    {
        _isPause = !_isPause;

        if (_isPause)
        {
            // playerMoveController.SetMovable(false);
            pauseGamePanel.SetActive(true);
            pausePlayerNameText.text = playerProfile.playerSettings.playerName;
            pauseScoreText.text = playerProfile.Score.ToString();
        }
        else
        {
            // pauseGamePanel.SetActive(false);
            playerMoveController.SetMovable(true);
        }
    }
    
    private void Init()
    {
        playerProfile.Init();
        playerMoveController.Init();
        _isPause = false;
        _isGameOver = false;
        pauseGamePanel.SetActive(false);
        endGamePanel.SetActive(false);
    }

    private void EndGame()
    {
        playerMoveController.SetMovable(false);
        _isGameOver = true;
        endGamePanel.SetActive(true);
        endgameScoreText.text = playerProfile.Score.ToString();
        endgamePlayerNameText.text = playerProfile.playerSettings.playerName;
    }

    #endregion
}
