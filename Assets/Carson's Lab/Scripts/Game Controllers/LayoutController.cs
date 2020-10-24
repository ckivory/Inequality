﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LayoutController : MonoBehaviour
{
    // Reference to the parent, to be set by the parent on initialization
    [HideInInspector]
    public GameObject parent;

    // List of rows of content, which may include sub-layouts with multiple columns.
    public List<GameObject> content;

    // Float for how much space should be at the edges of the window and around the title
    public float edgeMargin;

    // Float for how much space should be between rows
    public float contentMargin;

    public abstract Vector2 ContentDimensions();

    public abstract void PositionElements();

    public virtual void InitializeLayout()
    {
        foreach (GameObject element in content)
        {
            LayoutController elemLC = element.GetComponent<LayoutController>();
            if (elemLC != null)
            {
                elemLC.parent = this.gameObject;
                elemLC.InitializeLayout();
            }
        }

        PositionElements();
    }
}
