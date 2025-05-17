using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public DisplayCard card6, card7, card8;

    public List<NonBasicCard> deck = new List<NonBasicCard>();
    public List<NonBasicCard> discardPile = new List<NonBasicCard>();

    public bool characterSelected = false;

    public void DrawHand()
    {
        NonBasicCard card6Card = deck[Random.Range(0, deck.Count)];
        card6.card = card6Card;
        deck.Remove(card6Card);
        discardPile.Add(card6Card);
        card6.UpdateCard();
        NonBasicCard card7Card = deck[Random.Range(0, deck.Count)];
        card7.card = card7Card;
        deck.Remove(card7Card);
        discardPile.Add(card7Card);
        card7.UpdateCard();
        NonBasicCard card8Card = deck[Random.Range(0, deck.Count)];
        card8.card = card8Card;
        deck.Remove(card8Card);
        discardPile.Add(card8Card);
        card8.UpdateCard();
    }

    private void Update()
    {
        if (card6.card == null && characterSelected == true)
        {
            NonBasicCard card6Card = deck[Random.Range(0, deck.Count)];
            card6.card = card6Card;
            deck.Remove(card6Card);
            discardPile.Add(card6Card);
            card6.UpdateCard();
        }
        if (card7.card == null && characterSelected == true)
        {
            NonBasicCard card7Card = deck[Random.Range(0, deck.Count)];
            card7.card = card7Card;
            deck.Remove(card7Card);
            discardPile.Add(card7Card);
            card7.UpdateCard();
        }
        if (card8.card == null && characterSelected == true)
        {
            NonBasicCard card8Card = deck[Random.Range(0, deck.Count)];
            card8.card = card8Card;
            deck.Remove(card8Card);
            discardPile.Add(card8Card);
            card8.UpdateCard();
        }
        if (deck.Count == 0)
        {
            foreach(NonBasicCard card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }

}
