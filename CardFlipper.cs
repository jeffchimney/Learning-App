using UnityEngine;
using System.Collections;

public class CardFlipper : MonoBehaviour {


    SpriteRenderer spriteRenderer; // reference to card
    CardModel model; // instance of CardModel class

    public AnimationCurve scaleCurve; 
    public float duration = 0.5f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        model = GetComponent<CardModel>();
        
    }
	
    public void flipCard(Sprite startImage, Sprite endImage, int cardIndex)
    {
        StopCoroutine(Flip(startImage, endImage, cardIndex));
        StartCoroutine(Flip(startImage, endImage, cardIndex));

    }

    //Method to flip the card
    IEnumerator Flip(Sprite startImage, Sprite endImage, int cardIndex)
    {
        spriteRenderer.sprite = startImage;

        float time = 0f;
        while(time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration; // time taken to flip the card

            Vector3 localScale = transform.localScale;
            localScale.x = scale; // scaling the card on the x-axis (flat -> skinny -> flat)
            transform.localScale = localScale;

            if (time <= 0.5f)
            {
                spriteRenderer.sprite = endImage;
            }

            yield return new WaitForFixedUpdate(); 

        }

        if (cardIndex == -1)
        {
            model.ToggleFace(false); // show the back of the card
        }
        else
        {
            model.cardIndex = cardIndex;
            model.ToggleFace(true); // show the face of the card
        }
    }
}
