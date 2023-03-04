using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _resumeButton;
    private void OnEnable()
    {
        var button =  _resumeButton.GetComponent<Button>();
        button.Select();
    }    

    public void ResumeGame()
    {
        GameManager.Instance.TogglePauseGame();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        //stop play
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
