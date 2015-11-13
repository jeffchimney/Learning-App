using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***********************************************************************/
// NOTES TO SELF: things that need fixing:
// 1) only allow a user to choose a picture as their first card choice
// 2) Button that will take you to the settings screen
// 3) center the bottom cards if row != 6 cards
// 4) Shuffle method
/*********************************************************************/

public class GameController : MonoBehaviour {
    [SerializeField] private cardBack originalCard; // reference for the card in the scene
    [SerializeField] private cardBack originalCard2;


    [SerializeField] private Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] private Sprite[] pictureCues;
    [SerializeField] private AudioClip[] soundClips;
    [SerializeField] private AudioClip[] congratulations;
    [SerializeField] private AudioClip openingMessage;
    private List<Sprite> userChoicePictures = new List<Sprite>(); // this will hold the id of the chosen letters to be shown in the game
    private List<Sprite> userChoiceLetters = new List <Sprite>();
    private List<AudioClip> userChoiceSounds = new List<AudioClip>();
    [SerializeField] private letterSettings userSettings; // create an instance of letterSettings so we can retrieve the list of id's to be added
    
    // Values used for positioning
    public int gridRows = 3; 
    public int gridCols = 4;
    public float offsetX = 1.5f;
    public float offsetY = 4f;

    private string letterType = "letter"; // use to assign a card type
    private string pictureType = "picture"; // use to assign a card type

    private cardBack firstReveal; // store the first card clicked
    private cardBack secondReveal; // store the second card clicked

    private int score = 0; // using to debug for now, maybe later use to check if score == userChoices.Length then they have won the game.. show animation

    AudioSource playAudio;



    // Use this for initialization
    void Start () {
        print("this is letter a: " + PlayerPrefs.GetInt("letter0"));
        print("This is letter b:" + PlayerPrefs.GetInt("letter1"));
        print("list size: " + userSettings.getListSize());
        getUserChoices();
        userSettings = GetComponent<letterSettings>();
        createLetters();
        createPictures();
        playAudio = GetComponent<AudioSource>();
        playAudio.PlayOneShot(openingMessage); // "have fun finding the letters + pic cues that match"
    }

    // This method will extract the ids of each letter from the PlayerPrefs, and store into a list to be used for displaying their choices
    public void getUserChoices()
    {
        for (int i = 0; i <= 25; i++)
        {
            int letterId = PlayerPrefs.GetInt("letter" + i);

            if (letterId != 0)
            {
                // Store these choices into lists so we can position them on screen
                userChoiceSounds.Add(soundClips[letterId]);
                userChoiceLetters.Add(letters[letterId]);
                userChoicePictures.Add(pictureCues[letterId]);
            }
            
        }
        
    }

    // Method to create the letter cards and position them on screen.
    // This method will instantiate cardBack object, and from that create clones to avoid hardcoding each card in the letter array.
    // This method will also assign an id based on the card index assuming that the array will (hopefully) maintain the order given in the inspector.
    // This might have to be changed later depending on how we choose to shuffle the cards. 
    public void createLetters()
    {
        Vector3 startPos = originalCard2.transform.position; // position of the first card, all cards will be offset from here
        //ShuffleNumbers(userChoiceLetters);
      
        for (int i = 0; i < userChoiceLetters.Count; i++)
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


            letterCard.setCard(userChoiceLetters[i], i, letterType, userChoiceSounds[i]);


           float posX = startPos.x + (i % gridCols) * offsetX;
           float posY = startPos.y + (int)Mathf.Floor((float) i/ gridCols) * -offsetY;
           letterCard.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card

       

            //checking ids
            //print("letter " + i + " id = " + letterCard.getId());
        }


    }

    // Method to create the picture cards and position them on screen.
    // This method will instantiate cardBack object, and from that create clones to avoid hardcoding each card in the picture array.
    // This method will also give an id, which will match the id of the letter. 
    // This might have to be changed later depending on how we choose to shuffle the cards. 
    public void createPictures()
    {
        Vector3 startPos = originalCard.transform.position; // position of the first card, all cards will be offset from here
       // ShuffleNumbers(userChoicePictures);


        for (int i = 0; i < userChoicePictures.Count; i++)
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


            pictureCard.setCard(userChoicePictures[i], i, pictureType, userChoiceSounds[i]);



            float posX = startPos.x + (i % gridCols) * offsetX;
            float posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            pictureCard.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card
            

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
            int playThisOne = Random.Range(0, congratulations.Length); // randomize the sounds played each time
            playAudio.PlayOneShot(congratulations[playThisOne]);
            score++;
            Debug.Log("Score: " + score);
            if (score == userChoicePictures.Count)
            {
                Debug.Log("u win"); //
            }
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
    private List<Sprite> ShuffleNumbers(List<Sprite> input)
    {
        int n = input.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Sprite temp = input[k];
            input[k] = input[n];
            input[n] = temp;
        }
        /*int[] array = input.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int temp = array[i];
            int r = Random.Range(i, array.Length);
            array[i] = array[r];
            array[r] = temp;
        }*/
        return input;
    }
    
	
}
