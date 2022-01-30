using Enums;
using Interfaces;
using JetBrains.Annotations;
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
    [SerializeField] private GameObject buttons;
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
        input.actions[InputActions.ClosePopup.ToString()].performed += _ => ClosePopup();
    }

    private void ListenToEvents()
    {
        EventHandler.Instance.ListenToOnShowPopupMessage(ShowPopupMessage);
        EventHandler.Instance.ListenToOnShowPopupMessageWithOptions(ShowPopupMessageWithOptions);
        EventHandler.Instance.ListenToOnInteractWithDisk(ShowDiskPopup);
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
        buttons.SetActive(true);
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

    private void ClosePopup()
    {
        input.SwitchCurrentActionMap(ActionMaps.Player.ToString());
        _openedPopup.SetActive(false);
        
        itemFound.gameObject.SetActive(false);
        buttons.SetActive(false);
    }
}
