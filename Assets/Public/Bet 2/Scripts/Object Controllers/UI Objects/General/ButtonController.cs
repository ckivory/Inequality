using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class ButtonController : ElementController
{
    public TextBoxController2 textBox;

    public void SetListener(UnityAction listener)
    {
        GetOnClick().RemoveAllListeners();
        GetOnClick().AddListener(listener);
    }

    public ButtonClickedEvent GetOnClick()
    {
        return GetComponent<Button>().onClick;
    }

    public void SetInteractible(bool newState)
    {
        GetComponent<Button>().interactable = newState;
    }

    public void BuyButtonPressed()
    {
        Debug.Log("Buy!");
    }

    public void SellButtonPressed()
    {
        Debug.Log("Sell!");
    }

    public override void UpdateElement()
    {
        // Update text box if it exists
        try
        {
            textBox.alignment = TextAnchor.MiddleCenter;
            textBox.PlaceElement(this.size, new Vector2(0f, 0f));
        }
        catch (Exception)
        {
            // Debug.Log("No text box");
        }
    }
}
