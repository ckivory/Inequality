using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEventHandler : MonoBehaviour
{
    public void StartButtonPushed()
    {
        if (Context.numGenerations <= 0)
        {
            Context.numGenerations = 3;
        }
        if (Context.numTurnsPerGen <= 0)
        {
            Context.numTurnsPerGen = 4;
        }

        SceneManager.LoadScene("Portfolio");
    }


    public void SetupButtonPushed()
    {
        SceneManager.LoadScene("Game Setup");
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


    public void MenuButtonPushed()
    {
        SceneManager.LoadScene("Main Menu");
    }


    public void onIntegerOptionChanged()
    {
        try
        {
            int.Parse(gameObject.GetComponent<InputField>().text);
        }
        catch (Exception)
        {
            gameObject.GetComponent<InputField>().text = "";
        }
    }


    public void OnGensOptionEdit()
    {
        Context.numGenerations = int.Parse(gameObject.GetComponent<InputField>().text);
    }


    public void OnTurnsOptionEdit()
    {
        Context.numTurnsPerGen = int.Parse(gameObject.GetComponent<InputField>().text);
    }
}