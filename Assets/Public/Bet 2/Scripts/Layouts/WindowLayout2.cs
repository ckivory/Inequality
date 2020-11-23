using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowLayout2 : ColumnLayout2
{
    public Image background;

    public override void UpdateElement()
    {
        background.GetComponent<RectTransform>().sizeDelta = size;
        background.transform.localPosition = Vector2.zero;

        base.UpdateElement();
    }
}
