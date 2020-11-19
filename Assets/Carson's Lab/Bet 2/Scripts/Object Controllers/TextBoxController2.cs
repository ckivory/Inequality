using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController2 : ElementController
{
    public string text;
    public int preferredFontSize;
    public int minFontSize;

    private Text textComp;

    public void EllipsizeText()
    {
        string displayText = text;

        while (textComp.preferredHeight > size.y && displayText.Length > 0)
        {
            displayText = displayText.Substring(0, displayText.Length - 1);
            textComp.text = displayText + "...";
        }
    }


    public void ConstrainText()
    {
        while(textComp.preferredHeight > size.y)
        {
            int fontSize = textComp.fontSize;
            if(fontSize > minFontSize)
            {
                textComp.fontSize = fontSize - 1;
            }
            else
            {
                EllipsizeText();
            }
        }
    }


    public override void UpdateElement()
    {
        textComp = GetComponent<Text>();
        textComp.text = text;
        textComp.fontSize = preferredFontSize;
        textComp.alignment = TextAnchor.MiddleCenter;
        

        ConstrainText();
    }
}
