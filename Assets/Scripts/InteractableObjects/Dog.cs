using Interfaces;
using UnityEngine;

namespace InteractableObjects
{
    public class Dog : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;

        private bool _firstTime = true;
    
        public void OnInteract()
        {
            string msg;
        
            if (_firstTime)
            {
                msg = "Se a gente conseguir as informações sobre o sistema para" +
                      " transformar grafite em ouro nossa vida vai mudar, Caramelinho!";
                
                _firstTime = false;
            }
            else
            {
                msg = "Bom garoto...";
            }

            EventHandler.Instance.TriggerOnShowPopupMessage(msg);
        }

        public void OnEnterRange()
        {
            //_animator.SetBool("PossibleInteraction", true);
        }

        public void OnQuitRange()
        {
            //_animator.SetBool("PossibleInteraction", false);
        }
    }
}
