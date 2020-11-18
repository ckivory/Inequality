using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnLayout2 : LayoutController2
{
    public override void RepositionElements()
    {
        this.GetComponent<RectTransform>().sizeDelta = size;
        this.transform.localPosition = pos;

        int numContentMargins = content.Count - 1;
        Vector2 contentRealEstate = new Vector2(this.size.x - (this.edgeMargin * 2), this.size.y - (this.edgeMargin * 2) - (this.contentMargin * numContentMargins));

        float nextElementY = this.size.y / 2 - this.edgeMargin;
        for (int elementIndex = 0; elementIndex < content.Count; elementIndex++)
        {
            GameObject element = content[elementIndex];

            if (gameObject.name == "Column 2")
            {
                Debug.Log("Positioning " + element.name);
            }

            Vector2 elementSize = new Vector2(contentRealEstate.x, contentRealEstate.y * sizeFraction(elementIndex));
            nextElementY -= elementSize.y / 2;

            LayoutController2 LC = element.GetComponent<LayoutController2>();
            TextBoxController2 TB = element.GetComponent<TextBoxController2>();
            if (LC != null)
            {
                LC.PositionElements(elementSize, new Vector2(0f, nextElementY));
            }
            else if (TB != null)
            {
                TB.size = elementSize;
                TB.pos = new Vector2(0f, nextElementY);
                TB.FormatText();
            }
            else
            {
                element.GetComponent<RectTransform>().sizeDelta = elementSize;
                element.transform.localPosition = new Vector2(0f, nextElementY);
            }

            nextElementY -= elementSize.y / 2;
            nextElementY -= this.contentMargin;
        }
    }
}
