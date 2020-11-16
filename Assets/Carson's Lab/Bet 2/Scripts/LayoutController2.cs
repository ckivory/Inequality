using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LayoutController2 : MonoBehaviour
{
    // Reference to the parent, to be set by the parent on initialization
    [HideInInspector]
    public GameObject parent;

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

    // Position elements in reverse order and return last element offset in whichever dimension the layout uses
    public abstract void PositionElements(Vector2 size, Vector2 pos);
}
