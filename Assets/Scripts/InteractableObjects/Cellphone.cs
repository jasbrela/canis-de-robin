using Interfaces;
using UnityEngine;

namespace InteractableObjects
{
    public class Cellphone : MonoBehaviour, ICollectible
    {
        [SerializeField] private Collectible data;

        private int _password;

        private void GeneratePassword()
        {
            _password = Random.Range(1000, 10000);
            EventHandler.Instance.TriggerOnPasswordGenerated(_password);
        }

        public void OnCollect()
        {
            if (_password == default) GeneratePassword();
            EventHandler.Instance.TriggerOnShowPopupMessage(_password.ToString());
        }

        public Collectible GetData()
        {
            return data;
        }
    }
}
