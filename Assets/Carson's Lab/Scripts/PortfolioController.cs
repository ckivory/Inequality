using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioController : MonoBehaviour
{
    // Reference to parent, to be set by the parent
    [HideInInspector]
    public GameObject parent;

    // TODO: Using a list of windows, display one at a time depending on which tab was most recently clicked
    public List<WindowController> windows;

    WindowController activeWindow;

    public void SetActiveWindow(int windowIndex)
    {   
        activeWindow = windows[windowIndex];
        activeWindow.transform.SetAsFirstSibling();
    }

    public void InitializeWindows()
    {
        int i = 0;
        foreach (WindowController window in windows)
        {
            window.parent = gameObject;
            window.InitializeWindow();
            window.tab.SendMessage("PositionTab", new int[] { windows.Count, i });
            i++;
        }

        SetActiveWindow(0);
    }

    public void UpdateMainWindow()
    {
        activeWindow.PositionElements();
    }
}
