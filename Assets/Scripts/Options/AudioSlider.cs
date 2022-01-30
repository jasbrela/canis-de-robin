using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Options
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private void Start()
        {
            SoundManager.Instance.ChangeMasterVolume(slider.value);
            slider.onValueChanged.AddListener(SoundManager.Instance.ChangeMasterVolume);
        }
    }
}
