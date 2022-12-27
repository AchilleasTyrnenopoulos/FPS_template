using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform _playerStartingSpawnPoint;
    [SerializeField] private GameObject _playerPrefab;

    //not sure if needed
    [SerializeField] private string _sceneName;

    private void Awake()
    {
        _sceneName = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        Instantiate(_playerPrefab, _playerStartingSpawnPoint.position, _playerStartingSpawnPoint.rotation);    
    }
}
