using System.Collections;
using Enums;
using Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InteractableObjects
{
    public class Elevator : MonoBehaviour, IInteractable
    {
        [SerializeField] private AudioClip elevatorSound;
        [SerializeField] private ElevatorDirections floor;
        [SerializeField] private float duration;
        [SerializeField] private PlayerInput input;
        [SerializeField] private Transform upPart;
        [SerializeField] private Transform downPart;

        private ElevatorDirections _currentFloor;
        
        private void Start()
        {
            _currentFloor = floor;
        }

        private IEnumerator UseElevator()
        {
            SoundManager.Instance.PlaySound(elevatorSound);
            input.DeactivateInput();
            var destination = _currentFloor == ElevatorDirections.Down ? downPart.position : upPart.position;
            
            EventHandler.Instance.TriggerOnMoveElevator(destination.y);
            yield return new WaitForSecondsRealtime(duration);
            
            input.ActivateInput();
        }

        public void OnInteract()
        {
            _currentFloor = _currentFloor == ElevatorDirections.Up ? ElevatorDirections.Down : ElevatorDirections.Up;
            StartCoroutine(UseElevator());
        }
        
        public void OnEnterRange()
        {
            Debug.Log("Enter");
            // open doors
        }

        public void OnQuitRange()
        {
            Debug.Log("Quit");
            //if (_isElevatorActive) return; // don't open, wait for elevator to arrive
            // close doors
        }
    }
}
