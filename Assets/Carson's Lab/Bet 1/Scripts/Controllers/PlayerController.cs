using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text ClassLabel;
    public Text WealthLabel;
    public Text EducationLabel;

    public int wealth;
    public enum EconomicClass
    {
        LOW,
        MIDDLE,
        HIGH
    };
    public static EconomicClass[] ClassList = { EconomicClass.LOW, EconomicClass.MIDDLE, EconomicClass.HIGH };

    public EconomicClass eClass;

    public enum EducationLevel
    {
        LOW,
        MIDDLE,
        HIGH
    };

    public static EducationLevel[] EducationList = { EducationLevel.LOW, EducationLevel.MIDDLE, EducationLevel.HIGH };

    public EducationLevel education;

    public void SetClass(int newClass)
    {
        eClass = ClassList[newClass];
        ClassLabel.text = "Class: " + eClass.ToString().ToLower();
    }

    public void SetWealth(int newWealth)
    {
        wealth = newWealth;
        WealthLabel.text = "Wealth: " + wealth.ToString();
    }

    public void SetEducation(int newEducation)
    {
        education = EducationList[newEducation];
        EducationLabel.text = "Education: " + education.ToString().ToLower();
    }
}
