using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    // Reference to the parent, to be used for sorting elements.
    public Canvas canvas;

    // Optional
    public Text Title;

    // List of rows of content, which may include sub-layouts with multiple columns.
    public List<GameObject> rows;

    // Float for how much space should be at the edges of the window and around the title
    public float edgeMargin;

    // Float for how much space should be between rows
    public float rowMargin;

    Vector2 ContentDimensions()
    {
        float contentWidth = 0f;
        float contentHeight = 0f;

        if (Title != null)
        {
            contentWidth = Title.GetComponent<RectTransform>().rect.width;
            contentHeight = Title.GetComponent<RectTransform>().rect.height;
        }

        foreach (GameObject row in rows)
        {
            Rect rowRect = row.GetComponent<RectTransform>().rect;
            contentWidth = Mathf.Max(contentWidth, rowRect.width);
            contentHeight += rowRect.height;
        }
        
        return new Vector2(contentWidth, contentHeight);
    }

    void PositionElements()
    {
        Vector2 dimensions = ContentDimensions();

        // Edge margins
        float windowWidth = dimensions.x + (edgeMargin * 2);
        float windowHeight = dimensions.y + (edgeMargin * 2);

        // Extra edge margin between title and first row
        if(Title != null && rows.Count > 0)
        {
            windowHeight += edgeMargin;
        }

        // Row margins
        if (rows.Count > 1)
        {
            windowHeight += rowMargin * rows.Count - 1;
        }

        // Debug.Log("Changing Window size to: (" + windowWidth + ", " + windowHeight + ")");
        transform.localPosition = new Vector2(0f, 0f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(windowWidth, windowHeight);

        float nextElementY = windowHeight / 2 - edgeMargin;
        if (Title != null)
        {
            float halfTitleHeight = Title.GetComponent<RectTransform>().rect.height / 2;
            nextElementY -= halfTitleHeight;
            Title.transform.localPosition = new Vector2(0f,  nextElementY);
            nextElementY -= halfTitleHeight;
            nextElementY -= edgeMargin;
        }

        foreach(GameObject row in rows)
        {
            float halfElementHeight = row.GetComponent<RectTransform>().rect.height / 2;
            nextElementY -= halfElementHeight;
            row.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY -= halfElementHeight;
            nextElementY -= rowMargin;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PositionElements();
    }

    // Update is called once per frame
    void Update()
    {
        
        // if(Anything is moved or re-scaled)
        // {
        
        PositionElements();
        
        // }
    }
}
