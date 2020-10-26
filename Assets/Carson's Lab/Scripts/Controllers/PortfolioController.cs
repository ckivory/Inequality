using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioController : MonoBehaviour
{
    // No need for a parent reference, because the parent should always be the Canvas, which has the GameManager attached to it.

    public List<PanelLayout> panels;
    // A single vector that decides how big all panels of the portfolio should be, now and forever
    public Vector2 panelSize;

    public List<TabController> tabs;
    public float tabHeight;

    public float borderWidth;

    PanelLayout activeWindow;

    // Switch the active window to the one given by windowIndex
    public void SetActiveWindow(int windowIndex)
    {
        if(windowIndex < 0 || windowIndex > panels.Count - 1)
        {
            throw new Exception("Cannot switch to a window that doesn't exist");
        }

        if(activeWindow != null)
        {
            tabs[panels.IndexOf(activeWindow)].tabFace.GetComponent<Button>().interactable = true;

            // Move most recent tab to front of all borders, but back of main window
            tabs[panels.IndexOf(activeWindow)].tabFace.transform.SetSiblingIndex(tabs.Count);
        }

        activeWindow = panels[windowIndex];
        activeWindow.transform.SetAsLastSibling();
        tabs[windowIndex].tabFace.GetComponent<Button>().interactable = false;

        foreach (WindowLayout window in panels)
        {
            window.gameObject.SetActive(false);
        }
        activeWindow.gameObject.SetActive(true);

        // Move current tab face to front of portfolio's children
        tabs[windowIndex].tabFace.transform.SetAsLastSibling();
    }


    public WindowLayout GetActiveWindow()
    {
        return activeWindow;
    }


    // Set up the windows and tabs for the beginning of the game.
    public void InitializeWindows()
    {
        foreach(PanelLayout panel in panels)
        {
            panel.parent = gameObject;      // Not being called or something? Maybe it was a reference-vs-value error caused by using a local variable to store the panel
            panel.InitializeLayout();
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


    // Keep window and tabs positioned correctly
    public void UpdateMainWindow()
    {
        if(activeWindow != null)
        {
            activeWindow.RepositionElements();
        }

        foreach(TabController tab in tabs)
        {
            tab.RepositionTab();

            if (tab.tabIndex > -1)
            {
                tab.tabLabel.SetFontSize(tab.tabLabel.preferredFontSize);
            }
        }

        foreach(PanelLayout panel in panels)
        {
            panel.ResizeToFit();
        }
    }
}
