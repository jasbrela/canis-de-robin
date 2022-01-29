using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI msg;
    private void Start()
    {
        ListenToEvents();
    }

    private void ListenToEvents()
    {
        EventHandler.Instance.ListenToOnShowPopupMessage(ShowPopupMessage);
    }

    private void ShowPopupMessage(string message)
    {
        msg.text = message;
        popup.SetActive(true);
    }

    public void OnClickClose()
    {
        popup.SetActive(false);
    }
}
