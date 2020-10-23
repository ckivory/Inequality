using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    public Vector2 maxSize;
    [HideInInspector]
    public Vector2 containerSize;

    public bool shrinkFontOnOverflow;

    public int preferredFontSize;

    protected Text textComp;
    protected RectTransform boxRect;

    protected string boxContent;

    protected bool LineOverflow()
    {
        return textComp.preferredWidth > boxRect.rect.width;
        // Maybe boundingSize.x?
    }

    protected bool BoxOverflow()
    {
        return textComp.preferredHeight > boxRect.rect.height;
    }

    // Given some boxContent and the current font size, either fit the box to the content or set the text to an ellipsized version of the content
    public void FormatText()
    {
        Vector2 boundingSize = new Vector2(Mathf.Min(maxSize.x, containerSize.x), Mathf.Min(maxSize.y, containerSize.y));
        // Reach max width before wrapping
        while (boxRect.rect.width < boundingSize.x && LineOverflow())
        {
            boxRect.sizeDelta = new Vector2(boxRect.rect.width + 1, boxRect.rect.height);
        }

        // Reach max height before ellipsizing
        while(boxRect.rect.height < boundingSize.y && BoxOverflow())
        {
            boxRect.sizeDelta = new Vector2(boxRect.rect.width, boxRect.rect.height + 1);
        }

        // Enforce max size
        boxRect.sizeDelta = new Vector2(Mathf.Min(boxRect.sizeDelta.x, boundingSize.x), Mathf.Min(boxRect.sizeDelta.y, boundingSize.y));

        if (shrinkFontOnOverflow)
        {
            textComp.text = boxContent;
            while (BoxOverflow() && textComp.fontSize > 10)
            {
                SetFontSize(textComp.fontSize - 1);
            }
        }
        if(!shrinkFontOnOverflow || textComp.fontSize <= 10)
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

    public void InitializeTextBox()
    {
        textComp = GetComponent<Text>();
        boxRect = GetComponent<RectTransform>();
        boxContent = textComp.text;

        if (maxSize == Vector2.zero)
        {
            maxSize = boxRect.sizeDelta;
        }
        containerSize = maxSize;

        if (preferredFontSize == 0)
        {
            preferredFontSize = textComp.fontSize;
        }

        FormatText();
    }

    void Start()
    {
        InitializeTextBox();
    }
}
