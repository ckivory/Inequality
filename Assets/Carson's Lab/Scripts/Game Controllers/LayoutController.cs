using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LayoutController : MonoBehaviour
{
    // Reference to the parent, to be set by the parent on initialization
    [HideInInspector]
    public GameObject parent;

    // List of rows of content, which may include sub-layouts with multiple columns.
    public List<GameObject> content;
}
