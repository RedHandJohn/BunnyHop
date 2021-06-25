using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip BGMusic;
        public AudioClip JetPackMusic;
        public AudioClip BounceSfx;
        public AudioClip GameOverSound;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayBGMusic()
        {
            if(_audioSource.clip != BGMusic)
            {
                _audioSource.clip = BGMusic;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }

        public void PlayBounceSFX()
        {
            _audioSource.PlayOneShot(BounceSfx);
        }

        public void PlayJetPack()
        {
            if(_audioSource.clip != JetPackMusic)
            {
                _audioSource.clip = JetPackMusic;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }

        public void PlayGameOverSound()
        {
            _audioSource.clip = GameOverSound;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }
}
