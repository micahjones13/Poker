using System;
using System.Collections.Generic;
using System.Linq;
static class CardExtension
{
    public static void PrintCards(this List<Card> Cards)
    {
        foreach (var card in Cards)
        {
            card.PrintCard();
        }
    }
    public static List<Card> Order(this IEnumerable<Card> Cards)
    {

        return Cards.OrderByDescending(card => (card.Value != 1 ? card.Value : 14)).ToList();
    }
}

/*
    Extends the PrintCards function to any list of cards anywhere
    needs to be a static class and function, also uses 'this' keyword
    
*/