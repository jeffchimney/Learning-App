//ButtonModel.cs
//The purpose of this class is to display and position the buttons on start

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonModel : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public Texture[] cardBacks;
    public bool[] buttons;
    
   

    /*Render the Sprites that will be provided into the cardBacks array*/
    void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    /*Method to Shuffle the deck of cards*/
    public void Shuffle(bool[] input)
    {
        List<int> cards = new List<int>();
        

        for (int i = 0; i < input.Length; i++)
        {
            cards.Add(i); // add the cards into list to be shuffled
        }

        int n = cards.Count;
        //Begin shuffling list of input buttons
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }

    void OnGUI()
    {
        
        /******Spacing Variables**********/
        int buttonSizeWidth = 55;
        int buttonSizeHeight = 70;
        int buttonSpacing = 3;
        int xOffset = 20;
        int yOffset = 20;
        int numCols = 6;
        int numButtons = cardBacks.Length;
        /**********************************/

        buttons = new bool[numButtons]; // initalize button array to amount of cards chosen
        
        //Create parameters for button rectangle size
        Rect r = new Rect(0, 0, buttonSizeWidth, buttonSizeHeight);
       
        //Loop through the amount of buttons user will have selected and assign a new Button object per texture.
        for(int i = 0; i < numButtons; i++)
        {
            r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
            r.y = yOffset + (int)Mathf.Floor((float)i / numCols) * (buttonSizeHeight + buttonSpacing);

            Texture currTexture = cardBacks[i];
           
            
            buttons[i] = GUI.Button(new Rect(r), currTexture); //object type = bool (determine whether user has clicked (true) or not (false)
            

        }
        Shuffle(buttons); //call Shuffle method on buttons array
        
    }

    }
