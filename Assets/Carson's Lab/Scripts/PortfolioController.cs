﻿using System;
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
        }

        activeWindow = windows[windowIndex];
        activeWindow.transform.SetAsLastSibling();
        tabs[windowIndex].tabFace.GetComponent<Button>().interactable = false;

        foreach (WindowController window in windows)
        {
            window.gameObject.SetActive(false);
        }
        activeWindow.gameObject.SetActive(true);
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
            window.InitializeWindow();
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
        else
        {
            Debug.Log("Active window is null");
        }

        foreach(TabController tab in tabs)
        {
            tab.RepositionTab();
        }
    }
}
