using Enums;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private GameObject diskPopup;
        [SerializeField] private GameObject popup;
        [SerializeField] private TextMeshProUGUI msg;
        [SerializeField] private TextMeshProUGUI cancelBtn;
        [SerializeField] private TextMeshProUGUI collectBtn;
        [SerializeField] private GameObject collectableButtons;
        [SerializeField] private GameObject victoryButtons;
        [SerializeField] private Image itemFound;

        private AudioSource _audioSource;
        private GameObject _openedPopup;
        private ICollectible _currentCollectible;
    
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            SetUpControls();
            ListenToEvents();
        }

        private void SetUpControls()
        {
            input.actions[InputActions.ClosePopup.ToString()].performed += ClosePopupThroughControls;
        }

        private void UnsubscribeControls()
        {
            input.actions[InputActions.ClosePopup.ToString()].performed -= ClosePopupThroughControls;
        }

        private void ListenToEvents()
        {
            EventHandler.Instance.ListenToOnShowPopupMessage(ShowPopupMessage);
            EventHandler.Instance.ListenToOnShowVictoryPopup(ShowVictoryPopup);
            EventHandler.Instance.ListenToOnShowPopupMessageWithOptions(ShowPopupMessageWithOptions);
            EventHandler.Instance.ListenToOnInteractWithDisk(ShowDiskPopup);
            EventHandler.Instance.ListenToOnGameOver(UnsubscribeControls);
        }
    
        private void ShowDiskPopup()
        {
            input.SwitchCurrentActionMap(ActionMaps.UI.ToString());

            diskPopup.SetActive(true);
            _openedPopup = diskPopup;
        }

        private void ShowPopupMessage(string message)
        {
            _audioSource.Play();
            input.SwitchCurrentActionMap(ActionMaps.UI.ToString());

            msg.text = message;

            popup.SetActive(true);
            _openedPopup = popup;
        }
    
        private void ShowPopupMessageWithOptions(ICollectible collectible)
        {
            _currentCollectible = collectible;
            Collectible data = collectible.GetData();

            itemFound.sprite = data.sprite;
            collectBtn.text = data.collectMsg;
            cancelBtn.text = data.cancelMsg;
            itemFound.gameObject.SetActive(true);
            collectableButtons.SetActive(true);
            ShowPopupMessage(data.message);
        }

        public void OnClickCollect()
        {
            EventHandler.Instance.TriggerOnCollectCollectible(_currentCollectible.GetData().type);
            ClosePopup();
            _currentCollectible.OnCollect();
        }

        public void OnClickCancel()
        {
            _currentCollectible = null;
            ClosePopup();
        }

        private void ClosePopupThroughControls(InputAction.CallbackContext callbackContext)
        {
            ClosePopup();
        }
    
        private void ClosePopup()
        {
            if (_audioSource.isPlaying) _audioSource.Stop();
            input.SwitchCurrentActionMap(ActionMaps.Player.ToString());
            if (_openedPopup != null) _openedPopup.SetActive(false);
        
            itemFound.gameObject.SetActive(false);
            collectableButtons.SetActive(false);
            victoryButtons.SetActive(false);
        }

        private void ShowVictoryPopup()
        {
            victoryButtons.SetActive(true);
            ShowPopupMessage("Agora que eu consegui encontrar os dados sobre a tecnologia para" +
                             " transformar grafite em ouro, tenho uma grande decisão a tomar.");
        }

        private void OnClickGood()
        {
            ClosePopup();
            SceneManager.LoadScene(Scenes.Victory.ToString());
            // go to good scene
        }
    
        private void OnClickBad()
        {
            ClosePopup();
            SceneManager.LoadScene(Scenes.Victory.ToString());
        }
    }
}
