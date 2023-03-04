using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGamePaused = false;
    public bool GetIsGamePaused() => _isGamePaused;

    public static GameManager Instance { get; private set; }
    public InputActions Input { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning($"There are multiple GameManagers in the scene.\n Name: {this.gameObject.name}\n Position: {this.transform}");
            Destroy(this.gameObject);
        }

    }

    private void OnEnable()
    {
        Input = new InputActions();

        Input.UI.Enable();

        EventAggregator.GetEvent<PauseStartEvent>().Subscribe(MouseCursorEnable);
        EventAggregator.GetEvent<PauseEndEvent>().Subscribe(MouseCursorDisable);
    }    
    private void OnDisable()
    {
        Input.UI.Disable();

        EventAggregator.GetEvent<PauseStartEvent>().UnSubscribe(MouseCursorEnable);
        EventAggregator.GetEvent<PauseEndEvent>().UnSubscribe(MouseCursorDisable);
    }

    // Start is called before the first frame update
    void Start()
    {
        MouseCursorHandler.DisableCursor();
    }

    private void Update()
    {
        if(Input.UI.Back.triggered)
        {
            TogglePauseGame();
        }
    }


    public void MouseCursorEnable()
    {
        MouseCursorHandler.EnableCursor();
        //OnPause?.Invoke();
    }

    public void MouseCursorDisable()
    {
        MouseCursorHandler.DisableCursor();
        //OnPauseExit?.Invoke();
    }

    public void TogglePauseGame()
    {
        if(_isGamePaused)
        {
            Debug.Log("game is un-paused");
            _isGamePaused = false;            
            EventAggregator.GetEvent<PauseEndEvent>().Publish();
        }
        else
        {
            Debug.Log("game is paused");
            _isGamePaused = true;            
            EventAggregator.GetEvent<PauseStartEvent>().Publish();
        }
    }
}
