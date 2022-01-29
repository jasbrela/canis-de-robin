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
        
        private Directions _lastFacingDirection = Directions.Right;
        private Directions _otherFacingDirection = Directions.Right;
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

            SetUpControls();
        }

        private void SetUpControls()
        {
            _input.actions[InputActions.MoveLeft.ToString()].performed += _ => Move(Directions.Left);
            _input.actions[InputActions.MoveLeft.ToString()].canceled += _ => StopMovement();
            
            _input.actions[InputActions.MoveRight.ToString()].performed += _ => Move(Directions.Right);
            _input.actions[InputActions.MoveRight.ToString()].canceled += _ => StopMovement();

            _input.actions[InputActions.ChangeCharacter.ToString()].performed += _ => ChangeCharacter();
        }

        private void ChangeCharacter()
        {
            StopMovement();

            _otherCharacterRb2D = _mainCharacterRb2D;
            _otherCharacter = _currentCharacter;
            (_otherFacingDirection, _lastFacingDirection) = (_lastFacingDirection, _otherFacingDirection);

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

        private void Move(Directions dir)
        {
            Vector2 vec = dir == Directions.Left ? Vector2.left : Vector2.right;

            _mainCharacterRb2D.AddRelativeForce(vec * force, ForceMode2D.Force);

            if (_lastFacingDirection == dir) return;
            
            _lastFacingDirection = dir;
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
                    _otherFacingDirection = Directions.Left;
                }
                else
                {
                    otherVec = Vector2.right;
                    _otherFacingDirection = Directions.Right;
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
            _mainCharacterRb2D.velocity = Vector2.zero;
        }

        private void StopOtherMovement()
        {
            if (_otherIsNear) _otherCharacterRb2D.velocity = Vector2.zero;
        }
    }
}
