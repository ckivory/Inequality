using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    public Vector2 maxSize;
    public bool shrinkFontOnOverflow;

    private Text textComp;
    private RectTransform boxRect;

    private string boxContent;

    private bool LineOverflow()
    {
        return textComp.preferredWidth > boxRect.rect.width;
    }

    private bool BoxOverflow()
    {
        return textComp.preferredHeight > boxRect.rect.height;
    }

    // Given some boxContent and the current font size, either fit the box to the content or set the text to an ellipsized version of the content
    public void FormatText()
    {
        // Reach max width before wrapping
        while(boxRect.rect.width < maxSize.x && LineOverflow())
        {
            boxRect.sizeDelta = new Vector2(boxRect.rect.width + 1, boxRect.rect.height);
        }

        // Reach max height before ellipsizing
        while(boxRect.rect.height < maxSize.y && BoxOverflow())
        {
            boxRect.sizeDelta = new Vector2(boxRect.rect.width, boxRect.rect.height + 1);
        }

        if(shrinkFontOnOverflow)
        {
            while (BoxOverflow())
            {
                SetFontSize(textComp.fontSize - 1);
            }
        }
        else
        {
            // Ellipsize text
            int i = boxContent.Length - 1;
            while (BoxOverflow())
            {
                textComp.text = boxContent.Substring(0, i) + "...";
                i -= 1;
            }
        }
    }

    public void SetText(string newText)
    {
        boxContent = newText;
        textComp.text = boxContent;
        FormatText();
    }

    public void SetFontSize(int newFontSize)
    {
        if (newFontSize > 0)
        {
            textComp.fontSize = newFontSize;
        }
        FormatText();
    }

    void Start()
    {
        textComp = GetComponent<Text>();
        boxRect = GetComponent<RectTransform>();
        boxContent = textComp.text;
        FormatText();
    }
}
