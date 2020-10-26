using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowLayout : LayoutController
{
    // Get dimensions of content  as if there were no margins
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

            contentWidth += rowSize.x;
            contentHeight = Mathf.Max(contentHeight, rowSize.y);
        }

        return new Vector2(contentWidth, contentHeight);
    }


    // Set window dimensions to content size + normal margins
    public override void SetConstraints()
    {
        // Recursively set constraints of children
        base.SetConstraints();

        // Figure out size of all content based on child Content Dimensions.
        Vector2 dimensions = this.ContentDimensions();

        // Edge margins
        float rowWidth = dimensions.x + (edgeMargin * 2);
        float rowHeight = dimensions.y + (edgeMargin * 2);

        // Column margins
        if (content.Count > 1)
        {
            rowWidth += contentMargin * (content.Count - 1);
        }

        // Set size of resulting layout
        GetComponent<RectTransform>().sizeDelta = new Vector2(rowWidth, rowHeight);
    }

    // Going from left to right, place one element at a time, then return the next legal element offset
    public override float PositionElements()
    {
        // Place column elements
        float nextElementX = -1 * (GetComponent<RectTransform>().sizeDelta.x / 2) + edgeMargin;
        for  (int colIndex = 0; colIndex < content.Count; colIndex++)
        {
            GameObject col = content[colIndex];
            float halfElementWidth = col.GetComponent<RectTransform>().rect.width / 2;

            TextBoxController rowTBC = col.GetComponent<TextBoxController>();
            if (rowTBC != null)
            {
                halfElementWidth = rowTBC.GetEffectiveSize().x / 2;
            }

            nextElementX += halfElementWidth;
            col.transform.localPosition = new Vector2(nextElementX, 0f);
            nextElementX += halfElementWidth;

            // Add either row margin or edge margin depending on if we are at the top element
            if (colIndex >= content.Count - 1)
            {
                nextElementX += edgeMargin;
            }
            else
            {
                nextElementX += contentMargin;
            }

        }

        return nextElementX;
    }
}
