using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public TextBoxController2 wealthText;
    public TextBoxController2 educationText;
    public TextBoxController2 classText;

    protected int wealth = 0;
    protected int education = 0;
    protected int socialClass = 0;
    protected int loanTotal = 0;
    protected int loanRemaining = 0;
    
    public string namedClass()
    {
        if (socialClass == 0)
        {
            return "Low";
        }
        else if (socialClass == 1)
        {
            return "Middle";
        }
        else
        {
            return "High";
        }
    }

    public string namedEducation()
    {
        if(education == 0)
        {
            return "High School";
        }
        else if(education == 1)
        {
            return "Graduate";
        }
        else
        {
            return "Post-Graduate";
        }
    }

    public int GetPlayerNum()
    {
        return GameManager2.Instance.players.IndexOf(this) + 1;
    }

    public int GetWealth()
    {
        return wealth;
    }

    public int GetEducation()
    {
        return education;
    }

    public int GetClass()
    {
        return socialClass;
    }

    public void AddWealth(int delta)
    {
        SetWealth(wealth + delta);
    }

    public void SetWealth(int newWealth)
    {
        wealth = newWealth;
        wealthText.SetText(wealth.ToString());
    }

    public void SetEducation(int newEducation)
    {
        education = newEducation;
        educationText.SetText(namedEducation());
    }

    public void SetClass(int newClass)
    {
        socialClass = newClass;
        classText.SetText(namedClass());
    }

    public void SetLoan(int loanAmount)
    {
        Debug.Log("Setting loan to: " + loanAmount);
        loanTotal = loanAmount;
        loanRemaining = loanTotal;
    }

    public int GetPaymentAmount()
    {
        return (int)(loanTotal * 0.2f);
    }

    public void MakePayment()
    {
        if(loanRemaining > 0)
        {
            int payment = GetPaymentAmount();
            if(wealth > payment)
            {
                wealth -= payment;
                loanRemaining -= payment;
            }
        }
    }

    public void PayRemainingBalance()
    {
        if(wealth > loanRemaining)
        {
            wealth -= loanRemaining;
            loanRemaining = 0;
        }
    }

    public int GetRemainingBalance()
    {
        return loanRemaining;
    }
}
