using System.Net.NetworkInformation;
using Interfaces;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace InteractableObjects
{
    public class Cellphone : MonoBehaviour, IInteractable
    {
        [SerializeField] private Light2D light2D;

        private int _password;

        public void OnInteract()
        {
            if (_password == default) GeneratePassword();
            EventHandler.Instance.TriggerOnShowPopupMessage(_password.ToString());
        }

        private void GeneratePassword()
        {
            _password = Random.Range(1000, 10000);
        }
        
        public void OnEnterArea()
        {
            light2D.intensity = 1;
        }

        public void OnQuitArea()
        {
            light2D.intensity = 0;
        }
    }
}
