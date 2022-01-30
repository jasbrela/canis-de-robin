using System.Collections;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace InteractableObjects
{
    public class Alarm : MonoBehaviour, IInteractable
    {
        [SerializeField] private int gracePeriod;
        [SerializeField] private bool isStartAlarm;
        [CanBeNull][SerializeField] private BoxCollider2D protectedAreaLeft;
        [CanBeNull][SerializeField] private BoxCollider2D protectedAreaRight;

        private bool _isDeactivated;

        private void Start()
        {
            if (isStartAlarm) OnInteract();
        }

        public void ActivateAlarm()
        {
            _isDeactivated = false;
            ProtectAreas();
        }

        public void OnInteract()
        {
            if (_isDeactivated) return;
            _isDeactivated = true;
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
            Debug.Log("Entered range of: " + gameObject.name);
        }

        public void OnQuitRange()
        {
            Debug.Log("Quit range of: " + gameObject.name);
        }
        
        private void ProtectAreas()
        {
            if (protectedAreaLeft != null) protectedAreaLeft.enabled = true;
            if (protectedAreaRight != null) protectedAreaRight.enabled = true;
        }
        
        private void FreeAreas()
        {
            if (protectedAreaLeft != null) protectedAreaLeft.enabled = false;
            if (protectedAreaRight != null) protectedAreaRight.enabled = false;
        }
    }
}
