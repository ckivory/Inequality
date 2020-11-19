using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LayoutController2 : MonoBehaviour
{
    [HideInInspector]
    public Vector2 size;
    [HideInInspector]
    public Vector2 pos;

    // List of rows of content, which may include sub-layouts with multiple columns.
    public List<GameObject> content;
    public List<int> relativeSizes;

    // Float for how much space should be at the edges of the window and around the title
    public float edgeMargin;

    // Float for how much space should be between rows
    public float contentMargin;

    public float sizeFraction(int elementIndex)
    {
        int totalSize = 0;
        foreach(int size in relativeSizes)
        {
            totalSize += size;
        }

        return (float)relativeSizes[elementIndex] / totalSize;
    }

    public abstract void RepositionElements();

    // Position elements in reverse order and return last element offset in whichever dimension the layout uses
    public virtual void PositionElements(Vector2 newSize, Vector2 newPos)
    {
        if(content.Count != relativeSizes.Count)
        {
            throw new Exception("Must set the relative size of every element in the layout.");
        }

        this.size = newSize;
        this.pos = newPos;

        this.RepositionElements();
    }
}
