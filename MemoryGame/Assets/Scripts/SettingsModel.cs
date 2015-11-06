using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsModel : MonoBehaviour
{

    public Texture[] letterChoice;
    public bool[] buttons;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

        /******Spacing Variables**********/
        int buttonSizeWidth = 55;
        int buttonSizeHeight = 55;
        int buttonSpacing = 7;
        int xOffset = 20;
        int yOffset = 20;
        int numCols = 9;
        int numButtons = letterChoice.Length;
        /**********************************/

        buttons = new bool[numButtons]; // initalize button array to amount of cards chosen

        //Create parameters for button rectangle size
        Rect r = new Rect(0, 0, buttonSizeWidth, buttonSizeHeight);

        //Loop through the amount of buttons user will have selected and assign a new Button object per texture.
        for (int i = 0; i < numButtons; i++)
        {
            r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
            r.y = yOffset + (int)Mathf.Floor((float)i / numCols) * (buttonSizeHeight + buttonSpacing);

            if (i >= 18)
            {
                r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing) + 24; // shift right along x-axis to center
            }

            Texture currTexture = letterChoice[i];


            buttons[i] = GUI.Button(new Rect(r), currTexture); //object type = bool (determine whether user has clicked (true) or not (false)
        }
    }
}
