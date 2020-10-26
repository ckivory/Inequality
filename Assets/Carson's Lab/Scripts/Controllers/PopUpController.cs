using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    public Text titleText;

    public Text announcementText;

    public Button currentButton;
    public List<Button> buttonList;

    public void SwitchButtons(int buttonIndex)
    {
        for(int i = 0; i < buttonList.Count; i++)
        {
            if(i == buttonIndex)
            {
                buttonList[i].gameObject.SetActive(true);
            }
            else
            {
                buttonList[i].gameObject.SetActive(false);
            }
        }

        currentButton = buttonList[buttonIndex];
    }
}
