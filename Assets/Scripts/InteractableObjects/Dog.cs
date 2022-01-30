using System;
using Interfaces;
using UnityEngine;
using EventHandler = Managers.EventHandler;

namespace InteractableObjects
{
    [RequireComponent(typeof(Animator))]
    public class Dog : MonoBehaviour, IInteractable
    {
        private Animator _animator;
        private static readonly int PossibleInteraction = Animator.StringToHash("PossibleInteraction");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnInteract()
        {
            EventHandler.Instance.TriggerOnShowPopupMessage("Se a gente conseguir as informações sobre o sistema para" +
                                                            " transformar grafite em ouro nossa vida vai mudar, Caramelinho!");

            enabled = false;
        }

        public void OnEnterRange()
        {
            _animator.SetBool(PossibleInteraction, true);
        }

        public void OnQuitRange()
        {
            _animator.SetBool(PossibleInteraction, false);
        }
    }
}
