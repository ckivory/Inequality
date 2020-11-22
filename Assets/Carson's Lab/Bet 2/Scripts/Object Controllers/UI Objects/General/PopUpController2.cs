using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController2 : MonoBehaviour
{
    public TextBoxController2 title;

    public WindowLayout2 popupForeground;
    public ImageBoxController eventImage;
    public TextBoxController2 popupTitle;
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

    public void SetImageEnabled(bool enabled)
    {
        popupForeground.content = new List<GameObject>();
        popupForeground.relativeSizes = new List<int>();

        eventImage.gameObject.SetActive(false);

        if(enabled)
        {
            eventImage.gameObject.SetActive(true);
            popupForeground.content.Add(eventImage.gameObject);
            popupForeground.relativeSizes.Add(2);
        }

        popupForeground.content.Add(popupTitle.gameObject);
        popupForeground.relativeSizes.Add(1);

        popupForeground.content.Add(buttonRow.gameObject);
        popupForeground.relativeSizes.Add(1);

        popupForeground.UpdateElement();
    }

    public void SetImage(Sprite newImage)
    {
        SetImageEnabled(true);
        eventImage.image.sprite = newImage;
    }
}
