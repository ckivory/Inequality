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

    public EventListController personalEvents;
    public EventListController communityEvents;

    public ButtonController EndTurnButton;
    public ButtonController LoanButton;

    public List<PlayerController2> players;

    public List<float> classChances;

    public List<float> lowEducationChances;
    public List<float> medEducationChances;
    public List<float> highEducationChances;

    public List<int> incomeLevels;
    public List<int> funeralCosts;
    public List<int> loanAmounts;

    public List<StockController> stockOptions;

    private int turnNum = 1;
    private int roundNum = 1;
    private int generationNum = 1;

    public int roundsPerGen;
    public int gensPerGame;


    public PlayerController2 GetCurrentPlayer()
    {
        if(turnNum - 1 < 0 || turnNum > players.Count)
        {
            throw new System.Exception("Cannot get current player");
        }
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


    public PlayerController2 GetPlayer(int playerNumber)
    {
        return players[playerNumber - 1];
    }


    public int ChooseRandom(List<float> chances)
    {
        float roll = Random.value;
        for (int choiceIndex = 0; choiceIndex < chances.Count; choiceIndex++)
        {
            roll -= chances[choiceIndex];
            if (roll <= 0)
            {
                return choiceIndex;
            }
        }
        return chances.Count - 1;
    }


    public void DisableImage()
    {
        popup.SetImageEnabled(false);
    }


    public void ClosePopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        EndTurnButton.SetInteractible(true);

        if (player.GetEducation() < 2 && !(player.GetRemainingBalance() > 0))
        {
            LoanButton.SetInteractible(true);
        }
        else
        {
            LoanButton.SetInteractible(false);
        }

        popupContainer.gameObject.SetActive(false);
    }


    public void OpenPopup()
    {
        popupContainer.gameObject.SetActive(true);
        EndTurnButton.SetInteractible(false);
    }


    public void TakeLoan()
    {
        PlayerController2 player = GetPlayer(turnNum);

        if(player.GetEducation() < 2)
        {
            player.SetLoan(loanAmounts[player.GetEducation()]);
            player.SetEducation(player.GetEducation() + 1);
        }

        player.MakePayment();

        LoanButton.SetInteractible(false);
        EndTurnButton.SetInteractible(true);

        popupContainer.gameObject.SetActive(false);
    }


    public void CommunityEventPopup()
    {
        EventController chosenEvent = communityEvents.events[Random.Range(0, communityEvents.events.Count)];

        popup.SetImage(chosenEvent.eventImage);

        string eventTitle = chosenEvent.description + "\n";

        for(int effectIndex = 0; effectIndex < chosenEvent.effectsByClass.Count; effectIndex++)
        {
            int effectAmount = chosenEvent.effectsByClass[effectIndex];
            eventTitle += effectAmount.ToString();
            if(effectIndex < chosenEvent.effectsByClass.Count - 1)
            {
                eventTitle += "\t\t";
            }
        }
        
        popup.title.SetText(eventTitle);

        foreach(PlayerController2 player in players)
        {
            int effect = chosenEvent.effectsByClass[player.GetClass()];
            player.ChangeWealth(effect);
        }

        popup.buttons[0].textBox.SetText("Next Round");
        popup.buttons[0].SetListener(StartRound);
        popup.buttons[0].GetOnClick().AddListener(DisableImage);
        OpenPopup();
    }


    public void PersonalEventPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);
        EventController chosenEvent = personalEvents.events[Random.Range(0, personalEvents.events.Count)];

        popup.SetImage(chosenEvent.eventImage);
        popup.title.SetText(chosenEvent.description);

        if(chosenEvent.special == EventController.SpecialEffect.None)
        {
            int effect = chosenEvent.effectsByClass[player.GetClass()];
            popup.buttons[0].textBox.SetText(effect.ToString());

            player.ChangeWealth(effect);
        }
        else if(chosenEvent.special == EventController.SpecialEffect.Scholarship)
        {
            if(player.GetEducation() < 2)
            {
                player.SetEducation(player.GetEducation() + 1);
                popup.buttons[0].textBox.SetText("Get a Free " + player.namedEducation() + " Degree");
            }
            else
            {
                popup.buttons[0].textBox.SetText("Nothing Happens");
            }
        }
        else if(chosenEvent.special == EventController.SpecialEffect.Divorce)
        {
            player.ChangeWealth(-1 * player.GetWealth() / 2);
            popup.buttons[0].textBox.SetText("Lose Half of your Wealth");
        }

        popup.buttons[0].SetListener(ClosePopup);
        popup.buttons[0].GetOnClick().AddListener(DisableImage);
        OpenPopup();
    }


    public void LoanPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        Debug.Log("Player education: " + player.GetEducation());

        popup.SetButtonNum(2);

        if (player.GetEducation() < 2)
        {
            string nextEducation;
            if(player.GetEducation() == 0)
            {
                nextEducation = "Graduate";
            }
            else
            {
                nextEducation = "Post-Graduate";
            }
            popup.title.SetText("To go back for a " + nextEducation + " degree, you must have: " + loanAmounts[player.GetEducation()] + "\nYou will pay 20% of this amount on each turn, and the remaining balance in the event of your death.");
            
            popup.buttons[0].textBox.SetText("Cancel");
            popup.buttons[0].SetListener(ClosePopup);

            popup.buttons[1].textBox.SetText("Continue");
            
            if (player.GetWealth() >= loanAmounts[player.GetEducation()])
            {
                popup.buttons[1].SetListener(TakeLoan);
                popup.buttons[1].SetInteractible(true);
            }
            else
            {
                popup.buttons[1].SetInteractible(false);
            }

            OpenPopup();
        }
        else
        {
            LoanButton.SetInteractible(false);
            Debug.Log("Loan Button should not have been interactible");
        }
    }


    public int DividendTotal(PlayerController2 player)
    {
        int dividendTotal = 0;
        for(int i = 0; i < 3; i++)
        {
            dividendTotal += (int)(stockOptions[i].DividendAmount() * player.GetStocks(i));
        }

        return dividendTotal;
    }


    public void IncomePopup()
    {
        PlayerController2 player = GetPlayer(turnNum);
        player.ChangeWealth(incomeLevels[player.GetEducation()]);

        popup.SetButtonNum(1);

        string incomeText = "You collect your income of: " + incomeLevels[player.GetEducation()];

        if(player.GetRemainingBalance() > 0)
        {
            incomeText += "\nYou make another loan payment of: " + player.GetPaymentAmount();
            player.MakePayment();
        }

        if(player.HasStocks() && roundNum % 2 == 1)
        {
            incomeText += "\nYour stocks pay out a dividend of: " + DividendTotal(player);
            player.ChangeWealth(DividendTotal(player));
        }

        popup.title.SetText(incomeText);


        popup.buttons[0].textBox.SetText("Draw Event");
        popup.buttons[0].SetListener(PersonalEventPopup);
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

        popup.SetButtonNum(1);

        popup.title.SetText("You receive a " + player.namedEducation() + " education\nAnd will have an income of " + incomeLevels[player.GetEducation()]);
        popup.buttons[0].textBox.SetText("Collect Income");
        popup.buttons[0].SetListener(IncomePopup);
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

        player.ChangeWealth(inheritance);

        popup.SetButtonNum(1);

        popup.title.SetText("You have inherited: " + inheritance + "\nFor a total of: " + player.GetWealth());
        popup.buttons[0].textBox.SetText("Roll for Education");
        popup.buttons[0].SetListener(EducationPopup);
        OpenPopup();
    }


    public void InitialClassPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);
        player.SetClass(ChooseRandom(classChances));

        popup.SetButtonNum(1);

        popup.title.SetText("You are: \n" + player.namedClass() + " Class");
        popup.buttons[0].textBox.SetText("Collect Inheritance");

        popup.buttons[0].SetListener(InheritancePopup);
        OpenPopup();
    }


    public void NextClassPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        popup.SetButtonNum(1);

        popup.title.SetText("Because of your current education, your future class is:\n" + player.namedClass());
        popup.buttons[0].textBox.SetText("Roll for education");
        popup.buttons[0].SetListener(EducationPopup);
    }


    public void FuneralPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        popup.SetButtonNum(1);

        player.ChangeWealth(-1 * funeralCosts[player.GetClass()]);

        string funeralText =
            "We mourn the passing of Player " + turnNum + ", who is survived by their heir, Player " + turnNum + " Jr.\n"
            + "The funeral costs: " + funeralCosts[player.GetClass()];
        
        if(player.GetRemainingBalance() > 0)
        {
            funeralText += "\nTheir remaining student loan balance of " + player.GetRemainingBalance() + " is taken out of their estate.";
            player.PayRemainingBalance();
        }

        popup.title.SetText(funeralText);

        popup.buttons[0].textBox.SetText("Determine New Class");
        popup.buttons[0].SetListener(NextClassPopup);
    }


    public void ReadyPopup()
    {
        PlayerController2 player = GetPlayer(turnNum);

        popup.SetButtonNum(1);

        if (roundNum == 1)
        {
            if(generationNum == 1)
            {
                popup.title.SetText("Player " + turnNum + ", Ready!");
                popup.buttons[0].textBox.SetText("Roll for Class");
                popup.buttons[0].SetListener(InitialClassPopup);
            }
            else
            {
                popup.title.SetText("Player " + turnNum + " is dead.");
                popup.buttons[0].textBox.SetText("Continue");
                popup.buttons[0].SetListener(FuneralPopup);
            }
        }
        else
        {
            popup.title.SetText("Player " + turnNum + ", Ready!");
            player.SetClass(player.GetEducation());
            popup.buttons[0].textBox.SetText("Collect Income");
            popup.buttons[0].SetListener(IncomePopup);
        }
        OpenPopup();
    }


    private void StartTurn()
    {
        ReadyPopup();
    }


    private void StartRound()
    {
        turnNum = 1;
        StartTurn();
    }


    private void StartGeneration()
    {
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
        foreach(StockController stock in stockOptions)
        {
            stock.ResetAmount();
        }

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
        foreach(StockController stock in stockOptions)
        {
            stock.Grow();
        }

        if (roundNum < roundsPerGen)
        {
            roundNum++;
            CommunityEventPopup();
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
