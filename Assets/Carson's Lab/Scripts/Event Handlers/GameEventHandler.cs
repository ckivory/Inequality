using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public void TabClicked()
    {
        int tabIndex = gameObject.GetComponent<TabController>().tabIndex;
        GameObject.FindObjectOfType<PortfolioController>().SetActiveWindow(tabIndex);
    }

    // This is where I will put the event handlers for the game buttons

}
