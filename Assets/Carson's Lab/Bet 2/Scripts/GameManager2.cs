using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    public LayoutController2 container;
    public ColumnLayout2 popupContainer;
    public PopUpController2 popup;

    public ButtonController EndTurnButton;

    public List<PlayerController2> players;

    public List<float> classChances;

    public List<float> lowEducationChances;
    public List<float> medEducationChances;
    public List<float> highEducationChances;

    public List<int> incomeLevels;
    public List<int> funeralCosts;

    private int turnNum = 1;
    private int roundNum = 1;
    private int generationNum = 1;

    public int roundsPerGen;
    public int gensPerGame;

    public void ClosePopup()
    {
        popupContainer.gameObject.SetActive(false);
        EndTurnButton.SetInteractible(true);
    }

    public void OpenPopup()
    {
        popupContainer.gameObject.SetActive(true);
        EndTurnButton.SetInteractible(false);
    }

    public PlayerController2 GetCurrentPlayer()
    {
        return players[turnNum - 1];
    }

    public int RoundsLeft()
    {
        return roundsPerGen - roundNum;
    }

    public int GensLeft()
    {
        return gensPerGame - generationNum;
    }

    private PlayerController2 GetPlayer(int playerNumber)
    {
        return players[playerNumber - 1];
    }

    public int ChooseRandom(List<float> chances)
    {
        float roll = Random.value;
        for (int choiceIndex = 0; choiceIndex < chances.Count; choiceIndex++)
        {
            Debug.Log(chances[choiceIndex]);

            roll -= chances[choiceIndex];
            if (roll <= 0)
            {
                return choiceIndex;
            }
        }
        return chances.Count - 1;
    }

    public void IncomePopup()
    {
        PlayerController2 player = GetPlayer(turnNum);
        player.AddWealth(incomeLevels[player.GetEducation()]);
        popup.title.SetText("You collect your income of: " + incomeLevels[player.GetEducation()] + "\nFor a total of: " + player.GetWealth());
        popup.button.textBox.SetText("Start Turn");
        popup.button.SetListener(ClosePopup);
        OpenPopup();
    }

    public void EducationPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);
        if(player.GetClass() == 0)
        {
            player.SetEducation(ChooseRandom(lowEducationChances));
        }
        else if(player.GetClass() == 1)
        {
            player.SetEducation(ChooseRandom(medEducationChances));
        }
        else
        {
            player.SetEducation(ChooseRandom(highEducationChances));
        }

        popup.title.SetText("You receive a " + player.namedEducation() + " education\nAnd will have an income of " + incomeLevels[player.GetEducation()]);
        popup.button.textBox.SetText("Collect Income");
        popup.button.SetListener(IncomePopup);
        OpenPopup();
    }

    public void InheritancePopup()
    {
        PlayerController2 player = GetPlayer(turnNum);
        int inheritance;
        if(player.GetClass() == 0)
        {
            inheritance = 100;
        }
        else if(player.GetClass() == 1)
        {
            inheritance = 200;
        }
        else
        {
            inheritance = 600;
        }

        player.AddWealth(inheritance);

        popup.title.SetText("You have inherited: " + inheritance + "\nFor a total of: " + player.GetWealth());
        popup.button.textBox.SetText("Roll for Education");
        popup.button.SetListener(EducationPopup);
        OpenPopup();
    }

    public void InitialClassPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);
        player.SetClass(ChooseRandom(classChances));

        popup.title.SetText("You are: \n" + player.namedClass() + " Class");
        popup.button.textBox.SetText("Collect Inheritance");

        popup.button.SetListener(InheritancePopup);
        OpenPopup();
    }

    public void NextClassPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        popup.title.SetText("Because of your current education, your future class is:\n" + player.namedClass());
        popup.button.textBox.SetText("Roll for education");
        popup.button.SetListener(EducationPopup);
    }

    public void FuneralPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        player.AddWealth(-1 * funeralCosts[player.GetClass()]);
        popup.title.SetText("We mourn the passing of Player " + turnNum + ", who is survived by their heir, Player " + turnNum + " Jr.\nThe funeral costs: " + funeralCosts[player.GetClass()]);
        popup.button.textBox.SetText("Determine New Class");
        popup.button.SetListener(NextClassPopup);
    }

    public void ReadyPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        if (roundNum == 1)
        {
            if(generationNum == 1)
            {
                popup.title.SetText("Player " + turnNum + ", Ready!");
                popup.button.textBox.SetText("Roll for Class");
                popup.button.SetListener(InitialClassPopup);
            }
            else
            {
                popup.title.SetText("Player " + turnNum + " is dead.");
                popup.button.textBox.SetText("Continue");
                popup.button.SetListener(FuneralPopup);
            }
        }
        else
        {
            popup.title.SetText("Player " + turnNum + ", Ready!");
            player.SetClass(player.GetEducation());
            popup.button.textBox.SetText("Collect Income");
            popup.button.SetListener(IncomePopup);
        }
        OpenPopup();
    }

    private void StartTurn()
    {
        Debug.Log("Starting player " + turnNum + "'s turn");
        ReadyPopup();
    }

    private void StartRound()
    {
        Debug.Log("Starting round " + roundNum);

        turnNum = 1;
        StartTurn();
    }

    private void StartGeneration()
    {
        Debug.Log("Starting generation " + generationNum);

        // Determine stats based on previous stats

        roundNum = 1;
        StartRound();
    }

    public void StartGame()
    {
        turnNum = 1;
        ReadyPopup();
    }
    

    // Called with the End Turn button
    public void EndTurn()
    {
        if(turnNum < 4)
        {
            turnNum++;
            StartTurn();
        }
        else
        {
            turnNum = 1;
            EndRound();
        }
    }

    private void EndRound()
    {
        // Stock appreciates every other round (if they can choose the number of rounds, what if it's odd?)

        if (roundNum < roundsPerGen)
        {
            roundNum++;
            StartRound();
        }
        else
        {
            roundNum = 1;
            EndGeneration();
        }
    }

    private void EndGeneration()
    {
        if (generationNum < gensPerGame)
        {
            generationNum++;
            StartGeneration();
        }
        else
        {
            EndGame();
        }
    }

    private void CloseGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void EndGame()
    {
        Debug.Log("End");
        CloseGame();
    }

    



    void Start()
    {
        if (Instance != null)
        {
            throw new System.Exception("Cannot have more than one Game Manager at a time.");
        }

        Instance = this;
        Rect canvasRect = gameObject.GetComponent<RectTransform>().rect;

        container.PlaceElement(new Vector2(canvasRect.width, canvasRect.height), Vector2.zero);
        popupContainer.PlaceElement(new Vector2(canvasRect.width, canvasRect.height), Vector2.zero);

        StartGame();
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
