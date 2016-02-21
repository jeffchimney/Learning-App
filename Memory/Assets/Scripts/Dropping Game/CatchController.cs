using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatchController : MonoBehaviour {

    private GameImage imageClass = new GameImage(0, "l", false); // use this to create imageClass objects to create bool checks for letters being collected
    private GameImage[] imageObjects;
	private string currentLetter;
	private GUIStyle style = new GUIStyle(); //create a new variable
	private int pos = Screen.width / 10; // holds the position of the font
	private int Speed = 1;
    private string collected = ""; // a string to hold the current letters that are collected
    private string imageName; // hold the name of the image we are trying to match the letters to 
    public ImageController currImage;
    private Dictionary<string, bool> checkCaught = new Dictionary<string, bool>(); // hold the letter and bool to check if it has been printed
    private int lives = 3; // set their lives to 3, we can change this later if we allow the user to decide the amount of lives (difficulty) etc.. 

    // Update is called once per frame
    void Update () {

        checkLives();

        if (imageClass.checkWinning(imageObjects) == true){
            Debug.Log("You have won"); // will have to change this later to randomize a new image for a user to spell
        }

        // Decrement the x position when the left arrow is pressed down
        if (Input.GetKeyDown (KeyCode.LeftArrow)) {

			Vector3 position = this.transform.position;
			position.x--;
			this.transform.position = position;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Vector3 position = this.transform.position;
			position.x++;
			this.transform.position = position;
		}
	}

    void Start()
    {
        setName(); // set the name of the current Image as it is being displayed on awake in ImageController script. (Will have to recall both functions once the user has spelt out the word)
        createPairs(); 
    }


    // This method will grab the name of the image as it is being updated
    void setName()
    {
        imageName = currImage.GetComponent<SpriteRenderer>().sprite.name.ToString(); // grab name of the image currently being displayed
    }

    // from the name of the current image displayed, create key value pairs
    void createPairs()
    {
        Debug.Log(imageName.ToString());
        imageObjects = new GameImage[imageName.Length]; // initialize our array to the length of the imageName
        char[] imgChars = imageName.ToCharArray();  // split the image name into individual characters to check for matching
        for (int i = 0; i < imgChars.Length; i++)
        {
            //checkCaught.Add(imgChars[i].ToString(), false); // add all of the letters to a key value pair, initial bool = false because not yet caught
            imageObjects[i] = new GameImage(i, imgChars[i].ToString(), false); // create GameImage objects and store into array
            Debug.Log("These letters are in the dictionary: " + imageObjects[i].getLetter());
        }
    }

    // Destroy the coconut once it has collied with the catcher
    void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Coconut") {
			currentLetter = other.gameObject.GetComponent<SpriteRenderer> ().sprite.name.ToString ();
            if (imageClass.Contains(imageObjects, currentLetter) == true)
            {
                imageClass.findWithLetter(currentLetter, imageObjects).setCheck(true); // find the object within the array that contains the matching letter, and set it to true (it has been caught)
                collected = collected + currentLetter;
                Debug.Log(collected.ToString());
            }
            else if (collected.Contains(currentLetter))
            {
                lives--; // decrement lives :(
                Debug.Log("You already collected this letter");
            }
            else
                lives--; // decrement lives
			Destroy (other.gameObject);
			//Debug.Log ("I'm Colliding");
		}
	}

	private string getCollected(){
		return collected;
	}


	
    // Need to fix: 1) Upon repeated collection, the letter is being removed	
	void OnGUI () {
		//GUI.contentColor = Color.clear;
		style.fontSize = 33;
        GUI.Label(new Rect(Screen.width - 130, 0 + 20 , 120, 30), "Lives: " + lives.ToString(), style); // display the letter based on it's index key within the word

        // char[] collChars = new char[imageName.Length]; // split the collected letters into individual characters
        // collChars = collected.ToCharArray();    // change this to equal our key value pairs

        for (int i = 0; i < imageObjects.Length; i++) 
        {
            if(imageObjects[i].getCheck() == true) // if the check is set to true, it has been caught and will be printed to screen
            {
                GUI.contentColor = Color.black;
                GUI.Label(new Rect((pos * imageObjects[i].getKey()) / 3, Screen.height / 2, 100, 50), imageObjects[i].getLetter(), style); // display the letter based on it's index key within the word
            }
        }


    }

    void checkLives()
    {
        if(lives == 0)
        {
            lives = 0;
            Debug.Log("You lose"); // change this later, pause the game or redirect to a new screen asking if they want to play again
        }
    }






}
