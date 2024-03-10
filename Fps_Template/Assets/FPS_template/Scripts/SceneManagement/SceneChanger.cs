using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string _nextSceneName = string.Empty;
    public void LoadNewScene()
    {
        SceneManager.LoadScene("LoadingScene");
        GameData.Instance.SetCurrentScene(_nextSceneName);
    }
}
