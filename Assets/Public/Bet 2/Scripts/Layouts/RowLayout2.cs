using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowLayout2 : LinearLayout
{
    public override Vector2 ContentRealEstate()
    {
        int numContentMargins = content.Count - 1;
        return new Vector2(this.size.x - (this.edgeMargin * 2) - (this.contentMargin * numContentMargins), this.size.y - (this.edgeMargin * 2));
    }


    public override Vector2 ElementSize(int elementIndex)
    {
        Vector2 realEstate = ContentRealEstate();
        return new Vector2(realEstate.x * sizeFraction(elementIndex), realEstate.y);
    }


    public override Vector2 FirstElementPos()
    {
        return new Vector2((-1 * this.size.x / 2) + this.edgeMargin, 0f);
    }


    public override Vector2 CenterElement(Vector2 lastPos, Vector2 elementSize)
    {
        return new Vector2(lastPos.x + (elementSize.x / 2), lastPos.y);
    }


    public override Vector2 NextElementStart(Vector2 lastPos, Vector2 elementSize)
    {
        return new Vector2(lastPos.x + (elementSize.x / 2) + this.contentMargin, lastPos.y);
    }
}
