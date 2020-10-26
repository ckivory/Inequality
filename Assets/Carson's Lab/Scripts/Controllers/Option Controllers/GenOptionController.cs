using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenOptionController : MonoBehaviour
{
    void Start()
    {
        if (Context.numGenerations > 0)
        {
            GetComponent<InputField>().text = Context.numGenerations.ToString();
        }
    }
}
