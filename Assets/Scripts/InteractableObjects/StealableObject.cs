using System;
using Interfaces;
using UnityEngine;

namespace InteractableObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StealableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] Sprite normalSprite;
        [SerializeField] Sprite hoverSprite;
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnInteract()
        {
            EventHandler.Instance.TriggerOnShowPopupMessage("Tried to steal");
        }
        
        public void OnEnterRange()
        {
            _spriteRenderer.sprite = hoverSprite;
        }

        public void OnQuitRange()
        {
            _spriteRenderer.sprite = normalSprite;
        }
    }
}
