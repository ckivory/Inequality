using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A panel to be used as one tab of a portfolio. Like a window, but it must be the same size as every other panel, and it should resize its content to fill the available space
public class PanelLayout : WindowLayout
{
    private PortfolioController parentPC;

    private Vector2 scaleVector;

    public override void SetConstraints()
    {
        base.SetConstraints();

        if(parentPC == null)
        {
            parentPC = parent.GetComponent<PortfolioController>();
        }
        Vector2 targetSize = parentPC.panelSize;
        scaleVector = new Vector2(targetSize.x / rt.sizeDelta.x, targetSize.y / rt.sizeDelta.y);
        Debug.Log("Scale Vector: " + scaleVector);  // Making sure it doesn't instantly become 1 after the first time running
        background.rectTransform.localPosition = new Vector2(0f, 0f);

        // Set size of both window and background image
        background.rectTransform.sizeDelta = targetSize;
        rt.sizeDelta = targetSize;

        foreach (GameObject element in content)
        {
            LayoutController elementLC = element.GetComponent<LayoutController>();
            if (elementLC != null)
            {
                elementLC.RepositionElements();
            }
        }
    }

    public override float PositionElements()
    {
        float virtualEdgeMargin = edgeMargin * scaleVector.y;
        float virtualContentMargin = contentMargin * scaleVector.y;

        // A lot like the column layout version, but with the target panel size instead of the actual window size
        float nextElementY = -1 * (parentPC.panelSize.y / 2) + virtualEdgeMargin;
        for (int rowIndex = content.Count - 1; rowIndex >= 0; rowIndex--)
        {
            GameObject row = content[rowIndex];
            float halfElementHeight = row.GetComponent<RectTransform>().rect.height / 2 * scaleVector.y;

            TextBoxController rowTBC = row.GetComponent<TextBoxController>();
            if (rowTBC != null)
            {
                halfElementHeight = rowTBC.GetEffectiveSize().y / 2 * scaleVector.y;
            }

            nextElementY += halfElementHeight;
            row.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY += halfElementHeight;

            // Add either row margin or edge margin depending on if we are at the top element
            if (rowIndex <= 0)
            {
                nextElementY += virtualEdgeMargin;
            }
            else
            {
                nextElementY += virtualContentMargin;
            }

        }
        return nextElementY;
    }
}
