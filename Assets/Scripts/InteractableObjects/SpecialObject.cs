using Interfaces;
using Managers;
using UnityEngine;

namespace InteractableObjects
{
    public class SpecialObject : MonoBehaviour, IInteractable
    {
        [TextArea(5, 5)][SerializeField] private string message;
        public void OnInteract()
        {
            EventHandler.Instance.TriggerOnShowPopupMessage(message);
        }

        public void OnEnterRange()
        {
            Debug.Log("Entered range - " + gameObject.name);
            // show hvoer sprite
        }

        public void OnQuitRange()
        {
            Debug.Log("Quit range - " + gameObject.name);
            // show normal sprite
        }
    }
}
