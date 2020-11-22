using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController2 : MonoBehaviour
{
    public TextBoxController2 title;

    public RowLayout2 buttonRow;
    public List<ButtonController> buttons;

    public void SetButtonNum(int numButtons)
    {
        foreach(GameObject buttonObject in buttonRow.content)
        {
            buttonObject.SetActive(false);
        }

        buttonRow.content = new List<GameObject>();
        buttonRow.relativeSizes = new List<int>();

        for(int buttonIndex = 0; buttonIndex < Mathf.Min(numButtons, buttons.Count); buttonIndex++)
        {
            buttons[buttonIndex].gameObject.SetActive(true);
            buttonRow.content.Add(buttons[buttonIndex].gameObject);
            buttonRow.relativeSizes.Add(1);
        }

        buttonRow.UpdateElement();
    }
}
