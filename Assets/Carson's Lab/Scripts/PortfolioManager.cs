using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioManager : MonoBehaviour
{
    public PortfolioController PC;

    // Start is called before the first frame update
    void Start()
    {
        PC.parent = gameObject;
        PC.InitializeWindows();
    }

    // Update is called once per frame
    void Update()
    {
        PC.UpdateMainWindow();
    }
}
