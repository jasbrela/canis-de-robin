using System;
using Enums;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Player
{
    public class DogSkill : MonoBehaviour
    {
        // TODO: Change this to a LightManager to manage toggles
        
        [SerializeField] private Light2D globalLight;
        private void Start()
        {
            ListenToEvents();
        }

        private void ListenToEvents()
        {
            EventHandler.Instance.ListenToOnChangeCurrentCharacter(ToggleSkill);
        }

        private void ToggleSkill(Character character)
        {
            if (globalLight == null) throw new NullReferenceException("No Global Light2D was assigned.");
            globalLight.intensity = character == Character.Dog ? 0.25f : 0.1f;
        }
    }
}
