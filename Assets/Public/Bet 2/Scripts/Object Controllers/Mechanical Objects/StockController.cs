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


    public int stockIndex;

    protected int amount;
    public int price;

    public float growthMin;
    public float growthMax;

    public float dividendPercent;

    public TextBoxController2 amountText;
    public TextBoxController2 priceText;

    public ButtonController buyButton;
    public ButtonController sellButton;

    /*
    public float growthRate;
    public float payoutPercent;
    */

    public void UpdateText()
    {
        amountText.SetText("Shares: " + amount.ToString());
        priceText.SetText("Per Stock: $" + price.ToString());
    }

    public void UpdateButtons()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();

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

    public void ResetAmount()
    {
        amount = 0;
    }

    public void IncreaseAmount()
    {
        amount++;
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
    }

    public void BuyStock()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();

        if (player.GetWealth() >= price * amount)
        {
            player.ChangeStocks(stockIndex, amount);
            player.ChangeWealth(-1 * amount * price);
        }
    }

    public void SellStock()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();

        if (player.GetStocks(stockIndex) >= amount)
        {
            player.ChangeStocks(stockIndex, -1 * amount);
            player.ChangeWealth(amount * price);
        }
    }

    public void Grow()
    {
        float growthRate = Random.Range(growthMin, growthMax);
        price = (int)(price * (1 + growthRate));
    }

    public int DividendAmount()
    {
        return (int)(price * dividendPercent);
    }

    public void Update()
    {
        UpdateText();
        UpdateButtons();
    }
}
