using System;
using Enums;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerSpriteHandler : MonoBehaviour
    {
        [SerializeField] private Character character;
        private SpriteRenderer _spriteRenderer;
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            ListenToEvents();
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
