using System;
using System.Collections;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace InteractableObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Alarm : MonoBehaviour, IInteractable
    {
        [SerializeField] private float timerIncrease;
        [SerializeField] private float secondsToAlarm;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject alarmObject;

        [SerializeField] private Light2D light2D;
        
        private float _timer;
        private bool _alarmIsActivated;
        private Coroutine _alarmCoroutine;
        private SpriteRenderer _spriteRenderer;
        
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timer = secondsToAlarm;
            _alarmIsActivated = true;
            StartAlarm();
        }

        private void StartAlarm()
        {
            alarmObject.SetActive(true);
            _alarmCoroutine = StartCoroutine(AlarmTimer());
        }
    
        private IEnumerator AlarmTimer()
        {
            while (_alarmIsActivated)
            {
                yield return new WaitForSecondsRealtime(timerIncrease);
                _timer -= timerIncrease;
                timerText.text = FormatTime(_timer);
                
                if (!(_timer < 0f)) continue;
                
                _timer = 0;
                StopAlarm();
                SceneManager.LoadScene(Scenes.GameOver.ToString());
            }
        }

        private string FormatTime( float time )
        {
            int minutes = (int) time / 60 ;
            int seconds = (int) time - 60 * minutes;
            int milliseconds = (int) (10 * (time - minutes * 60 - seconds));
            return $"{minutes:0}:{seconds:00}.{milliseconds:0}";
        }

        public void OnInteract() // STOP ALARM
        {
            StopAlarm();
        }

        private void StopAlarm()
        {
            StopCoroutine(_alarmCoroutine);
            alarmObject.SetActive(false);
        }
        
        public void OnEnterRange()
        {
            light2D.intensity = 0.55f;
        }

        public void OnQuitRange()
        {
            light2D.intensity = 0;
        }
    }
}
