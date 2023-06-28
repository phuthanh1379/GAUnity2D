using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioFile> files = new();

        private void Awake()
        {
            Instance = this;
        }

        public void PlayAudioClip(string audioName)
        {
            foreach (var file in files)
            {
                if (file.name == audioName)
                    audioSource.PlayOneShot(file.clip);
            }
        }
    }

    [Serializable]
    public class AudioFile
    {
        public string name;
        public AudioClip clip;
    }
}