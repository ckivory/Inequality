using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
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

    public void SetupButton(Vector2 newSize, Vector2 newPos)
    {
        this.GetComponent<RectTransform>().sizeDelta = newSize;
        this.transform.localPosition = newPos;


    }
}
