using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public WindowLayout2 MenuWindow;

    public void Start()
    {
        MenuWindow.PlaceElement(new Vector2(500f, 500f), new Vector2(0f, 0f));
    }
}
