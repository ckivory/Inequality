﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabController2 : MonoBehaviour
{
    public Image face;
    public Image border;
    public TextBoxController2 textBox;

    [HideInInspector]
    public PortfolioController2 PC;

    // Called by clicking the tab face
    public void SwitchTabs()
    {
        int tabIndex = PC.tabs.IndexOf(this);
        PC.SetWindow(tabIndex);
        PC.UpdateElement();
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
        this.PC = parentPC;

        // Set sizes and transform heirarchy
        this.GetComponent<RectTransform>().sizeDelta = newSize;
        this.transform.localPosition = newPos;

        face.GetComponent<RectTransform>().sizeDelta = newSize;
        face.transform.SetParent(PC.windows[PC.activeWindow].transform);

        border.GetComponent<RectTransform>().sizeDelta = newSize;
        border.transform.SetParent(PC.windows[PC.activeWindow].transform);

        // Move tab face to front of background and border to just behind it if active
        if (inFront)
        {
            Transform backgroundTransform = PC.windows[PC.activeWindow].background.transform;
            face.transform.SetSiblingIndex(backgroundTransform.GetSiblingIndex() + 1);
            border.transform.SetSiblingIndex(Mathf.Max(backgroundTransform.GetSiblingIndex() - 1, 0));
        }
        // Else send to back then send border to back immediately after
        else
        {
            face.transform.SetAsFirstSibling();
            border.transform.SetAsFirstSibling();
        }
        
        // Set global positions
        face.transform.position = transform.position;
        border.transform.position = transform.position;

        // Set button to be clickable if it is not in the foreground currently
        face.GetComponent<Button>().interactable = !inFront;

        try
        {
            Rect tabRect = GetComponent<RectTransform>().rect;
            float tabBorderSize = face.sprite.border[0] / face.pixelsPerUnitMultiplier;
            Vector2 visibleSize = new Vector2(tabRect.width - (tabBorderSize * 2), tabRect.height - NeckHeight());
            textBox.PlaceElement(visibleSize, new Vector2(0f, NeckHeight() / 2));
        }
        catch (Exception)
        {
            throw new Exception("All tabs must have a label.");
        }
    }
}
