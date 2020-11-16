using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioController2 : LayoutController2
{
    public List<GameObject> tabs;
    public List<GameObject> windows;

    public float tabOverlap;

    [HideInInspector]
    public int activeWindow = -1;

    public override void PositionElements(Vector2 newSize, Vector2 newPos)
    {
        this.size = newSize;
        this.pos = newPos;

        if(activeWindow == -1)
        {
            throw new Exception("Active Window is not set. Make sure you set it before the elements get positioned for the first time.");
        }

        GetComponent<RectTransform>().sizeDelta = size;
        transform.localPosition = pos;

        for (int windowIndex = 0; windowIndex < windows.Count; windowIndex++)
        {
            GameObject window = windows[windowIndex];

            if (windowIndex == activeWindow)
            {
                window.SetActive(true);
                WindowLayout2 WL = window.GetComponent<WindowLayout2>();
                if (WL != null)
                {
                    WL.PositionElements(size, new Vector2(0f, 0f));
                }
                else
                {
                    throw new Exception("No window layout script attached");
                }
            }
            else
            {
                window.SetActive(false);
            }
        }
    }
}
