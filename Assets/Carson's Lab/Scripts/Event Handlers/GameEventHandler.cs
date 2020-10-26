using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public int buttonValue;

    public void TabClicked()
    {
        int tabIndex = gameObject.GetComponent<TabController>().tabIndex;
        GameObject.FindObjectOfType<PortfolioController>().SetActivePanel(tabIndex);
    }

    // buttonValue should represent which stock was bought or sold
    // Determine whether or not the player is allowed to buy or sell it, and then let them or not
    public void BuyButtonClicked()
    {
        Debug.Log("Bought stock number " + buttonValue);
    }

    public void SellButtonClicked()
    {
        Debug.Log("Sold stock number " + buttonValue);
    }

    public void AcknowledgeButtonClicked()
    {
        GameManager.Instance.popup.gameObject.SetActive(false);
    }

    public void EndTurnButtonClicked()
    {
        GameManager.Instance.NextTurn();
    }
}
