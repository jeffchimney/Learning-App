/***********************************************************************************************************************/
// NOTES TO SELF: things that need fixing
// 2) maintain "bolded" images of letters that are chosen on destroying the scene
// 3) "add all" button
/**********************************************************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class letterSettings : MonoBehaviour {

    [SerializeField] private Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] private settingsMouse originalCard; // reference for the card in the scene
    static bool isEmpty = false; // boolean to check if the user selections list is empty
    public int gridRows = 3; // value for how many grid spaces to make + how far apart to place them
    public int gridCols = 4;
    public float offsetX = 1.5f;
    public float offsetY = 4f;
    static List<int> userChoices = new List<int>(); // a list to store the id's of the letters in which they have chosen, will somehow need to pass this into the game scene. 
	
    // Use this for initialization
    void Start () {
        createLetters();
        
        //print(PlayerPrefs.GetInt("letter1"));
        //print("userchoices size: " + userChoices.Count);
	}

    public int getListSize()
    {
        return userChoices.Count;
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
        {
            userChoices.Add(letter.getId());
            isEmpty = false;
        }
        else if (userChoices.Contains(letter.getId()))
        {
            userChoices.Remove(letter.getId());
        }
        else if (userChoices.Count == 0)
        {
            isEmpty = true; // this variable will be passed into the playbutton screen, if it is set to true, do not allow the user to continue to the game screen
        }
      
            
    }

    public void saveUserPreferences()
    {
        int i = 0;

        foreach (int id in userChoices)
        {
                //print("My Current Ids Chosen: " + id);
                PlayerPrefs.SetInt("letter" + i, id);
            
            i++;
        }
		// Set the remaining choices to -1 so we know which ones to include when we pass them through to the next scene
		//-1 will mean that there is still space left for these letters to be chosen
		for (int numChoices = userChoices.Count; numChoices <= 25; numChoices++) {
			PlayerPrefs.SetInt ("letter" + numChoices, -1);
		}

        PlayerPrefs.Save(); // needs work
       
    }


    void OnDestroy(){
		/*foreach (int i in userChoices) {
			print ("My Current Ids: " + i);
		}*/
        	saveUserPreferences();
    }

    public bool getIsEmpty()
    {
        return isEmpty;
    }

    public void setIsEmpty(bool input)
   {
        isEmpty = input;
   }
}
