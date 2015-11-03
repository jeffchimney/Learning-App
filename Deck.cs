using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    List <int> cards;

    //public method to iterate through deck of cards
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    public void Shuffle()
    {
        if (cards == null)
        {
            cards = new List<int>();

        }
        else
        {
            cards.Clear(); // clear everything from the deck
        }

        for(int i = 0; i<52; i++)
        {
            cards.Add(i);
        }

        int cursor = cards.Count; //cursor through array
        while(cursor > 1)
        {
            //Shuffle the deck randomly
            cursor--;
            int r = Random.Range(0, cursor + 1);
            int temp = cards[r];
            cards[r] = cards[cursor];
            cards[cursor] = temp;
            
        }
    }
    // Use this for initialization
    void Start()
    {
        Shuffle();
    }

    void Update() { }
}
	
