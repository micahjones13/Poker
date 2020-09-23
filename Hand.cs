using System;
using System.Collections.Generic;
class Hand
{
    private int HandSize = 2;
    public List<Card> HandList { get; set; } //storing the hand as a list, !Private set

    public Hand()
    {
        HandList = new List<Card>(); //initalize list
    }

    public void AddCards(Card card)
    {
        HandList.Add(card);
        // if the HandList is not greater than the handsize (2), then you can add cards.
        // if (HandList.Count < HandSize)
        // {
        //     HandList.Add(card);
        // }
        // else
        // {
        //     Console.WriteLine($"You already have enough cards");
        // }
        //later on return true/false on if a card was added or not
    }

    public void PrintHand()
    {
        HandList.PrintCards();
    }


}