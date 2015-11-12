using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class letterSettings : MonoBehaviour {

    [SerializeField] private Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] private settingsMouse originalCard; // reference for the card in the scene

    public int gridRows = 3; // value for how many grid spaces to make + how far apart to place them
    public int gridCols = 4;
    public float offsetX = 1.5f;
    public float offsetY = 4f;
    static List<int> userChoices = new List<int>(); // a list to store the id's of the letters in which they have chosen, will somehow need to pass this into the game scene. 

    // Use this for initialization
    void Start () {
      
        PlayerPrefs.Save();
        createLetters();
	}

    

    public void createLetters()
    {
        Vector3 startPos = originalCard.transform.position; // position of the first card, all cards will be offset from here
       
        int index = 0;
        for (int i = 0; i < letters.Length; i++)
        {
            settingsMouse letter; // hold either the original card, or the copies created in the inspector

            if (i == 0)
            {
                letter = originalCard;
            }
            else
            {
                letter = Instantiate(originalCard) as settingsMouse;
            }


            //setLetter from the settingsMouse class, using the sprites provides and creating an id based on the i-iteration
            letter.setLetter(letters[index], i);


            float posX = startPos.x + (i % gridCols) * offsetX;
            float posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card


            // Center the remaining 4 letters positioning
            if (letters.Length - i < 5)
            {
                posX = startPos.x + (i % gridCols) * offsetX + 4;
                posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            }

            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card



            index++;

            
        }
    }

    public void lettersChosen(settingsMouse letter)
    {
        if (!userChoices.Contains(letter.getId()))
            userChoices.Add(letter.getId());
        else
            print("u alrdy chose this letter"); // this needs work, on a reclick, maybe remove the letter from the list. 
    }

    public void saveUserPreferences()
    {
        int i = 0;
        foreach (int id in userChoices)
        {
            PlayerPrefs.SetInt("letter" + i, id);
            i++;
        }

        PlayerPrefs.Save(); // needs work
       
    }
}
