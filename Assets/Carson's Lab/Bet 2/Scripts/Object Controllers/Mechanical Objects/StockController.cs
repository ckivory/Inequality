using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockController : MonoBehaviour
{
    // Make arrow buttons increase and decrease stock amount
    // Make buy and sell buttons get grayed out if you don't have enough money to buy or enough stock to sell
    // Make buy and sell buttons actually buy and sell
    // Make stock change value after each round
    // Make stock payout happen after every other round

    public TextBoxController2 amountText;
    public TextBoxController2 priceText;

    public ButtonController buyButton;
    public ButtonController sellButton;

    public int stockIndex;

    protected int amount;
    protected int price;

    /*
    public float growthRate;
    public float payoutPercent;
    */

    public void UpdateText()
    {
        amountText.SetText("Shares: " + amount.ToString());
        amountText.UpdateElement();
    }

    public void UpdateButtons()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();

        Debug.Log(player);

        if (player.GetWealth() >= price * amount && amount > 0)
        {
            buyButton.SetInteractible(true);
        }
        else
        {
            buyButton.SetInteractible(false);
        }

        if (player.GetStocks(stockIndex) >= amount && amount > 0)
        {
            sellButton.SetInteractible(true);
        }
        else
        {
            sellButton.SetInteractible(false);
        }
    }

    public void InitializeStock()
    {
        amount = 0;
        UpdateText();
        UpdateButtons();
    }

    public void IncreaseAmount()
    {
        amount++;
        UpdateText();
        UpdateButtons();
    }

    public void DecreaseAmount()
    {
        if (amount > 0)
        {
            amount--;
        }
        else
        {
            amount = 0;
        }

        UpdateText();
        UpdateButtons();
    }

    public void BuyStock()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();

        if (player.GetWealth() >= price * amount)
        {
            Debug.Log("Successfully bought");
        }
        else
        {
            Debug.Log("Unsuccessfully bought");
        }
    }

    public void SellStock()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();

        if (player.GetStocks(stockIndex) >= amount)
        {
            Debug.Log("Successfully sold");
        }
        else
        {
            Debug.Log("Unsuccessfully sold");
        }
    }
}
