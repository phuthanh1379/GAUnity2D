using System;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameLogic
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button aboutButton;
        [SerializeField] private Button optionButton;

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
            SceneManager.LoadScene(GameConstants.SceneMainGame);
        }

        private void OnClickOption()
        {
            
        }

        private void OnClickAbout()
        {
            
        }
    }
}
