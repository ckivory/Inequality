using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayerWindow : MonoBehaviour
{
    public TextBoxController2 playerLabel;

    public TextBoxController2 wealthText;
    public TextBoxController2 educationText;
    public TextBoxController2 classText;

    void Update()
    {
        PlayerController2 player = GameManager2.Instance.GetCurrentPlayer();
        playerLabel.SetText("Player " + player.GetPlayerNum());

        wealthText.SetText("$" + player.GetWealth().ToString());
        educationText.SetText(player.namedEducation());
        classText.SetText(player.namedClass());
    }
}
