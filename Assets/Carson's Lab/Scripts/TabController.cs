using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    // Reference to the parent. Should be set from the parent;
    [HideInInspector]
    public WindowController window;

    public Image tabBorder;
    public Image tabFace;

    // TODO: Maybe move this value to some globals game-variables script so it can be reused. Especially if I want to save data between scenes.
    public float borderWidth;
    public float tabHeight;

    private int numTabs = -1;
    [HideInInspector]
    public int tabIndex = -1;

    private void OnMouseDown()
    {
        Debug.Log("Clicked tab: " + gameObject.name);
    }

    void PositionTab(int[] tabData)
    {
        numTabs = tabData[0];
        tabIndex = tabData[1];

        RepositionTab();
    }

    public void RepositionTab()
    {
        if(numTabs > 0)
        {
            float windowWidth = window.GetComponent<RectTransform>().rect.width;
            float tabWidth = (windowWidth * 1f / numTabs);

            Vector2 tabSize = new Vector2(tabWidth, tabHeight);
            tabBorder.rectTransform.sizeDelta = tabSize;
            GetComponent<RectTransform>().sizeDelta = tabBorder.rectTransform.sizeDelta;

            Image windowImage = window.background;
            float neckHeight = (float)windowImage.sprite.border[3] / windowImage.pixelsPerUnitMultiplier;   // Size of top border of window image
            Vector2 tabPosition = new Vector2(
                (-1 * windowWidth / 2) + tabWidth * (0.5f + tabIndex),
                window.GetComponent<RectTransform>().rect.height / 2 + tabSize.y / 2 - neckHeight / 2
                );

            tabBorder.transform.localPosition = new Vector2(tabPosition.x, tabPosition.y);

            float adjustedBorder = borderWidth / tabBorder.pixelsPerUnitMultiplier;
            tabFace.GetComponent<RectTransform>().sizeDelta = new Vector2(tabSize.x - adjustedBorder * 2, tabSize.y - adjustedBorder);
            tabFace.transform.localPosition = new Vector3(tabPosition.x, tabPosition.y - adjustedBorder / 2);
        }
    }

    public void DeployTab()
    {
        // Moves tab border to back of the window
        tabBorder.transform.SetParent(window.transform);
        tabBorder.transform.SetAsFirstSibling();

        // Moves tab face to front of the window
        tabFace.transform.SetParent(window.transform);
        tabFace.transform.SetAsLastSibling();
    }
}
