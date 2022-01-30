using Interfaces;
using TMPro;
using UnityEngine;

namespace InteractableObjects
{
    public class Disk : MonoBehaviour, IInteractable, ICollectible
    {
        [SerializeField] private TextMeshProUGUI passwordText;
        private int _password;
        private string _input;
        
        private void Start()
        {
            ListenToEvents();
        }

        private void ListenToEvents()
        {
            EventHandler.Instance.ListenToOnPasswordGenerated(SetPassword);
        }

        private void SetPassword(int pass)
        {
            _password = pass;
        }
        
        
        public void OnInteract()
        {
            EventHandler.Instance.TriggerOnInteractWithDisk();
        }

        public void OnEnterRange()
        {
            /*throw new System.NotImplementedException();*/
        }

        public void OnQuitRange()
        {
            /*throw new System.NotImplementedException();*/
        }

        public void OnClickNumber(int number)
        {
            _input += number;
            UpdateUI();
            if (_input.Length == 4) TryPassword();
        }

        private void TryPassword()
        {
            int.TryParse(_input, out int attempt);
            if (attempt == _password)
            {
                Debug.Log("Correct!");
                // correct
            }
            else
            {
                Debug.Log("WRONG");
                OnClickClear();
            }
        }

        public void OnClickErase()
        {
            _input = _input.Remove(_input.Length-1);
            UpdateUI();
        }
        
        public void OnClickClear()
        {
            _input = "";
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            string final = _input;
            for (int i = _input.Length; i < 4; i++)
            {
                final += "_";
            }

            passwordText.text = final;
        }

        public void OnCollect()
        {
            throw new System.NotImplementedException();
        }
    }
}
