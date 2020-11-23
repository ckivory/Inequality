using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBoxController : ElementController
{
    public Image image;

    public static float Aspect(Vector2 inputSize)
    {
        if (inputSize.x <= 0)
        {
            return -1;
        }
        return inputSize.y / inputSize.x;
    }

    public override void UpdateElement()
    {
        float imageAspect = Aspect(image.GetComponent<RectTransform>().sizeDelta);
        if (imageAspect < 0)
        {
            throw new Exception("Invalid aspect ratio");
        }

        Vector2 constrainedSize;
        if (Aspect(size) > imageAspect)
        {
            constrainedSize = new Vector2(size.x, size.x * imageAspect);
        }
        else
        {
            constrainedSize = new Vector2(size.y / imageAspect, size.y);
        }

        image.GetComponent<RectTransform>().sizeDelta = constrainedSize;
    }
}
