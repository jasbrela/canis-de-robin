using Interfaces;
using UnityEngine;

namespace InteractableObjects
{
    public class Dog : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;
        
        public void OnInteract()
        {
            EventHandler.Instance.TriggerOnShowPopupMessage("Se a gente conseguir as informações sobre o sistema para" +
                                                            " transformar grafite em ouro nossa vida vai mudar, Caramelinho!");

            enabled = false;
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
