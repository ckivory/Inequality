using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabController2 : MonoBehaviour
{
    public Image face;
    public Image border;

    [HideInInspector]
    public PortfolioController2 PC;

    // For some reason this isn't working at the moment.
    void SwitchTabs()
    {
        Debug.Log("Clicking!");
        // Should never occur before PC is set
        PC.SetWindow(PC.tabs.IndexOf(this));
        PC.RepositionElements();
    }

    public float NeckHeight()
    {
        Image faceImage = face.GetComponent<Image>();
        return faceImage.sprite.border[1];     // Get bottom border of sprite
    }

    public float GetResolution()
    {
        Image faceImage = face.GetComponent<Image>();
        return faceImage.pixelsPerUnitMultiplier;
    }

    public void PositionTab(Vector2 newSize, Vector2 newPos, PortfolioController2 parentPC, bool inFront)
    {
        // Debug.Log("Size: " + newSize);
        // Debug.Log("Pos: " + newPos);

        this.PC = parentPC;

        this.GetComponent<RectTransform>().sizeDelta = newSize;
        this.transform.localPosition = newPos;

        face.GetComponent<RectTransform>().sizeDelta = newSize;
        face.transform.SetParent(PC.windows[PC.activeWindow].transform);
        // Move tab face to front of background if active
        if (inFront)
        {
            int backgroundIndex = PC.windows[PC.activeWindow].background.transform.GetSiblingIndex();
            face.transform.SetSiblingIndex(backgroundIndex + 1);
        }
        // Else send to back then send border to back immediately after
        else
        {
            face.transform.SetAsFirstSibling();
        }
        face.transform.position = transform.position;

        // Move tab border to back
        border.GetComponent<RectTransform>().sizeDelta = newSize;
        border.transform.SetParent(PC.windows[PC.activeWindow].transform);
        border.transform.SetAsFirstSibling();
        border.transform.position = transform.position;
    }
}
