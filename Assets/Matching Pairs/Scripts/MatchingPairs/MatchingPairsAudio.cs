using System.Collections.Generic;
using MatchingPairs.pure_unity_methods;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingPairs
{
    [RequireComponent(typeof(AudioSource))]
    public class MatchingPairsAudio : Singleton<MatchingPairsAudio>
    {
        private static AudioSource _backGroundAudioSource;
        private static readonly Queue<AudioSource> AudioSources = new ();
        [SerializeField] private int maximumAudioSources;
        [SerializeField] private GameObject audioSourcePrefab;
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip success;
        [SerializeField] private AudioClip failure;
        [SerializeField] private AudioClip win;

        public void Initialise()
        {
            ResolveDependencies();
            AssignAllButtonsTheClickSound();
            PlayBackgroundMusic();
        }

        private void ResolveDependencies()
        {
            _backGroundAudioSource = GetComponent<AudioSource>(); //Secured by the require component attribute.
            EmptyQueue();
            for (var i = 0; i < maximumAudioSources; i++)
            {
                AudioSources.Enqueue(Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>());
            }
        }
    
        private static void EmptyQueue()
        {
            var itemsInQueue = AudioSources.Count;
            for (var i = 0; i < itemsInQueue; i++)
            {
                AudioSources.Dequeue();
            }
        }

        private static void AssignAllButtonsTheClickSound()
        {
            var allButtons = FindObjectsOfType<Button>();
            foreach (var button in allButtons)
            {
                button.onClick.RemoveListener(PlayButtonClickSound); //Avoid any risk of duplicate sounds.
                button.onClick.AddListener(PlayButtonClickSound);
            }
        }
    
        private static void PlayBackgroundMusic()
        {
            _backGroundAudioSource.Play();
        }

        private static void PlayButtonClickSound()
        {
            PlayClip(Instance.buttonClick);
        }
    
        public static void PlaySuccess()
        {
            PlayClip(Instance.success);
        }
    
        public static void PlayFailure()
        {
            PlayClip(Instance.failure);
        }
    
        public static void PlayGameWon()
        {
            PlayClip(Instance.win);
        }
    
        private static void PlayClip(AudioClip clip)
        {
            if (!Instance) return;
            var audioSource = ReturnFirstUnusedAudioSource();
            audioSource.clip = clip;
            audioSource.Play();
        }

        private static AudioSource ReturnFirstUnusedAudioSource()
        {
            var audioSource = AudioSources.Dequeue();
            AudioSources.Enqueue(audioSource);
            return audioSource;
        }
    }
}
