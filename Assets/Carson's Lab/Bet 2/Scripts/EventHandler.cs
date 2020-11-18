using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    // Can act as a different enum for each event type
    public int buttonValue;

    // For some reason this isn't working at the moment.
    public void SwitchTabs()
    {
        // Should only be called from tab face button
        // Should never occur before PC is set

        Debug.Log("Clicking!");
        PortfolioController2 PC = GameObject.FindObjectOfType<PortfolioController2>();
        PC.SetWindow(buttonValue);
        PC.RepositionElements();
    }
}
