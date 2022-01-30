using System;
using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D human;
        [SerializeField] private Rigidbody2D dog;
        [SerializeField] private float force;
        [SerializeField] private float otherForce;
        [SerializeField] private float otherMaxVelocity;
        [SerializeField] private float maxDistance;

        private PlayerDirections _lastFacingPlayerDirection = PlayerDirections.Left;
        private PlayerDirections _otherFacingPlayerDirection = PlayerDirections.Left;
        private bool _otherIsNear;
        private PlayerInput _input;
        private Rigidbody2D _mainCharacterRb2D;
        private Rigidbody2D _otherCharacterRb2D;
        private Character _currentCharacter = Character.Human;
        private Character _otherCharacter = Character.Dog;

        private void Start()
        {
            _input = GetComponent<PlayerInput>();
            _mainCharacterRb2D = human;
            _otherCharacterRb2D = dog;

            EventHandler.Instance.ListenToOnMoveElevator(ChangeFloor);
            
            SetUpControls();
        }

        private void SetUpControls()
        {
            _input.actions[InputActions.MoveLeft.ToString()].performed += _ => Move(PlayerDirections.Left);
            _input.actions[InputActions.MoveLeft.ToString()].canceled += _ => StopMovement();

            _input.actions[InputActions.MoveRight.ToString()].performed += _ => Move(PlayerDirections.Right);
            _input.actions[InputActions.MoveRight.ToString()].canceled += _ => StopMovement();

            _input.actions[InputActions.ChangeCharacter.ToString()].performed += _ => ChangeCharacter();
        }

        private void ChangeCharacter()
        {
            StopMovement();

            _otherCharacterRb2D = _mainCharacterRb2D;
            _otherCharacter = _currentCharacter;
            (_otherFacingPlayerDirection, _lastFacingPlayerDirection) = (_lastFacingPlayerDirection, _otherFacingPlayerDirection);

            switch (_currentCharacter)
            {
                case Character.Human:
                    _mainCharacterRb2D = dog;
                    _currentCharacter = Character.Dog;
                    break;
                case Character.Dog:
                    _mainCharacterRb2D = human;
                    _currentCharacter = Character.Human;
                    break;
            }
            EventHandler.Instance.TriggerOnChangeCurrentCharacter(_currentCharacter);
        }

        private void Move(PlayerDirections dir)
        {
            if (_mainCharacterRb2D == null) return;
            
            Vector2 vec = dir == PlayerDirections.Left ? Vector2.left : Vector2.right;

            _mainCharacterRb2D.AddRelativeForce(vec * force, ForceMode2D.Force);

            if (_lastFacingPlayerDirection == dir) return;
            
            _lastFacingPlayerDirection = dir;
            EventHandler.Instance.TriggerOnChangeFacingDirection(vec, _currentCharacter);
        }

        private void Update()
        {
            _otherIsNear = Mathf.Abs(_mainCharacterRb2D.transform.position.x - _otherCharacterRb2D.transform.position.x) < maxDistance;
            
            if (!_otherIsNear)
            {
                if (_otherCharacterRb2D.velocity.magnitude > otherMaxVelocity) return;
                
                Vector2 otherVec;
                if (_otherCharacterRb2D.transform.position.x > _mainCharacterRb2D.transform.position.x)
                {
                    otherVec = Vector2.left;
                    _otherFacingPlayerDirection = PlayerDirections.Left;
                }
                else
                {
                    otherVec = Vector2.right;
                    _otherFacingPlayerDirection = PlayerDirections.Right;
                }

                _otherCharacterRb2D.AddRelativeForce(otherVec * otherForce, ForceMode2D.Force);
                EventHandler.Instance.TriggerOnChangeFacingDirection(otherVec, _otherCharacter);
            }
            else
            {
                StopOtherMovement();
            }
        }

        private void StopMovement()
        {
            if (_mainCharacterRb2D == null) return;
            _mainCharacterRb2D.velocity = Vector2.zero;
        }

        private void StopOtherMovement()
        {
            if (_otherIsNear) _otherCharacterRb2D.velocity = Vector2.zero;
        }
        
        private void ChangeFloor(float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }
}
