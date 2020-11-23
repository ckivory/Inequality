using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementController : MonoBehaviour
{
    [HideInInspector]
    public Vector2 size;
    [HideInInspector]
    public Vector2 pos;

    public abstract void UpdateElement();

    public virtual void PlaceElement(Vector2 newSize, Vector2 newPos)
    {
        this.size = newSize;
        this.pos = newPos;
        this.GetComponent<RectTransform>().sizeDelta = size;
        this.transform.localPosition = pos;

        UpdateElement();
    }
}
