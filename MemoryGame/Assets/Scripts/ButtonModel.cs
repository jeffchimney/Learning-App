//ButtonModel.cs
//The purpose of this class is to display and position the buttons on start

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonModel : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public Texture faceDown;
    public Texture[] cardBacks; //array to store picture cues
    public Texture[] letters; // array to store the letters
    public int cardIndex;
    public CardFlipper flipper;
    

    /*Render the Sprites that will be provided into the cardBacks array*/
    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        flipper = GetComponent<CardFlipper>(); // create an instance of CardFlipper class
        
    }

    // Method to Toggle the face of the card.  Once the game is starting, ToggleFace(false) to show backs of all input cards.
    public void ToggleFace(bool showFace, int cardPressed)
    {

       
            if (showFace)
            {
                print("showing picture cue"); // change this for later, ex on click ToggleFace(true)
            }
            else
            {
                cardBacks[cardPressed] = faceDown;
                letters[cardPressed] = faceDown;

        }
    }



    /*Method to Shuffle the deck of cards*/
    public void Shuffle(Texture[] input1, Texture[] input2)
    {
        
        
        int n = input1.Length;
        //Begin shuffling list of input buttons
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int j = Random.Range(0, n + 1);
            // Input picture cues and letter cues
            Texture temp = input1[k];
            input1[k] = input1[n];
            input1[n] = temp;

            Texture temp2 = input2[j];
            input2[j] = input2[n];
            input2[n] = temp2;
        }
        
    }

    void OnGUI()
    {
        //TODO: Work on spacing for cards that will be displayed on bottom row, have them centered if columns < 6
        /******Spacing Variables**********/
        int buttonSizeWidth = 55;
        int buttonSizeHeight = 70;
        int buttonSpacing = 3;
        int xOffset = 20;
        int yOffset = 20;
        int numCols = 6;
        int numButtons = cardBacks.Length;
        bool isShowing;
       
        /**********************************/
        
        //Create parameters for button rectangle size
        Rect r = new Rect(0, 0, buttonSizeWidth, buttonSizeHeight); // rect for picture cues
        Rect r2 = new Rect(0, 0, buttonSizeWidth, buttonSizeHeight);
       
        //Loop through the amount of buttons user will have selected and assign a new Button object per texture.
        for(int i = 0; i < numButtons; i++)
        {
            r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
            r.y = yOffset + (int)Mathf.Floor((float)i / numCols) * (buttonSizeHeight + buttonSpacing);

            Texture currTexture = cardBacks[i];
           
            // This needs work
           if( GUI.Button(new Rect(r), currTexture) == true) //object type = bool (determine whether user has clicked (true) or not (false)
            {
                isShowing = false;
                ToggleFace(false, i);
            }
          

        }

        for (int i = 0; i < numButtons; i++)
        {

            r2.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing) + 500;
            r2.y = yOffset + (int)Mathf.Floor((float)i / numCols) * (buttonSizeHeight + buttonSpacing);

            Texture letterTexture = letters[i];

            GUI.Button(new Rect(r2), letterTexture);

        }




    }
    void Start()
    {
        Shuffle(cardBacks, letters); // on start shuffle the given amount of buttons chosen
    }

}
