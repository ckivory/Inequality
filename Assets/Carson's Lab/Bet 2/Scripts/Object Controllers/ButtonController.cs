using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : ElementController
{
    public TextBoxController2 textBox;

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
            textBox.PlaceElement(this.size, new Vector2(0f, 0f));
        }
        catch (Exception)
        {
            // Debug.Log("No text box");
        }
    }
}
