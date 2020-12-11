using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStocksWindow : MonoBehaviour
{
    public List<TextBoxController2> quantityTexts;
    public List<TextBoxController2> priceTexts;

    void Update()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();
        List<StockController> stocks = GameManager2.Instance.stockOptions;

        for(int i = 0; i < 3; i++)
        {
            quantityTexts[i].SetText(player.GetStocks(i).ToString());
            priceTexts[i].SetText("$" + stocks[i].price.ToString());
        }
    }
}
