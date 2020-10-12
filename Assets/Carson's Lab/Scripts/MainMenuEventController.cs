using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEventController : MonoBehaviour
{
    public void StartButtonPushed()
    {
        SceneManager.LoadScene("Portfolio");
    }

    public void OptionsButtonPushed()
    {
        SceneManager.LoadScene("Options Menu");
    }

    public void ExitButtonPushed()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
