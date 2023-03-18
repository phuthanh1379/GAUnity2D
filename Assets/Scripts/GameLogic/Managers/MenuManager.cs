using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button aboutButton;
        [SerializeField] private Button optionButton;

        [SerializeField] private SpriteRenderer renderer;

        private void Start()
        {
            //SharedData.Instance.data = 100;
            Debug.Log(PlayerPrefs.GetString("CurrentScene"));
            Debug.Log(PlayerPrefs.GetInt(GameConstants.CurrentSceneIndex));
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnClickPlay);
            aboutButton.onClick.AddListener(OnClickOption);
            optionButton.onClick.AddListener(OnClickAbout);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnClickPlay);
            aboutButton.onClick.RemoveListener(OnClickOption);
            optionButton.onClick.RemoveListener(OnClickAbout);
        }

        private void OnClickPlay()
        {
            SceneController.Instance.ClickLoadScene(GameConstants.SceneMainGame);
        }

        private void OnClickOption()
        {
            renderer.DOFade(1f, 1f).Play();
        }

        private void OnClickAbout()
        {
            
        }
    }
}
