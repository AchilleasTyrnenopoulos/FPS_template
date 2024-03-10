using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _newGameBtn;
    [SerializeField] private SceneChanger _sceneChanger;

    private void OnEnable()
    {
        _newGameBtn?.Select();
        _sceneChanger = GetComponent<SceneChanger>();
    }

    public void NewGame()
    {
        //SceneManager.LoadSceneAsync(1);
        // call SceneChanger
        _sceneChanger.LoadNewScene();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        //stop play
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
