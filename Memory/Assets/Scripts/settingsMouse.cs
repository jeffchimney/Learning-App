/* This script will be used to take the user */
/*input from the cards that they want to have within the game, and then pass*/
/*them into the main game scene*/



using UnityEngine;
using System.Collections;

public class settingsMouse : MonoBehaviour {
	SpriteRenderer sprite;
    [SerializeField] private letterSettings settings;
    [SerializeField] private GameObject letter;
    private int id;
    private int[] userChoices; // an array to store the letters which a user would like to use in the game
                               // Use this for initialization
    
    

    public int getId()
    {
        return this.id;
    }

    public void setId(int input)
    {
        this.id = input;
    }

    public void OnMouseDown()
    {
		sprite = GetComponent<SpriteRenderer>();
        // call the lettersChosen method from the letterSettings screen, the input will refer to the current button being clicked.
        settings.lettersChosen(this);
        
        this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        sprite.color = Color.grey;
        
       
    }



    public void setLetter(Sprite image, int letterId)
    {
        id = letterId;
        GetComponent<SpriteRenderer>().sprite = image;

    }
}
