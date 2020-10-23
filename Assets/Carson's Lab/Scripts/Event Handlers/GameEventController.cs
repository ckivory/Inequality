using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    public void TabClicked()
    {
        int tabIndex = gameObject.GetComponent<TabController>().tabIndex;
        GameObject.FindObjectOfType<PortfolioController>().SetActiveWindow(tabIndex);
    }
}
