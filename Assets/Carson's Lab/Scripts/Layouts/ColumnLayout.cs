using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnLayout : LayoutController
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

            contentWidth = Mathf.Max(contentWidth, rowSize.x);
            contentHeight += rowSize.y;
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
        float columnWidth = dimensions.x + (edgeMargin * 2);
        float columnHeight = dimensions.y + (edgeMargin * 2);

        // Row margins
        if (content.Count > 1)
        {
            columnHeight += contentMargin * (content.Count - 1);
        }

        // Set size of resulting layout
        GetComponent<RectTransform>().sizeDelta = new Vector2(columnWidth, columnHeight);
    }


    // Going from bottom to top, place one element at a time, then return the next legal element offset
    public override float PositionElements()
    {
        // Place row elements
        float nextElementY = -1 * (GetComponent<RectTransform>().sizeDelta.y / 2) + edgeMargin;
        for (int rowIndex = content.Count - 1; rowIndex >= 0; rowIndex--)
        {
            GameObject row = content[rowIndex];
            float halfElementHeight = row.GetComponent<RectTransform>().rect.height / 2;

            TextBoxController rowTBC = row.GetComponent<TextBoxController>();
            if(rowTBC != null)
            {
                halfElementHeight = rowTBC.GetEffectiveSize().y / 2;
            }

            nextElementY += halfElementHeight;
            row.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY += halfElementHeight;

            // Add either row margin or edge margin depending on if we are at the top element
            if(rowIndex <= 0)
            {
                nextElementY += edgeMargin;
            }
            else
            {
                nextElementY += contentMargin;
            }
            
        }
        return nextElementY;
    }
}
