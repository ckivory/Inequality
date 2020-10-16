using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public WindowController mainWindow;

    // Start is called before the first frame update
    void Start()
    {
        mainWindow.InitializeWindow();
    }

    // Update is called once per frame
    void Update()
    {
        mainWindow.PositionElements();
    }
}
