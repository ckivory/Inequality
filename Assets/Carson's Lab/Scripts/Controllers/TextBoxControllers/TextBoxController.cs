using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TextBoxController : MonoBehaviour
{
    public int preferredFontSize;

    protected Text textComp;
    protected RectTransform boxRect;

    protected string boxContent;


    public virtual Vector2 GetEffectiveSize()
    {
        float textWidth = 0;
        float textHeight = 0;

        if (boxRect != null)
        {
            textWidth = Mathf.Min(boxRect.rect.width, textComp.preferredWidth);
            textHeight = Mathf.Min(boxRect.rect.height, textComp.GetComponent<Text>().preferredHeight);
        }
        return new Vector2(textWidth, textHeight);
    }

    protected virtual bool LineOverflow()
    {
        return textComp.preferredWidth > boxRect.rect.width;
        // Maybe boundingSize.x?
    }

    protected virtual bool BoxOverflow()
    {
        return textComp.preferredHeight > boxRect.rect.height;
    }

    // Given some boxContent and the current font size, either fit the box to the content or set the text to an ellipsized version of the content
    public abstract void FormatText();    

    public abstract void SetText(string newText);

    public virtual void SetFontSize(int newFontSize)
    {
        if (newFontSize > 0)
        {
            textComp.fontSize = newFontSize;
        }
        FormatText();
    }

    public virtual void InitializeTextBox()
    {
        textComp = GetComponent<Text>();
        boxRect = GetComponent<RectTransform>();
        boxContent = textComp.text;

        textComp.alignment = TextAnchor.MiddleCenter;

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
