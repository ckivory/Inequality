using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioController2 : LayoutController2
{
    public List<TabController2> tabs;
    public List<WindowLayout2> windows;

    public float tabHeight;
    public float tabOverlap;

    [HideInInspector]
    public int activeWindow = -1;

    public void SetWindow(int newActiveWindow)
    {
        activeWindow = newActiveWindow;
    }

    public float RealTabHeight()
    {
        TabController2 TC = tabs[0].GetComponent<TabController2>();
        float actualHeight = this.tabHeight + TC.NeckHeight();
        actualHeight += TC.NeckHeight();
        actualHeight /= TC.GetResolution();
        return actualHeight;
    }

    private void PositionWindows()
    {
        TabController2 TC = tabs[0].GetComponent<TabController2>();
        float windowHeight = size.y - RealTabHeight() + (TC.NeckHeight() / TC.GetResolution());
        float windowY = -1 * (size.y - windowHeight) / 2;

        for (int windowIndex = 0; windowIndex < windows.Count; windowIndex++)
        {
            WindowLayout2 window = windows[windowIndex];

            if (windowIndex == activeWindow)
            {
                window.gameObject.SetActive(true);
                
                Debug.Log("Portfolio height: " + this.size.y);
                Debug.Log("Tab height: " + this.tabHeight);
                Debug.Log("Window Height: " + windowHeight);
                Debug.Log("Window Y: " + windowY);

                window.PositionElements(new Vector2(size.x, windowHeight), new Vector2(0f, windowY));
                
            }
            else
            {
                window.gameObject.SetActive(false);
            }
        }
    }

    private void PositionTabs()
    {
        // To make the tabs overlap, they have to be equal to their proportional width plus a specific fraction of the overlap
        Vector2 tabSize = new Vector2(
            (this.size.x / tabs.Count) + (this.tabOverlap * (tabs.Count - 1) / tabs.Count),
            this.tabHeight
        );

        float realHeight = RealTabHeight();
        float tabY = (this.size.y / 2) - (realHeight / 2);

        float nextTabX = -1 * (this.size.x / 2);
        for (int tabIndex = 0; tabIndex < tabs.Count; tabIndex++)
        {
            TabController2 tab = tabs[tabIndex];

            nextTabX += tabSize.x / 2;

            Vector2 realSize = new Vector2(tabSize.x, realHeight);
            Vector2 tabPos = new Vector2(nextTabX, tabY);
            tab.PositionTab(realSize, tabPos, this, (tabIndex == activeWindow));

            nextTabX += (tabSize.x / 2) - tabOverlap;
        }
        
    }

    public void RepositionElements()
    {
        if (activeWindow == -1)
        {
            Debug.Log("Active Window is not set. Make sure you set it before the elements get positioned for the first time.");
            activeWindow = 0;
        }

        GetComponent<RectTransform>().sizeDelta = size;
        transform.localPosition = pos;

        PositionWindows();
        PositionTabs();
    }

    public override void PositionElements(Vector2 newSize, Vector2 newPos)
    {
        this.size = newSize;
        this.pos = newPos;

        RepositionElements();
    }
}
