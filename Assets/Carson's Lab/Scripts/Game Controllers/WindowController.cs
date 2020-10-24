using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : ColumnController
{
    public Image background;

    // Optional
    public TextBoxController Title;

    Vector2 ContentDimensions()
    {
        float contentWidth = 0f;
        float contentHeight = 0f;

        if (Title != null)
        {
            Vector2 titleSize = Title.GetEffectiveSize();
            contentWidth = titleSize.x;
            contentHeight = titleSize.y;
        }

        foreach (GameObject row in content)
        {
            TextBoxController rowTBC = row.GetComponent<TextBoxController>();
            Vector2 rowSize = row.GetComponent<RectTransform>().sizeDelta;

            if (rowTBC != null)
            {
                Text rowText = rowTBC.GetComponent<Text>();
                rowSize = rowTBC.GetEffectiveSize();
                Debug.Log("Row Size: " + rowSize);
            }
            
            contentWidth = Mathf.Max(contentWidth, rowSize.x);
            contentHeight += rowSize.y;
        }
        
        return new Vector2(contentWidth, contentHeight);
    }

    public void PositionElements()
    {
        // Tell children to recursively set their constraints before doing so yourself
        foreach(GameObject row in content)
        {
            WindowController rowWC = row.GetComponent<WindowController>();
            if (rowWC != null)
            {
                rowWC.PositionElements();
            }
        }

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
            windowHeight += contentMargin * (content.Count - 1);
        }

        transform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.sizeDelta = new Vector2(windowWidth, windowHeight);
        GetComponent<RectTransform>().sizeDelta = background.rectTransform.sizeDelta;

        // Place Title
        float nextElementY = windowHeight / 2 - edgeMargin;
        if (Title != null)
        {
            float halfTitleHeight = Title.GetEffectiveSize().y / 2;
            nextElementY -= halfTitleHeight;
            Title.transform.localPosition = new Vector2(0f,  nextElementY);
            nextElementY -= halfTitleHeight;

            if (content.Count > 0)
            {
                nextElementY -= edgeMargin;
            }
        }

        // Place row elements
        foreach (GameObject row in content)
        {
            float halfElementHeight = row.GetComponent<RectTransform>().rect.height / 2;
            nextElementY -= halfElementHeight;
            row.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY -= halfElementHeight;
            nextElementY -= contentMargin;
        }
    }

    public override void InitializeLayout()
    {
        foreach (GameObject row in content)
        {
            WindowController rowWC = row.GetComponent<WindowController>();
            if (rowWC != null)
            {
                rowWC.parent = this.gameObject;
                rowWC.InitializeLayout();
            }
        }

        PositionElements();
    }
}
