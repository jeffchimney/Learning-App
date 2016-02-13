using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatchController : MonoBehaviour {

	private string currentLetter;
	private GUIStyle style = new GUIStyle(); //create a new variable
	private int pos = Screen.width / 10; // holds the position of the font
	private int Speed = 1;
    private string collected = ""; // a string to hold the current letters that are collected
    private string imageName; // hold the name of the image we are trying to match the letters to 
    public ImageController currImage;
    private List<KeyValuePair<string, bool>> lettersCaught = new List<KeyValuePair<string, bool>>(); // hold the letter and bool to check if it has been printed

    // Update is called once per frame
    void Update () {
        setName();
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

    // This method will grab the name of the image as it is being updated
    void setName()
    {
        imageName = currImage.GetComponent<SpriteRenderer>().sprite.name.ToString();

    }

    // Destroy the coconut once it has collied with the catcher
    void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Coconut") {
			//pos = pos + 15;
			currentLetter = other.gameObject.GetComponent<SpriteRenderer> ().sprite.name.ToString ();
            if (imageName.Contains(currentLetter)) // check if the current letter being caught matches a letter in the current image being displayed
            {
                lettersCaught.Add(new KeyValuePair<string, bool>(currentLetter, false));
                collected = collected + currentLetter;
                Debug.Log(collected.ToString());
            }
            else if (collected.Contains(currentLetter))
            {
                Debug.Log("You already collected this letter");
            }
			Destroy (other.gameObject);
			//Debug.Log ("I'm Colliding");
		}
	}

	private string getCollected(){
		return collected;
	}


		
	void OnGUI () {
		GUI.contentColor = Color.clear;
		style.fontSize = 33;
        char[] imgChars = imageName.ToCharArray();  // split the image name into individual characters to check for matching
        char[] collChars = new char[imageName.Length]; // split the collected letters into individual characters
        collChars = collected.ToCharArray();    // change this to equal our key value pairs
 
        //Debug.Log()
        for (int i = 0; i<collChars.Length; i++)
        {
            if (imageName.Contains(collChars[i].ToString()))
            {
                int index = imageName.IndexOf(collChars[i]);
                GUI.contentColor = Color.black;
                GUI.Label(new Rect((pos*index)/3, Screen.height / 2, 100, 50), collChars[i].ToString(), style); // print the letter to screen based on it's position in the word

            }
        }


    }






}
