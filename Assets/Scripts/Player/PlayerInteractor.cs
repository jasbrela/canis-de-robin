using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Transform humanTransform;
        [SerializeField] private float maxRange;
        [SerializeField] private LayerMask interactableMask;
        [SerializeField] private bool debug;

        private PlayerInput _input;
        private Vector2 _forward = Vector2.right;

        private IInteractable _currentTarget;
        private Character _currentCharacter = Character.Human;

        private void Start()
        {
            ListenToEvents();
            
            _input = GetComponent<PlayerInput>();

            SetUpControls();
        }
        
        private void ListenToEvents()
        {
            EventHandler.Instance.ListenToOnChangeFacingDirection(ChangeFacingDirection);
            EventHandler.Instance.ListenToOnChangeCurrentCharacter(ChangeCurrentCharacter);
        }
        private void Update()
        {
            RaycastForInteractable();
        }

        private void ChangeCurrentCharacter(Character character)
        {
            _currentCharacter = character;
            if (character == Character.Dog) ResetTarget();
        }

        private void ChangeFacingDirection(Vector2 direction, Character character)
        {
            _forward = direction;
        }

        private void RaycastForInteractable()
        {
            if (_currentCharacter != Character.Human) return;
            
            Ray2D ray = new Ray2D(humanTransform.position, _forward);
            RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, maxRange, interactableMask);

            if (debug) Debug.DrawRay(ray.origin, ray.direction * maxRange, Color.cyan);
            
            if (hit2D.collider != null)
            {
                IInteractable interactable = hit2D.collider.gameObject.GetComponent<IInteractable>();

                if (_currentTarget == interactable) return;

                _currentTarget?.OnQuitRange();
                _currentTarget = interactable;
                _currentTarget.OnEnterRange();
            }
            else
            {
                ResetTarget();
            }
        }

        private void ResetTarget()
        {
            if (_currentTarget == null) return;

            _currentTarget.OnQuitRange();
            _currentTarget = null;
        }
        
        private void SetUpControls()
        {
            _input.actions[InputActions.Interact.ToString()].performed += _ => Interact();
        }
        
        private void Interact()
        {
            _currentTarget?.OnInteract();
        }
    }
}
