using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnLayout2 : LinearLayout
{
    public override Vector2 ContentRealEstate()
    {
        int numContentMargins = content.Count - 1;
        return new Vector2(this.size.x - (this.edgeMargin * 2), this.size.y - (this.edgeMargin * 2) - (this.contentMargin * numContentMargins));
    }


    public override Vector2 ElementSize(int elementIndex)
    {
        Vector2 realEstate = ContentRealEstate();
        return new Vector2(realEstate.x, realEstate.y * sizeFraction(elementIndex));
    }


    public override Vector2 FirstElementPos()
    {
        return new Vector2(0f, this.size.y / 2 - this.edgeMargin);
    }


    public override Vector2 CenterElement(Vector2 lastPos, Vector2 elementSize)
    {
        return new Vector2(lastPos.x, lastPos.y - (elementSize.y / 2));
    }

   
    public override Vector2 NextElementStart(Vector2 lastPos, Vector2 elementSize)
    {
        return new Vector2(lastPos.x, lastPos.y - (elementSize.y / 2) - this.contentMargin);
    }
}
