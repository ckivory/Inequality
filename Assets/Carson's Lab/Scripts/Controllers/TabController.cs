using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    // TODO: Figure out why tab labels are not resizing correctly based on the size of the window that is being switched to.


    // Reference to the parent. Should be set from the parent;
    [HideInInspector]
    public PortfolioController PC;

    public Image tabBorder;
    public Image tabFace;

    public TextBoxController tabLabel;

    private int numTabs = -1;
    [HideInInspector]
    public int tabIndex = -1;

    private float neckHeight;

    private void OnMouseDown()
    {
        Debug.Log("Clicked tab: " + gameObject.name);
    }

    public void PositionTab(int newNumTabs, int newTabIndex)
    {
        numTabs = newNumTabs;
        tabIndex = newTabIndex;

        RepositionTab();
    }

    public void RepositionTab()
    {
        if(numTabs > 0)
        {
            float windowWidth = PC.GetActiveWindow().GetComponent<RectTransform>().rect.width;
            float tabWidth = (windowWidth * 1f / numTabs);

            Vector2 tabSize = new Vector2(tabWidth, PC.tabHeight);
            tabBorder.rectTransform.sizeDelta = tabSize;
            // GetComponent<RectTransform>().sizeDelta = tabBorder.rectTransform.sizeDelta;

            Image windowImage = PC.GetActiveWindow().background;
            neckHeight = (float)windowImage.sprite.border[3] / windowImage.pixelsPerUnitMultiplier;   // Size of top border of window image
            Vector2 tabPosition = new Vector2(
                (-1 * windowWidth / 2) + tabWidth * (0.5f + tabIndex),
                PC.GetActiveWindow().GetComponent<RectTransform>().rect.height / 2 + tabSize.y / 2 - neckHeight / 2
                );

            tabBorder.transform.localPosition = new Vector2(tabPosition.x, tabPosition.y);

            float adjustedBorder = PC.borderWidth / tabBorder.pixelsPerUnitMultiplier;
            tabFace.GetComponent<RectTransform>().sizeDelta = new Vector2(tabSize.x - adjustedBorder * 2, tabSize.y - adjustedBorder);
            tabFace.transform.localPosition = new Vector3(tabPosition.x, tabPosition.y - adjustedBorder / 2);

            // Update container size
            tabLabel.containerSize = tabFace.GetComponent<RectTransform>().sizeDelta - new Vector2(neckHeight, neckHeight);
            tabLabel.FormatText();
        }
    }

    public void DeployTab()
    {
        // Moves tab border to back of the window
        tabBorder.transform.SetParent(PC.transform);
        tabBorder.transform.SetAsFirstSibling();

        // Moves tab face to front of the window
        tabFace.transform.SetParent(PC.transform);
        tabFace.transform.SetAsLastSibling();

        tabLabel.shrinkFontOnOverflow = true;
        tabLabel.InitializeTextBox();
    }
}
