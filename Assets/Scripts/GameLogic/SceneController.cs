using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [Header("Loading UI")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private Slider slider;

    public static SceneController Instance { get; private set; }

    // Which scene is currently loaded?
    private Scene _currentLoadedScene;

    #region Unity Methods
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region Events

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentLoadedScene = scene;
        mainCamera.gameObject.SetActive(false);

        PlayerPrefs.SetString("CurrentScene", scene.name);
        PlayerPrefs.SetInt(GameConstants.CurrentSceneIndex, scene.buildIndex);

        Debug.Log($"{scene.name}, {mode}");
        loadingPanel.SetActive(false);
    }

    #endregion

    #region Methods

    private void Init()
    {
        Instance = this;
        loadingPanel.SetActive(false);

        LoadMenuScene();
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(GameConstants.SceneMenu, LoadSceneMode.Additive);
    }

    /// <summary>
    /// On-click to load new scene
    /// Click vào để load scene mới
    /// </summary>
    /// <param name="sceneName"></param>
    public void ClickLoadScene(string sceneName)
    {
        // Load SINGLE (single scene, load new scene will remove the old scene)
        // Load scene đơn lẻ (load scene mới sẽ bỏ scene cũ)
        //SceneManager.LoadScene(GameConstants.SceneMainGame);

        // Load ADDITIVE (load new scene will add to the scenes stack, not remove the old scene)
        // Load theo mode Additive (load scene mới sẽ add thêm scene chứ ko xóa đi)
        //SceneManager.LoadScene(GameConstants.SceneMainGame, LoadSceneMode.Additive);

        // Unload old scene
        SceneManager.UnloadSceneAsync(_currentLoadedScene.buildIndex);

        // Temp camera
        mainCamera.gameObject.SetActive(true);

        // Load scene and show progress
        StartCoroutine(LoadSceneProgress(sceneName));
    }

    /// <summary>
    /// Load scene and show progress
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneProgress(string sceneName)
    {
        yield return null;

        // Set active loading panel
        loadingPanel.SetActive(true);

        // Một công việc để gán cho việc load scene (operation ~ công việc)
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        // turn off allowSceneActivation so the scene will not be loaded automatically
        operation.allowSceneActivation = false;

        // while the operation is not done yet, display the progress
        while (!operation.isDone)
        {
            loadingText.text = $"{operation.progress * 100}%";

            Debug.Log($"{operation.progress * 100} %");
            slider.value = operation.progress;

            // when the operation is ready
            if (operation.progress >= 0.9f)
            {
                slider.value = slider.maxValue;
                loadingText.text = "Press any button to continue";
                if (Input.anyKeyDown)
                    operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    #endregion
}
