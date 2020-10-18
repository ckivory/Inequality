using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    // Reference to the parent, to be set by the parent on initialization
    [HideInInspector]
    public GameObject parent;

    public Image background;

    // Optional
    public TabController tab;

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

    public void PositionElements()
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

        transform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.localPosition = new Vector2(0f, 0f);
        background.rectTransform.sizeDelta = new Vector2(windowWidth, windowHeight);
        GetComponent<RectTransform>().sizeDelta = background.rectTransform.sizeDelta;

        // Move tab to correct location
        if(!(tab == null))
        {
            tab.RepositionTab();
        }

        // Place Title
        float nextElementY = windowHeight / 2 - edgeMargin;
        if (Title != null)
        {
            float halfTitleHeight = Title.preferredHeight / 2;
            nextElementY -= halfTitleHeight;
            Title.transform.localPosition = new Vector2(0f,  nextElementY);
            nextElementY -= halfTitleHeight;
            nextElementY -= edgeMargin;
        }

        // Place row elements
        foreach(GameObject row in rows)
        {
            // Recursively figure out the size of each child before you place it
            WindowController rowWC = row.GetComponent<WindowController>();
            if (rowWC != null)
            {
                rowWC.PositionElements();
            }

            float halfElementHeight = row.GetComponent<RectTransform>().rect.height / 2;
            nextElementY -= halfElementHeight;
            row.transform.localPosition = new Vector2(0f, nextElementY);
            nextElementY -= halfElementHeight;
            nextElementY -= rowMargin;
        }
    }

    // Start is called before the first frame update
    public void InitializeWindow()
    {
        if (tab != null)
        {
            tab.window = this;
            tab.SendMessage("DeployTab");
        }

        foreach (GameObject row in rows)
        {
            WindowController rowWC = row.GetComponent<WindowController>();
            if (rowWC != null)
            {
                rowWC.parent = this.gameObject;
                rowWC.InitializeWindow();
            }
        }

        PositionElements();
    }
}
