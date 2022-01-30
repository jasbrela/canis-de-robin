using Interfaces;
using UnityEngine;

namespace InteractableObjects
{
    public class SearchableObject : MonoBehaviour, IInteractable
    {
        private ICollectible _collectible;

        private bool _searched;
        
        public void OnInteract()
        {
            // TODO: if searched, show "already searched" message    else show "found item , pick?"
            _collectible?.OnCollect();
        }

        public void SetCollectible(ICollectible collectible)
        {
            _collectible = collectible;
        }

        public void OnEnterRange()
        {
            throw new System.NotImplementedException();
        }

        public void OnQuitRange()
        {
            throw new System.NotImplementedException();
        }
    }
}
