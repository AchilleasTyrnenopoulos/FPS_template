using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action OnPause;

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

    // Start is called before the first frame update
    void Start()
    {
        MouseCursorHandler.DisableCursor();
    }

    public void OnPauseInvoke()
    {
        MouseCursorHandler.EnableCursor();
        OnPause?.Invoke();
    }
}
