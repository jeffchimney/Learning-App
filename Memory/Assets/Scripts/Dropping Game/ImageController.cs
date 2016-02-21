using UnityEngine;
using System.Collections;

public class ImageController : MonoBehaviour {

    public Sprite[] gameImages; // instantiate an array of images as the current game picture
	// Use this for initialization
	void Start () {
	}

    void Awake()
    {
        randomizeImage(); // call this on awake of the game to randomize the image being displayed

    }

    // Update is called once per frame
    void Update () {
	
	}

    // Method that will grab an image from gameImages and display it for the user to match the letters to
    void randomizeImage()
    {
        int rand = Random.Range(0, gameImages.Length);
        this.GetComponent<SpriteRenderer>().sprite = gameImages[rand];
    }
}
