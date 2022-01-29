using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI msg;
    
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
    }

    private void ShowPopupMessage(string message)
    {
        input.SwitchCurrentActionMap(ActionMaps.UI.ToString());

        msg.text = message;
        popup.SetActive(true);
    }

    private void ClosePopup()
    {
        input.SwitchCurrentActionMap(ActionMaps.Player.ToString());
        popup.SetActive(false);
    }
}
