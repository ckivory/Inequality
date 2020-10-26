using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatioTextBox : TextBoxController
{
    // Width over height
    [Range(0.25f, 4f)]
    public float aspectRatio;

    public override void FormatText()
    {
        // Reach max height before ellipsizing
        while (BoxOverflow())
        {
            float targetWidth = boxRect.rect.height * aspectRatio;

            float newWidth = boxRect.rect.width;
            float newHeight = boxRect.rect.height;

            if(boxRect.rect.width >= targetWidth)
            {
                newHeight++;
            }
            else
            {
                newWidth++;
            }

            boxRect.sizeDelta = new Vector2(newWidth, newHeight);
        }
    }

    

    public override void SetText(string newText)
    {
        throw new System.NotImplementedException();
    }
}
