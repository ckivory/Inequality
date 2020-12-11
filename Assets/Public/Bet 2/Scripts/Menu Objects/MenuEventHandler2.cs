using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEventHandler2 : MonoBehaviour
{
    public void StartGame()
    {
        if (Context.numGenerations <= 0)
        {
            Context.numGenerations = 3;
        }
        if (Context.numTurnsPerGen <= 0)
        {
            Context.numTurnsPerGen = 4;
        }

        SceneManager.LoadScene("Portfolio2");
    }

    public void ExitGame()
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
