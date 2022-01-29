using System;
using Enums;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private static EventHandler _instance;

    public static EventHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject(typeof(EventHandler).ToString());
                go.AddComponent<EventHandler>();
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public delegate void OnChangeFacingDirection(Vector2 vector2, Character mainCharacter);
    private OnChangeFacingDirection _onChangeFacingDirection;

    public void ListenToOnChangeFacingDirection(OnChangeFacingDirection method)
    {
        _onChangeFacingDirection += method;
    } 
    
    public void TriggerOnChangeFacingDirection(Vector2 direction, Character mainCharacter)
    {
        _onChangeFacingDirection?.Invoke(direction, mainCharacter);
    } 
    
    
    public delegate void OnShowPopupMessage(string msg);
    private OnShowPopupMessage _onShowPopupMessage;

    public void ListenToOnShowPopupMessage(OnShowPopupMessage method)
    {
        _onShowPopupMessage += method;
    } 
    
    public void TriggerOnShowPopupMessage(string msg)
    {
        _onShowPopupMessage?.Invoke(msg);
    }
    
    
    public delegate void OnInteractWithDisk();
    private OnInteractWithDisk _onInteractWithDisk;

    public void ListenToOnInteractWithDisk(OnInteractWithDisk method)
    {
        _onInteractWithDisk += method;
    } 
    
    public void TriggerOnInteractWithDisk()
    {
        _onInteractWithDisk?.Invoke();
    }
    
    
    public delegate void OnChangeCurrentCharacter(Character currentCharacter);
    private OnChangeCurrentCharacter _onChangeCurrentCharacter;

    public void ListenToOnChangeCurrentCharacter(OnChangeCurrentCharacter method)
    {
        _onChangeCurrentCharacter += method;
    } 
    
    public void TriggerOnChangeCurrentCharacter(Character currentCharacterTransform)
    {
        _onChangeCurrentCharacter?.Invoke(currentCharacterTransform);
    }
    
    
    public delegate void OnPasswordGenerated(int pass);
    private OnPasswordGenerated _onPasswordGenerated;

    public void ListenToOnPasswordGenerated(OnPasswordGenerated method)
    {
        _onPasswordGenerated += method;
    } 
    
    public void TriggerOnPasswordGenerated(int pass)
    {
        _onPasswordGenerated?.Invoke(pass);
    }
}