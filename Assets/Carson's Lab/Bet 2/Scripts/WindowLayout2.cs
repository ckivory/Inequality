using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowLayout2 : ColumnLayout2
{
    public Image background;

    public override void PositionElements(Vector2 newSize, Vector2 newPos)
    {
        this.size = newSize;
        this.pos = newPos;
        background.GetComponent<RectTransform>().sizeDelta = size;
        background.transform.localPosition = new Vector2(0f, 0f);

        base.PositionElements(this.size, this.pos);
    }
}
