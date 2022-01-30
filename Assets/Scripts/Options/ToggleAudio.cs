using Managers;
using UnityEngine;

namespace Options
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] private bool toggleMusic, toggleEffects;

        public void Toggle()
        {
            if (toggleEffects) SoundManager.Instance.ToggleEffects();
            /*
            if (toggleMusic) SoundManager.Instance.ToggleMusic();
        */
        }
    }
}
