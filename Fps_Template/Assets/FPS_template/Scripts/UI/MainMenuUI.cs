using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
