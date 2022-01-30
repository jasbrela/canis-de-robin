using Managers;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Deadzone : MonoBehaviour
{
    [SerializeField] private AudioClip alarmHit;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(alarmHit);
            EventHandler.Instance.TriggerOnGameOver();
        }
    }
}
