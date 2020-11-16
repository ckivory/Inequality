using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowLayout2 : LayoutController2
{
    public override void PositionElements(Vector2 newSize, Vector2 newPos)
    {
        size = newSize;
        pos = newPos;

        this.GetComponent<RectTransform>().sizeDelta = size;
        this.transform.localPosition = pos;

        int numContentMargins = content.Count - 1;
        Vector2 contentRealEstate = new Vector2(this.size.x - (this.edgeMargin * 2) - (this.contentMargin * numContentMargins), this.size.y - (this.edgeMargin * 2));

        float nextElementX = (-1 * this.size.x / 2) + this.edgeMargin;
        for (int elementIndex = 0; elementIndex < content.Count; elementIndex++)
        {
            GameObject element = content[elementIndex];

            Vector2 elementSize = new Vector2(contentRealEstate.x * sizeFraction(elementIndex), contentRealEstate.y);
            nextElementX += elementSize.x / 2;

            LayoutController2 LC = element.GetComponent<LayoutController2>();
            TextBoxController2 TB = element.GetComponent<TextBoxController2>();
            if (LC != null)
            {
                LC.PositionElements(elementSize, new Vector2(nextElementX, 0f));
            }
            else if (TB != null)
            {
                TB.size = elementSize;
                TB.pos = new Vector2(nextElementX, 0f);
                TB.FormatText();
            }

            nextElementX += elementSize.x / 2;
            nextElementX += this.contentMargin;
        }
    }
}
