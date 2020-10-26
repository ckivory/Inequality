using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PortfolioController PC;

    void Start()
    {
        if(Instance != null)
        {
            throw new System.Exception("Cannot have more than one Game Manager at a time.");
        }

        Instance = this;
        PC.InitializePanels();
    }

    public void UpdatePortfolio()
    {
        PC.UpdateMainPanel();
    }

    void Update()
    {
            UpdatePortfolio();   
    }
}
