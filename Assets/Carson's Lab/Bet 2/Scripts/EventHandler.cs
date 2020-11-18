using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public void SwitchTabs()
    {
        // Should only be called from tab face button
        // Should never occur before PC is set

        PortfolioController2 PC = GameObject.FindObjectOfType<PortfolioController2>();
        TabController2 TC = GetComponent<TabController2>();
        if(TC != null)
        {
            int tabIndex = PC.tabs.IndexOf(TC);
            PC.SetWindow(tabIndex);
            PC.RepositionElements();
        }
    }

    public void DummyEvent()
    {
        Debug.Log("Event handled by " + gameObject.name);
    }
}
