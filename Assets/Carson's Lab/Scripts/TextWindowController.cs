using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindowController : MonoBehaviour
{
    public string TitleContent;
    public string MainContent;

    public Text TitleText;
    public Text MainText;
    // public Font font;

    // Start is called before the first frame update
    void Start()
    {
        TitleText.text = TitleContent;
        MainText.text = MainContent;
        /* To Do:
         * Set font of title and main text based on public font variable
        */
    }

    // Update is called once per frame
    void Update()
    {
        /* To Do:
         * Update content based on current events
         * Format title and main text size and position so they always fill the correct amount of the window
         */
    }
}
