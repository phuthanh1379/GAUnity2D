using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    /// <summary>
    /// Script to manage the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables

        [Header("Managers")]
        [SerializeField] private LevelManager levelManager;
        
        [Header("In-game UI")]
        [SerializeField] private TMP_Text playerNameText;
        [SerializeField] private TMP_Text playerHPText;
        [SerializeField] private TMP_Text scoreText;

        [Header("Pause Game panel")]
        [SerializeField] private GameObject pauseGamePanel;
        [SerializeField] private TMP_Text pauseScoreText;
        [SerializeField] private TMP_Text pausePlayerNameText;

        [Header("End Game panel")]
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private TMP_Text endgamePlayerNameText;
        [SerializeField] private TMP_Text endgameScoreText;

        [Header("Player scripts")]
        [SerializeField] private PlayerProfile playerProfile;
        [SerializeField] private PlayerMoveController playerMoveController;

        // Private
        private bool _isPause;
        private bool _isGameOver;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            playerProfile.PlayerScoring += OnPlayerScoring;
            playerProfile.PlayerDead += OnPlayerDead;
            levelManager.EndLevel += OnEndLevel;
        }

        private void OnDisable()
        {
            playerProfile.PlayerScoring -= OnPlayerScoring;
            playerProfile.PlayerDead -= OnPlayerDead;
            levelManager.EndLevel -= OnEndLevel;
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
        /// <param name="score">Player's score input</param>
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

        private void OnEndLevel()
        {
            Debug.Log("End level!");
            EndGame();
        }

        #endregion

        #region Methods
    
        /// <summary>
        /// Initialize the script
        /// </summary>
        private void Init()
        {
            // Init level-manager script
            levelManager.Init();

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
}
