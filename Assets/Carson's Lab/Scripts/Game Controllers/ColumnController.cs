using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnController : LayoutController
{
    public override Vector2 ContentDimensions()
    {
        float contentWidth = 0f;
        float contentHeight = 0f;

        foreach (GameObject row in content)
        {
            TextBoxController rowTBC = row.GetComponent<TextBoxController>();
            Vector2 rowSize = row.GetComponent<RectTransform>().sizeDelta;

            if (rowTBC != null)
            {
                rowSize = rowTBC.GetEffectiveSize();
            }

            contentWidth = Mathf.Max(contentWidth, rowSize.x);
            contentHeight += rowSize.y;
        }

        return new Vector2(contentWidth, contentHeight);
    }

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


        // Row margins
        if (content.Count > 1)
        {
            windowHeight += contentMargin * (content.Count - 1);
        }

        // transform.localPosition = new Vector2(0f, 0f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(windowWidth, windowHeight);
    }

    public override float PositionElements()
    {   
        // Place row elements
        float nextElementY = -1 * (GetComponent<RectTransform>().sizeDelta.y / 2) + edgeMargin;
        for (int rowIndex = content.Count - 1; rowIndex >= 0; rowIndex--)
        {
            GameObject row = content[rowIndex];
            float halfElementHeight = row.GetComponent<RectTransform>().rect.height / 2;
            nextElementY += halfElementHeight;
            row.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY += halfElementHeight;

            // Add either row margin or edge margin depending on if we are at the top element
            nextElementY += (rowIndex > 0 ? contentMargin : edgeMargin);
        }
        return nextElementY;
    }
}
