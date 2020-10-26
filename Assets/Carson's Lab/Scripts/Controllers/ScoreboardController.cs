using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour
{
    // Should have a bar, four colored circles that move back and forth on it, and maybe units
    public List<PlayerController> players;

    public List<Image> markers;

    public void MoveMarkers()
    {
        float maxScore = 0f;
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].wealth > maxScore)
            {
                maxScore = players[i].wealth;
            }
        }

        for (int i = 0; i < markers.Count; i++)
        {
            float width = GetComponent<RectTransform>().rect.width;
            float progress = players[i].wealth / maxScore * width;
            markers[i].transform.localPosition = new Vector2(progress - width / 2, 0f);
        }
    }
}
