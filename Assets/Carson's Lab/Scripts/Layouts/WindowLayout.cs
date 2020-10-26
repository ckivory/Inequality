using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowLayout : ColumnLayout
{
    public Image background;

    // Optional
    public ConstrainedTextBox Title;


    // Get dimensions of content and title
    public override Vector2 ContentDimensions()
    {   
        Vector2 contentDimensions = base.ContentDimensions();
        float contentWidth = contentDimensions.x;
        float contentHeight = contentDimensions.y;

        if (Title != null)
        {
            Vector2 titleSize = Title.GetEffectiveSize();
            contentWidth = Mathf.Max(contentWidth, titleSize.x);
            contentHeight += titleSize.y;
        }
        
        return new Vector2(contentWidth, contentHeight);
    }


    // Sets window dimensions including title and extra edge margin
    public override void SetConstraints()
    {
        // Find constraints of children and set sizeDelta to ContentDimensions + normal margins
        base.SetConstraints();

        Vector2 windowSize = GetComponent<RectTransform>().sizeDelta;
        float windowWidth = windowSize.x;
        float windowHeight = windowSize.y;

        // Extra edge margin between title and content if they both exist
        if (Title != null && content.Count > 0)
        {
            windowHeight += edgeMargin;
        }

        // Set size of both window and background image
        background.rectTransform.sizeDelta = new Vector2(windowWidth, windowHeight);
        background.rectTransform.localPosition = new Vector2(0f, 0f);

        // Set size of resulting layout
        GetComponent<RectTransform>().sizeDelta = new Vector2(windowWidth, windowHeight);
    }


    // Uses base method to position content elements from bottom up, then places title
    public override float PositionElements()
    {
        // Position all content elements and return the offset value to start at for any child-specific elements
        float nextElementY = base.PositionElements();

        if (Title != null)
        {
            float halfTitleHeight = Title.GetEffectiveSize().y / 2;
            nextElementY += halfTitleHeight;
            Title.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY += halfTitleHeight;
            nextElementY += edgeMargin;
        }

        return nextElementY;
    }
}
