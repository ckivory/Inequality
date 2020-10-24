using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : ColumnController
{
    public Image background;

    // Optional
    public TextBoxController Title;

    public override Vector2 ContentDimensions()
    {
        // Get dimensions of contents
        Vector2 contentDimensions = base.ContentDimensions();
        float contentWidth = contentDimensions.x;
        float contentHeight = contentDimensions.y;

        // Adjust for title if necessary
        if (Title != null)
        {
            Vector2 titleSize = Title.GetEffectiveSize();
            contentWidth = Mathf.Max(contentWidth, titleSize.x);
            contentHeight += titleSize.y;
        }
        
        return new Vector2(contentWidth, contentHeight);
    }

    /*
    * To leverage the ColumnController positioning function, I could try to reconfigure the process so that it starts with a reference to
    * the bottom of the window and then places in order towards the top, which would allow me to do that whole process for the WindowController
    * just as easily. So step 1 is calculate all relevant constraints, which will be different for the two controller types. Step 1 is to use the
    * base PositionElements method to place all elements except for the title, which will then be placed in step 3.
    */

    public override void SetConstraints()
    {
        // Tell children to recursively set their constraints before doing so yourself
        foreach (GameObject row in content)
        {
            LayoutController rowLC = row.GetComponent<LayoutController>();
            if (rowLC != null)
            {
                rowLC.RepositionElements();
            }
        }

        // Figure out size of content
        Vector2 dimensions = ContentDimensions();

        // Edge margins
        float windowWidth = dimensions.x + (edgeMargin * 2);
        float windowHeight = dimensions.y + (edgeMargin * 2);

        // Extra edge margin between title and first row
        if (Title != null && content.Count > 0)
        {
            windowHeight += edgeMargin;
        }

        // Row margins
        if (content.Count > 1)
        {
            windowHeight += contentMargin * (content.Count - 1);
        }

        // transform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.sizeDelta = new Vector2(windowWidth, windowHeight);
        GetComponent<RectTransform>().sizeDelta = background.rectTransform.sizeDelta;
    }

    public override float PositionElements()
    {
        // Position all content elements and return the offset value to start at for any child-specific elements
        float nextElementY = base.PositionElements();

        // Place Title
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
