using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardWindow : MonoBehaviour
{
    public TextBoxController2 roundText;
    public TextBoxController2 genText;
    
    // Update is called once per frame
    void Update()
    {
        roundText.SetText("Rounds Left: " + GameManager2.Instance.RoundsLeft().ToString());
        genText.SetText("Generations Left: " + GameManager2.Instance.GensLeft().ToString());
    }
}
