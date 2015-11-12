using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***********************************************************************/
// NOTES TO SELF: things that need fixing:
// 1) only allow a user to choose a picture as their first card choice
// 2) two different card backs to distinguish letters/pictures
// 3) center the bottom cards if row != 6 cards
// 4) Shuffle method
/*********************************************************************/

public class GameController : MonoBehaviour {
    [SerializeField] private cardBack originalCard; // reference for the card in the scene
    [SerializeField] private cardBack originalCard2;
    [SerializeField] private Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] private Sprite[] pictureCues;
    [SerializeField] private AudioClip[] soundClips;

    private letterSettings userSettings; // create an instance of letterSettings so we can retrieve the list of id's to be added
        
    public int gridRows = 3; // value for how many grid spaces to make + how far apart to place them
    public int gridCols = 4;
    public float offsetX = 1.5f;
    public float offsetY = 4f;

    private string letterType = "letter"; // use to assign a card type
    private string pictureType = "picture"; // use to assign a card type

    private cardBack firstReveal; // store the first card clicked
    private cardBack secondReveal; // store the second card clicked

    private int score = 0; // using to debug for now

    AudioSource playAudio;



    // Use this for initialization
    void Start () {
        userSettings = GetComponent<letterSettings>();
        createLetters();
        createPictures();
        playAudio = GetComponent<AudioSource>();
    }

    // Method to create the letter cards and position them on screen.
    // This method will instantiate cardBack object, and from that create clones to avoid hardcoding each card in the letter array.
    // This method will also assign an id based on the card index assuming that the array will (hopefully) maintain the order given in the inspector.
    // This might have to be changed later depending on how we choose to shuffle the cards. 
    public void createLetters()
    {
        Vector3 startPos = originalCard2.transform.position; // position of the first card, all cards will be offset from here
       
        int index = 0;
        for (int i = 0; i < letters.Length; i++)
        {
            cardBack letterCard; // hold either the original card, or the copies created in the inspector

            if (i == 0)
            {
                letterCard = originalCard2;
            }
            else
            {
                letterCard = Instantiate(originalCard2) as cardBack;
            }


            letterCard.setCard(letters[index], i, letterType, soundClips[i]);


           float posX = startPos.x + (i % gridCols) * offsetX;
           float posY = startPos.y + (int)Mathf.Floor((float) i/ gridCols) * -offsetY;
           letterCard.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card

            index++;

            //checking ids
            print("letter " + i + " id = " + letterCard.getId());
        }


    }

    // Method to create the picture cards and position them on screen.
    // This method will instantiate cardBack object, and from that create clones to avoid hardcoding each card in the picture array.
    // This method will also give an id, which will match the id of the letter. 
    // This might have to be changed later depending on how we choose to shuffle the cards. 
    public void createPictures()
    {
        Vector3 startPos = originalCard.transform.position; // position of the first card, all cards will be offset from here

        int index = 0;
        for (int i = 0; i < pictureCues.Length; i++)
        {
            cardBack pictureCard; // hold either the original card, or the copies created in the inspector

            if (i == 0)
            {
                pictureCard = originalCard;
            }
            else
            {
                pictureCard = Instantiate(originalCard) as cardBack;
            }


            pictureCard.setCard(pictureCues[index], i, pictureType, soundClips[i]);



            float posX = startPos.x + (i % gridCols) * offsetX;
            float posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            pictureCard.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card
            
            
            index++;

            //checking ids
            print("picture " + i + " id = " + pictureCard.getId());
        }

    }



    // Method to get first, use this to check what "type" the card is (picture or letter)
    public cardBack getFirst()
    {
        return firstReveal;
    }

    //Getter method to check if a second has been chosen or not (equals null if one has not been selected)
    public cardBack getSecond()
    {
        return secondReveal;
    }

    // Method to determine whether or not a user still has one more card to pick
    public bool canChoose()
    {
        if (getSecond() == null)
        {
            return true;
        }
        else
            return false;   
    }

    public void cardChosen(cardBack card)
    {
        
        if (firstReveal == null)
        {
            print(PlayerPrefs.GetInt("letter").ToString());
            firstReveal = card; // set the first card as the first chosen by user
            //Debug.Log("Id =" + firstReveal.getType());
            playAudio.PlayOneShot(firstReveal.getClip()); // play sound once user has decided to choose a card
        }

        else
        {
            playAudio.Stop(); // stop the audio if a second card is being chosen right away
            secondReveal = card; // if a first card has already been chose, set the secondReveal as the chosen card
            StartCoroutine(checkIDs()); // begin coroutine of comparing id's between the card
        }
    }

    // This needs work
    private IEnumerator checkCardType()
    {
        if(firstReveal.getType() != pictureType)
        {
            yield return new WaitForSeconds(.5f);
            firstReveal.Unreveal();
           
        }
        firstReveal = null;
        secondReveal = null;      
    }

    private IEnumerator checkIDs()
    {
        if (firstReveal.getId() == secondReveal.getId())
        {
            score++;
            Debug.Log("Score: " + score);
            
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            firstReveal.Unreveal();
            secondReveal.Unreveal();
        }
        firstReveal = null;
        secondReveal = null;
    }


    // Reminder to work on this once rest of game is running smooth
    private int[] ShuffleNumbers(int [] input)
    {
        int[] array = input.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int temp = array[i];
            int r = Random.Range(i, array.Length);
            array[i] = array[r];
            array[r] = temp;
        }
        return array;
    }
    
	
}
