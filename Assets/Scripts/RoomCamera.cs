using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    [SerializeField] private GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) virtualCam.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) virtualCam.SetActive(false);
    }
    
}
