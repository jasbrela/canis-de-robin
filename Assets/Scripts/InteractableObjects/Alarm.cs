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
        [CanBeNull][SerializeField] private GameObject protectedAreaLeft;
        [CanBeNull][SerializeField] private GameObject protectedAreaRight;

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
