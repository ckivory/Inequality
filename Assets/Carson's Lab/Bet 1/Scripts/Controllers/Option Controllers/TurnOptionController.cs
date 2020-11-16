using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOptionController : MonoBehaviour
{
    void Start()
    {
        if (Context.numTurnsPerGen > 0)
        {
            GetComponent<InputField>().text = Context.numTurnsPerGen.ToString();
        }
    }
}
