using UnityEngine;
using System.Collections;

public class CardFlipper : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    ButtonModel model;

    public AnimationCurve scaleCurve; // create a parabolic shape curve in Unity Settings
    public float duration = 0.5f; // time ease to flip the card

    //Similar to Start(), show when the game is played
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        model = GetComponent<ButtonModel>();
    }

    //Method to flip the card, provide the startImage (cardback) and end Image (picture cue/letter cue) 
    public void FlipCard(Texture startImage, Texture endImage, int cardIndex)
    {
        StopCoroutine(Flip(startImage, endImage, cardIndex));
        StartCoroutine(Flip(startImage, endImage, cardIndex));
    }
    
    /***************** This needs Work ************************/
    //TODO: Figure out how to access and render texture image
    // Might have to change to sprites
    IEnumerator Flip(Texture startImage, Texture endImage, int cardIndex)
    {
        startImage = model.faceDown;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time); //traces parabolic curve to determine the flipping motion of the card over the y-axis
            time = time + Time.deltaTime / duration;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f)
            {
                startImage = model.cardBacks[cardIndex]; // once the time ease to flip the card is finished, show the face of the card (picutre, letter cues)
            }

            yield return new WaitForFixedUpdate();
        }

        if (cardIndex == -1)
        {
            model.ToggleFace(false, cardIndex);
        }
        else
        {
            model.cardIndex = cardIndex;
            model.ToggleFace(true, cardIndex);
        }
    }

}
