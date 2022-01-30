using System;
using Enums;
using InteractableObjects;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    
    public delegate void OnShowPopupMessageWithOptions(ICollectible collectible);
    private OnShowPopupMessageWithOptions _onShowPopupMessageWithOptions;

    public void ListenToOnShowPopupMessageWithOptions(OnShowPopupMessageWithOptions method)
    {
        _onShowPopupMessageWithOptions += method;
    } 
    
    public void TriggerOnShowPopupMessageWithOptions(ICollectible collectible)
    {
        _onShowPopupMessageWithOptions?.Invoke(collectible);
    }
    
    
    public delegate void OnShowVictoryPopup();
    private OnShowVictoryPopup _onShowVictoryPopup;

    public void ListenToOnShowVictoryPopup(OnShowVictoryPopup method)
    {
        _onShowVictoryPopup += method;
    } 
    
    public void TriggerOnShowVictoryPopup()
    {
        _onShowVictoryPopup?.Invoke();
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
    
    
    public delegate void OnMoveElevator(float y);
    private OnMoveElevator _onMoveElevator;

    public void ListenToOnMoveElevator(OnMoveElevator method)
    {
        _onMoveElevator += method;
    } 
    
    public void TriggerOnMoveElevator(float y)
    {
        _onMoveElevator?.Invoke(y);
    }
    
    
    public delegate void OnCollectCollectible(Collectibles collectible);
    private OnCollectCollectible _onCollectCollectible;

    public void ListenToOnCollectCollectible(OnCollectCollectible method)
    {
        _onCollectCollectible += method;
    } 
    
    public void TriggerOnCollectCollectible(Collectibles collectible)
    {
        _onCollectCollectible?.Invoke(collectible);
    }
    
    
    public delegate void OnAlarmIsDeactivated(Alarm alarm);
    private OnAlarmIsDeactivated _onAlarmIsDeactivated;

    public void ListenToOnAlarmIsDeactivated(OnAlarmIsDeactivated method)
    {
        _onAlarmIsDeactivated += method;
    } 
    
    public void TriggerOnAlarmIsDeactivated(Alarm alarm)
    {
        _onAlarmIsDeactivated?.Invoke(alarm);
    }
    
    
    public delegate void OnGameOver();
    private OnGameOver _onGameOver;

    public void ListenToOnGameOver(OnGameOver method)
    {
        _onGameOver += method;
    } 
    
    public void TriggerOnGameOver()
    {
        _onGameOver?.Invoke();
        SceneManager.LoadScene(Scenes.GameOver.ToString());
    }
}