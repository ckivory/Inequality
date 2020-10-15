using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image neck;
    public Image headBorder;
    public Image headFace;

    // TODO: Maybe move this value to some globals game-variables script so it can be reused. Especially if I want to save data between scenes.
    public float border_width;
    public float tab_height;

    public void ConstructTab(float window_width, int num_tabs, int tab_index)
    {
        float neckHeight = neck.sprite.border[3] / neck.pixelsPerUnitMultiplier;
        neck.transform.localPosition = Vector2.zero;
        neck.GetComponent<RectTransform>().sizeDelta = new Vector2(window_width, neckHeight);

        float tab_width = (window_width * 1f / num_tabs);
        // Left side of window, plus at least half a tab width to center it, offset by the size of the other tabs
        Vector2 head_position = new Vector2((-1 * window_width / 2) + tab_width * (0.5f + tab_index), tab_height / 2 - neckHeight / 2);
        headBorder.transform.localPosition = head_position;

        Vector2 head_size = new Vector2(tab_width, tab_height);
        headBorder.GetComponent<RectTransform>().sizeDelta = head_size;

        float adjustedBorder = border_width / headBorder.pixelsPerUnitMultiplier;
        headFace.transform.localPosition = new Vector2(head_position.x, head_position.y - adjustedBorder / 2);
        headFace.GetComponent<RectTransform>().sizeDelta = new Vector2(head_size.x - adjustedBorder * 2, head_size.y - adjustedBorder);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Makes the tab think it belongs in the middle of a 300-unit-wide window with 3 tabs, which should make it 100 units wide and centered at X=0
        ConstructTab(300f, 3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
