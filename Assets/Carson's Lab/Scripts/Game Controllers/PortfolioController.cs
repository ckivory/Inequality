using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioController : MonoBehaviour
{
    // Reference to parent, to be set by the parent
    [HideInInspector]
    public GameObject parent;

    public List<WindowController> windows;
    public List<TabController> tabs;
    public float tabHeight;

    public float borderWidth;

    WindowController activeWindow;

    public void SetActiveWindow(int windowIndex)
    {
        if(windowIndex < 0 || windowIndex > windows.Count - 1)
        {
            throw new Exception("Cannot switch to a window that doesn't exist");
        }

        if(activeWindow != null)
        {
            tabs[windows.IndexOf(activeWindow)].tabFace.GetComponent<Button>().interactable = true;

            // Move most recent tab to front of all borders, but back of main window
            tabs[windows.IndexOf(activeWindow)].tabFace.transform.SetSiblingIndex(tabs.Count);
        }

        activeWindow = windows[windowIndex];
        activeWindow.transform.SetAsLastSibling();
        tabs[windowIndex].tabFace.GetComponent<Button>().interactable = false;

        foreach (WindowController window in windows)
        {
            window.gameObject.SetActive(false);
        }
        activeWindow.gameObject.SetActive(true);

        

        // Move current tab face to front of portfolio's children
        tabs[windowIndex].tabFace.transform.SetAsLastSibling();
    }

    public WindowController GetActiveWindow()
    {
        return activeWindow;
    }

    public void InitializeWindows()
    {
        for(int i = 0; i < windows.Count; i++)
        {
            WindowController window = windows[i];
            window.parent = gameObject;
            window.InitializeLayout();
            i++;
        }

        SetActiveWindow(0);

        for(int i = 0; i < tabs.Count; i++)
        {
            TabController tab = tabs[i];
            tab.PC = this;
            tab.DeployTab();
            tab.PositionTab(tabs.Count, i);
        }
    }

    public void UpdateMainWindow()
    {
        if(activeWindow != null)
        {
            activeWindow.PositionElements();
        }

        foreach(TabController tab in tabs)
        {
            tab.RepositionTab();

            if (tab.tabIndex > -1)
            {
                tab.tabLabel.SetFontSize(tab.tabLabel.preferredFontSize);
            }
        }
    }
}
