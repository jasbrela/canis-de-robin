using Enums;
using Interfaces;
using JetBrains.Annotations;
using Managers;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace InteractableObjects
{
    public class SearchableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private Light2D hover;
        [SerializeField] private Collectibles type;
        
        [CanBeNull] private ICollectible _collectible;
        private bool _searched;

        private void Start()
        {
            EventHandler.Instance.ListenToOnCollectCollectible(OnCollectedCollectible);
        }

        private void OnCollectedCollectible(Collectibles collectible)
        {
            if (_collectible != null && type == collectible)
            {
                _searched = true;
            }
        }
        
        public void OnInteract()
        {
            if (_searched)
            {
                if (_collectible == null)
                {
                    string msg = "Eu tenho quase certeza que passei por aqui antes. Bom, não tem nada.";
                    EventHandler.Instance.TriggerOnShowPopupMessage(msg);
                }
                else
                {
                    _collectible.OnCollect();
                }
                
            }
            else
            {
                if (_collectible == null)
                {
                    _searched = true;
                    
                    string msg = "Não encontrei nada.";
                    EventHandler.Instance.TriggerOnShowPopupMessage(msg);
                }
                else
                {
                    EventHandler.Instance.TriggerOnShowPopupMessageWithOptions(_collectible);
                }
            }
        }

        public void SetCollectible(ICollectible collectible)
        {
            _collectible = collectible;
        }

        public void OnEnterRange()
        {
            hover.intensity = 1;
        }

        public void OnQuitRange()
        {
            hover.intensity = 0;
        }
    }
}
