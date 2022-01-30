using System.Collections;
using Interfaces;
using JetBrains.Annotations;
using Managers;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using EventHandler = Managers.EventHandler;

namespace InteractableObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Alarm : MonoBehaviour, IInteractable
    {
        [SerializeField] private AudioClip deactivatedSound;
        [SerializeField] private int gracePeriod;
        [SerializeField] private bool isStartAlarm;
        [CanBeNull][SerializeField] private GameObject protectedAreaLeft;
        [CanBeNull][SerializeField] private GameObject protectedAreaRight;
        [SerializeField] private Light2D hover;
        
        private bool _isDeactivated;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void ActivateAlarm()
        {
            _isDeactivated = false;
            ProtectAreas();
        }

        public void OnInteract()
        {
            if (isStartAlarm) EventHandler.Instance.TriggerOnShowPopupMessage(
                "Era esperado que um cara como o Lone Sumk tivesse um ótimo sistema de alarmes, " +
                "ele só não esperava que quem iria roubar a casa dele seria um dos seus " +
                "engenheiros de segurança da informação...");
                
            if (_isDeactivated) return;
            _isDeactivated = true;
            SoundManager.Instance.PlaySound(deactivatedSound);
            EventHandler.Instance.TriggerOnAlarmIsDeactivated(this);
            FreeAreas();
            StartCoroutine(StartGracePeriod());
        }

        private IEnumerator StartGracePeriod()
        {
            yield return new WaitForSecondsRealtime(gracePeriod + 0.1f);
            ActivateAlarm();
        }

        public int GetGracePeriod()
        {
            return gracePeriod;
        }
        
        public void OnEnterRange()
        {
            if (_isDeactivated) return;
            hover.intensity = 0.4f;
        }

        public void OnQuitRange()
        {
            hover.intensity = 0;
        }
        
        private void ProtectAreas()
        {
            if (protectedAreaLeft != null) protectedAreaLeft.SetActive(true);
            if (protectedAreaRight != null) protectedAreaRight.SetActive(true);
        }
        
        private void FreeAreas()
        {
            if (protectedAreaLeft != null) protectedAreaLeft.SetActive(false);
            if (protectedAreaRight != null) protectedAreaRight.SetActive(false);
        }
    }
}
