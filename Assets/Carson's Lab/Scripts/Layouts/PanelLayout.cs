using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A panel to be used as one tab of a portfolio. Like a window, but it must be the same size as every other panel, and it should resize its content to fill the available space
public class PanelLayout : WindowLayout
{
    public void ResizeToFit()
    {
        Vector2 sizeToFit = parent.GetComponent<PortfolioController>().panelSize;
        
        Debug.Log("Max size = " + sizeToFit);

        // If too small, create scroll rect
        

        // If too big, stretch content to fill space
    }
}
