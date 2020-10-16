using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    // Reference to the parent;
    public WindowController window;

    public Image tabBorder;
    public Image tabFace;

    // TODO: Maybe move this value to some globals game-variables script so it can be reused. Especially if I want to save data between scenes.
    public float borderWidth;
    public float tabHeight;

    

    public void ConstructTab(int numTabs, int tabIndex)
    {
        float windowWidth = window.GetComponent<RectTransform>().rect.width;
        float tabWidth = (windowWidth * 1f / numTabs);
        
        Vector2 tabSize = new Vector2(tabWidth, tabHeight);
        tabBorder.GetComponent<RectTransform>().sizeDelta = tabSize;

        Image windowImage = window.GetComponent<Image>();
        float neckHeight = (float)windowImage.sprite.border[3] / windowImage.pixelsPerUnitMultiplier;   // Size of top border of window image
        Vector2 tabPosition = new Vector2(
            (-1 * windowWidth / 2) + tabWidth * (0.5f + tabIndex),
            window.GetComponent<RectTransform>().rect.height / 2 + tabSize.y / 2 - neckHeight / 2
            );

        tabBorder.transform.localPosition = new Vector2(tabPosition.x, tabPosition.y);

        // Deploys tab border to back of Canvas
        tabBorder.transform.SetParent(window.canvas.transform);
        tabBorder.transform.SetSiblingIndex(0);

        float adjustedBorder = borderWidth / tabBorder.pixelsPerUnitMultiplier;
        tabFace.GetComponent<RectTransform>().sizeDelta = new Vector2(tabSize.x - adjustedBorder * 2, tabSize.y - adjustedBorder);
        tabFace.transform.localPosition = new Vector3(tabPosition.x, tabPosition.y - adjustedBorder / 2);
        
        // Deploys tab face to front of Canvas
        tabFace.transform.SetParent(window.canvas.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Tells the tab to act like it is the middle of three tabs.
        ConstructTab(3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
