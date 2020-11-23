using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public Sprite eventImage;
    public string description;
    public List<int> effectsByClass;

    public enum SpecialEffect
    {
        Scholarship,
        Divorce,
        None
    }

    public SpecialEffect special = SpecialEffect.None;
}
