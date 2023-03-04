using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _newGameBtn;

    private void OnEnable()
    {
        _newGameBtn?.Select();
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
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
