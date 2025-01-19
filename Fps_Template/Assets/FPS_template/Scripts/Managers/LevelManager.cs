using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerStartingSpawnPoint;
    [SerializeField] private GameObject _playerPrefab;

    //not sure if needed
    [SerializeField] private string _sceneName;

    private void Awake()
    {
        _sceneName = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        //TODO get spawnpoint index from GameData

        var player = Instantiate(_playerPrefab, _playerStartingSpawnPoint[0].position, _playerStartingSpawnPoint[0].rotation);
        EventAggregator.GetEvent<OnPlayerUnitSpawn>().Publish(player.transform);
    }
}
