using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set;}
    [Header("---Scenes---")]
    [SerializeField] private string _currentSceneName = string.Empty;
    public void SetCurrentScene(string sceneName) => _currentSceneName = sceneName;
    public string GetCurrentSceneName() => _currentSceneName;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this.gameObject);
    }
}
