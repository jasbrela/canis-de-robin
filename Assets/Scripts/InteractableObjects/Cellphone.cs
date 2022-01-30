using Interfaces;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace InteractableObjects
{
    public class Cellphone : MonoBehaviour, IInteractable, ICollectible
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
            EventHandler.Instance.TriggerOnPasswordGenerated(_password);
        }
        
        public void OnEnterRange()
        {
            light2D.intensity = 1;
        }

        public void OnQuitRange()
        {
            light2D.intensity = 0;
        }

        public void OnCollect()
        {
            throw new System.NotImplementedException();
        }
    }
}
