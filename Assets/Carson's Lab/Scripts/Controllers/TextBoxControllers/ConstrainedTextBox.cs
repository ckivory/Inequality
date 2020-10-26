using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstrainedTextBox : TextBoxController
{
    public bool shrinkFontOnOverflow;

    public Vector2 maxSize;
    [HideInInspector]
    public Vector2 containerSize;

    // Given some boxContent and the current font size, either fit the box to the content or set the text to an ellipsized version of the content
    public override void FormatText()
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

    public override void SetText(string newText)
    {
        boxContent = newText;
        textComp.text = boxContent;
        FormatText();
    }

    public override void InitializeTextBox()
    {
        textComp = GetComponent<Text>();
        boxRect = GetComponent<RectTransform>();
        boxContent = textComp.text;

        textComp.alignment = TextAnchor.MiddleCenter;

        if (preferredFontSize == 0)
        {
            preferredFontSize = textComp.fontSize;
        }

        if (maxSize == Vector2.zero)
        {
            maxSize = boxRect.sizeDelta;
        }
        containerSize = maxSize;

        FormatText();
    }
}
