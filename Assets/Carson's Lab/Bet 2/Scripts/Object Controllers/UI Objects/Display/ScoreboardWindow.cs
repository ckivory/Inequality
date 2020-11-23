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

    
    void Update()
    {
        roundText.SetText("Rounds Left: " + GameManager2.Instance.RoundsLeft().ToString());
        genText.SetText("Generations Left: " + GameManager2.Instance.GensLeft().ToString());

        // Find min and max wealth
        PlayerController2 player1 = GameManager2.Instance.GetPlayer(1);
        int minWealth = player1.GetWealth();
        int maxWealth = player1.GetWealth();
        for(int playerIndex = 2; playerIndex <= 4; playerIndex++)
        {
            PlayerController2 player = GameManager2.Instance.GetPlayer(playerIndex);
            minWealth = Mathf.Min(minWealth, player.GetWealth());
            maxWealth = Mathf.Max(maxWealth, player.GetWealth());
        }

        float scoreboardWidth = scoreboard.GetComponent<RectTransform>().rect.width;
        float wealthRange = maxWealth - minWealth;

        for (int markerIndex = 0; markerIndex < 4; markerIndex++)
        {
            PlayerController2 player = GameManager2.Instance.GetPlayer(markerIndex + 1);
            if(wealthRange > 0)
            {
                float rangeFraction = (float)(player.GetWealth() - minWealth) / wealthRange;
                markers[markerIndex].transform.localPosition = new Vector2(scoreboardWidth * (rangeFraction - 0.5f), 0f);
            }
        }
    }
}
