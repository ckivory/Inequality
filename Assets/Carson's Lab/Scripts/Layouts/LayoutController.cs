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

    // Float for how much space should be at the edges of the window and around the title
    public float edgeMargin;

    // Float for how much space should be between rows
    public float contentMargin;
    
    protected RectTransform rt;

    public RectTransform RT { get => rt; set => rt = value; }
    
    // Helper to get size of content elements without margins
    public abstract Vector2 ContentDimensions();


    // Recursively enforce sizing and alignment before arranging elements
    public virtual void SetConstraints()
    {
        if(rt == null)
        {
            rt = GetComponent<RectTransform>();
        }

        foreach (GameObject element in content)
        {
            LayoutController elementLC = element.GetComponent<LayoutController>();
            if (elementLC != null)
            {
                elementLC.RepositionElements();
            }
        }
    }


    // Position elements in reverse order and return last element offset in whichever dimension the layout uses
    public abstract float PositionElements();


    // Determine constraints for window and then do the work to move everything into place
    public void RepositionElements()
    {
        SetConstraints();
        PositionElements();
    }


    // Set parent of each child to self and place layout in initial configuration
    public virtual void InitializeLayout()
    {
        if (rt == null)
        {
            rt = GetComponent<RectTransform>();
        }

        foreach (GameObject element in content)
        {
            LayoutController elementLC = element.GetComponent<LayoutController>();
            if (elementLC != null)
            {
                elementLC.parent = this.gameObject;
                elementLC.InitializeLayout();
            }
        }

        // Figure out how big the window will need to be, where its components should go, etc.
        SetConstraints();
        PositionElements();
    }
}
