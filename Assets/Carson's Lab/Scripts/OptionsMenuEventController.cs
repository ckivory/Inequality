using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuEventController : MonoBehaviour
{
    public List<GameObject> ToggleOptions;

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // For fun to demonstrate toggle options
    public void ToggleToggles()
    {
        bool shouldEnable = GetComponent<Toggle>().isOn;
        foreach (GameObject option in ToggleOptions)
        {
            option.SetActive(shouldEnable);
        }
    }
}
