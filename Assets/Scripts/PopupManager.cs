using System;
using Enums;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    
    private GameObject _openedPopup;
    private ICollectible _currentCollectible;
    
    private void Start()
    {
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
        input.SwitchCurrentActionMap(ActionMaps.Player.ToString());
        if (_openedPopup != null) _openedPopup.SetActive(false);
        
        itemFound.gameObject.SetActive(false);
        collectableButtons.SetActive(false);
        victoryButtons.SetActive(false);
    }

    private void ShowVictoryPopup()
    {
        victoryButtons.SetActive(true);
        ShowPopupMessage("Agora que Robin conseguiu encontrar os dados sobre a tecnologia para" +
                         " transformar grafite em ouro, tem uma grande decisão a tomar.");
    }

    private void OnClickGood()
    {
        ClosePopup();
        // go to good scene
    }
    
    private void OnClickBad()
    {
        ClosePopup();
        // go to bad scene
    }
}
