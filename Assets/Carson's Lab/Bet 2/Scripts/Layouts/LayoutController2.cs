using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LayoutController2 : ElementController
{
    // List of rows of content, which may include sub-layouts with multiple columns.
    public List<GameObject> content;
}
