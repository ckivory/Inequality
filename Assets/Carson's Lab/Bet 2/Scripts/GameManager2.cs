using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    public LayoutController2 container;

    public PortfolioController2 PC;

    /*
    public ScoreboardController SC;

    public PopUpController popup;

    public List<PlayerController> players;

    public List<float> initialClassProbabilities;

    public List<int> startingWealthOptions;

    public Text currentPlayerLabel;
    public Text turnCounter;
    public Text genCounter;

    private int playerNumber;
    private int turnsLeftInGen;
    private int generationsLeft;
    

    public int ProbDistribution(List<float> probabilities)
    {
        float rand = Random.value;
        float chanceSoFar = 0f;

        for (int i = 0; i < probabilities.Count; i++)
        {
            chanceSoFar += probabilities[i];
            if (chanceSoFar >= rand)
            {
                return i;
            }
        }
        return probabilities.Count - 1;
    }
    

    public void StartGame()
    {
        popup.gameObject.SetActive(true);
        popup.titleText.text = "Player 1, Start!";
        playerNumber = 1;
        popup.announcementText.text = "Press \"Ready\" to generate your initial class and inheritance.";
        popup.SwitchButtons(0);

        generationsLeft = Context.numGenerations;
        turnsLeftInGen = Context.numTurnsPerGen;

        turnCounter.text = "Turns Left: " + turnsLeftInGen.ToString();
        genCounter.text = "Generations Left: " + generationsLeft.ToString();


        int probChoice = ProbDistribution(initialClassProbabilities);

        players[playerNumber - 1].SetWealth(startingWealthOptions[probChoice]);
        players[playerNumber - 1].SetClass(probChoice);
    }

    public void NextTurn()
    {
        playerNumber++;
        currentPlayerLabel.text = "Player " + playerNumber.ToString();
        if (playerNumber > 4)
        {
            playerNumber = 1;
            currentPlayerLabel.text = "Player " + playerNumber.ToString();
            turnsLeftInGen--;
            turnCounter.text = "Turns Left: " + turnsLeftInGen.ToString();
            if (turnsLeftInGen == 0)
            {
                turnsLeftInGen = Context.numTurnsPerGen;
                turnCounter.text = "Turns Left: " + turnsLeftInGen.ToString();
                generationsLeft--;
                genCounter.text = "Generations Left: " + generationsLeft.ToString();
                if (generationsLeft == 0)
                {
                    EndGame();
                }
            }
        }
    }

    void EndGame()
    {
        popup.gameObject.SetActive(true);
        popup.titleText.text = "The End";
        popup.announcementText.text = "A Pretty Owesome Games Production";
        popup.SwitchButtons(1);

    }

    */

    void Start()
    {
        if (Instance != null)
        {
            throw new System.Exception("Cannot have more than one Game Manager at a time.");
        }

        Instance = this;
        Rect canvasRect = gameObject.GetComponent<RectTransform>().rect;

        container.PlaceElement(new Vector2(canvasRect.width, canvasRect.height), Vector2.zero);


        // StartGame();
    }


    void Update()
    {
        Rect canvasRect = gameObject.GetComponent<RectTransform>().rect;
        if (container.size != canvasRect.size)
        {
            container.PlaceElement(new Vector2(canvasRect.width, canvasRect.height), Vector2.zero);
        }
        // SC.MoveMarkers();
    }
}
