using System;
using Enums;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))/*, typeof(Animator)*/]
    public class PlayerSpriteHandler : MonoBehaviour
    {
        [SerializeField] private Character character;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            ListenToEvents();
        }

        private void Update()
        {
            if (_animator == null) return;
            _animator.SetFloat(Speed, _rigidbody2D.velocity.magnitude);
        }

        private void ListenToEvents()
        {
            EventHandler.Instance.ListenToOnChangeFacingDirection(OnFacingDirectionChanges);
            EventHandler.Instance.ListenToOnChangeCurrentCharacter(ChangeSortingOrder);
        }

        private void ChangeSortingOrder(Character currentCharacter)
        {
            _spriteRenderer.sortingOrder = currentCharacter == character ? 1 : 0;
        }

        private void OnFacingDirectionChanges(Vector2 dir, Character currentCharacter)
        {
            if (currentCharacter == character) _spriteRenderer.flipX = dir.x < 0;
        }
    }
}
