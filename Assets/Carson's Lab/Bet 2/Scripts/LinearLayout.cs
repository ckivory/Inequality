using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LinearLayout : LayoutController2
{
    public List<int> relativeSizes;

    // Margins around and between elements
    public float edgeMargin;
    public float contentMargin;


    public float sizeFraction(int elementIndex)
    {
        int totalSize = 0;
        foreach (int size in relativeSizes)
        {
            totalSize += size;
        }

        return (float)relativeSizes[elementIndex] / totalSize;
    }


    public abstract Vector2 ContentRealEstate();


    public abstract Vector2 ElementSize(int elementIndex);


    public abstract Vector2 FirstElementPos();


    public abstract Vector2 CenterElement(Vector2 lastPos, Vector2 elementSize);


    public abstract Vector2 NextElementStart(Vector2 lastPos, Vector2 elementSize);


    public override void UpdateElement()
    {
        Vector2 contentRealEstate = ContentRealEstate();

        Vector2 nextElementPos = FirstElementPos();
        for (int elementIndex = 0; elementIndex < content.Count; elementIndex++)
        {
            GameObject element = content[elementIndex];

            Vector2 elementSize = ElementSize(elementIndex);
            nextElementPos = CenterElement(nextElementPos, elementSize);

            LayoutController2 LC = element.GetComponent<LayoutController2>();
            TextBoxController2 TB = element.GetComponent<TextBoxController2>();
            ButtonController BC = element.GetComponent<ButtonController>();

            if (LC != null)
            {
                LC.PlaceElement(elementSize, nextElementPos);
            }
            else if (TB != null)
            {
                TB.PlaceElement(elementSize, nextElementPos);
            }
            else if (BC != null)
            {
                BC.PlaceElement(elementSize, nextElementPos);
            }
            else
            {
                element.GetComponent<RectTransform>().sizeDelta = elementSize;
                element.transform.localPosition = nextElementPos;
            }

            nextElementPos = NextElementStart(nextElementPos, elementSize);
        }
    }


    public override void PlaceElement(Vector2 newSize, Vector2 newPos)
    {
        if (content.Count != relativeSizes.Count)
        {
            throw new Exception("Must set the relative size of every element in the layout.");
        }

        base.PlaceElement(newSize, newPos);
    }
}
