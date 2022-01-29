using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private GameObject diskPopup;
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI msg;

    private GameObject openedPopup;
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
        EventHandler.Instance.ListenToOnInteractWithDisk(ShowDiskPopup);
    }
    
    private void ShowDiskPopup()
    {
        input.SwitchCurrentActionMap(ActionMaps.UI.ToString());

        diskPopup.SetActive(true);
        openedPopup = diskPopup;
    }

    private void ShowPopupMessage(string message)
    {
        input.SwitchCurrentActionMap(ActionMaps.UI.ToString());

        msg.text = message;
        popup.SetActive(true);
        openedPopup = popup;
    }

    private void ClosePopup()
    {
        input.SwitchCurrentActionMap(ActionMaps.Player.ToString());
        openedPopup.SetActive(false);
    }
}
