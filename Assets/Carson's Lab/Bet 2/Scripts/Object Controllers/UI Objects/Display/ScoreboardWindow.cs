using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardWindow : MonoBehaviour
{
    public TextBoxController2 roundText;
    public TextBoxController2 genText;

    public Image scoreboard;
    public List<Image> markers;

    // Update is called once per frame
    void Update()
    {
        roundText.SetText("Rounds Left: " + GameManager2.Instance.RoundsLeft().ToString());
        genText.SetText("Generations Left: " + GameManager2.Instance.GensLeft().ToString());

        int maxWealth = 0;
        for(int playerIndex = 1; playerIndex <= 4; playerIndex++)
        {
            PlayerController2 player = GameManager2.Instance.GetPlayer(playerIndex);
            maxWealth = Mathf.Max(maxWealth, player.GetWealth());
        }

        Debug.Log("Max wealth: " + maxWealth);

        for(int markerIndex = 0; markerIndex < 4; markerIndex++)
        {
            PlayerController2 player = GameManager2.Instance.GetPlayer(markerIndex + 1);
            float scoreboardWidth = scoreboard.GetComponent<RectTransform>().rect.width;
            if(maxWealth > 0)
            {
                markers[markerIndex].transform.localPosition = new Vector2(scoreboardWidth * (((float)player.GetWealth() / maxWealth) - 0.5f), 0f);
            }
        }
    }
}
