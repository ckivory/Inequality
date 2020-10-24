using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : ColumnController
{
    public Image background;

    // Optional
    public Text Title;

    

    // Float for how much space should be at the edges of the window and around the title
    public float edgeMargin;

    // Float for how much space should be between rows
    public float rowMargin;

    Vector2 ContentDimensions()
    {
        float contentWidth = 0f;
        float contentHeight = 0f;

        if (Title != null)
        {
            contentWidth = Title.GetComponent<RectTransform>().rect.width;
            contentHeight = Title.GetComponent<RectTransform>().rect.height;
        }

        foreach (GameObject row in content)
        {
            Rect rowRect = row.GetComponent<RectTransform>().rect;
            contentWidth = Mathf.Max(contentWidth, rowRect.width);
            contentHeight += rowRect.height;
        }
        
        return new Vector2(contentWidth, contentHeight);
    }

    public void PositionElements()
    {
        Vector2 dimensions = ContentDimensions();

        // Edge margins
        float windowWidth = dimensions.x + (edgeMargin * 2);
        float windowHeight = dimensions.y + (edgeMargin * 2);

        // Extra edge margin between title and first row
        if(Title != null && content.Count > 0)
        {
            windowHeight += edgeMargin;
        }

        // Row margins
        if (content.Count > 1)
        {
            windowHeight += rowMargin * (content.Count - 1);
        }

        transform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.sizeDelta = new Vector2(windowWidth, windowHeight);
        GetComponent<RectTransform>().sizeDelta = background.rectTransform.sizeDelta;

        // Place Title
        float nextElementY = windowHeight / 2 - edgeMargin;
        if (Title != null && content.Count > 0)
        {
            float halfTitleHeight = Title.preferredHeight / 2;
            nextElementY -= halfTitleHeight;
            Title.transform.localPosition = new Vector2(0f,  nextElementY);
            nextElementY -= halfTitleHeight;
            nextElementY -= edgeMargin;
        }

        // Place row elements
        foreach (GameObject row in content)
        {
            WindowController rowWC = row.GetComponent<WindowController>();
            if (rowWC != null)
            {
                rowWC.PositionElements();
            }

            float halfElementHeight = row.GetComponent<RectTransform>().rect.height / 2;
            nextElementY -= halfElementHeight;
            row.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY -= halfElementHeight;
            nextElementY -= rowMargin;
        }
    }

    public void InitializeWindow()
    {
        foreach (GameObject row in content)
        {
            WindowController rowWC = row.GetComponent<WindowController>();
            if (rowWC != null)
            {
                rowWC.parent = this.gameObject;
                rowWC.InitializeWindow();
            }
        }

        PositionElements();
    }
}
