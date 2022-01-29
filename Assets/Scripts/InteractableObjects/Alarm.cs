using System;
using System.Collections;
using Interfaces;
using TMPro;
using UnityEngine;

namespace InteractableObjects
{
    public class Alarm : MonoBehaviour, IInteractable
    {
        [SerializeField] private float timerIncrease;
        [SerializeField] private float secondsToAlarm;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject alarmObject;
        
        [SerializeField] Sprite normalSprite;
        [SerializeField] Sprite hoverSprite;
        
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
                while (!_alarmIsActivated) yield return null;
                
                yield return new WaitForSecondsRealtime(timerIncrease);
                _timer -= timerIncrease;
                timerText.text = FormatTime(_timer);
                if (Math.Abs(_timer - secondsToAlarm) < .05f)
                {
                    Debug.Log("Lose");
                    StopAlarm();
                }
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
            alarmObject.SetActive(false);
            _alarmIsActivated = false;
        }
        
        public void OnEnterArea()
        {
            _spriteRenderer.sprite = hoverSprite;
        }

        public void OnQuitArea()
        {
            _spriteRenderer.sprite = normalSprite;
        }
    }
}
