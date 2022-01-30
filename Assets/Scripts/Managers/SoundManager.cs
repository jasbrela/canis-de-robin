using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {

        private AudioSource _effect;
        
        private static SoundManager _instance;

        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(SoundManager).ToString());
                    go.AddComponent<SoundManager>();
                }
                return _instance;
            }
        }
    
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
                SetUpSoundManager();
            }
        }

        public void ResetSound()
        {
            _effect.Stop();
        }

        private void SetUpSoundManager()
        {
            GameObject effect = new GameObject("Effect Source");
            effect.transform.SetParent(transform);
            _effect = effect.AddComponent<AudioSource>();
            _effect.playOnAwake = false;
            _effect.loop = false;
        }
        
        public void PlaySound(AudioClip audioClip)
        {
            _effect.PlayOneShot(audioClip);
        }

        public void ChangeMasterVolume(float volume)
        {
            AudioListener.volume = volume;
        }

        public void ToggleEffects()
        {
            _effect.mute = !_effect.mute;
        }
    }
}
