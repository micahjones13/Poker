using System;
using System.Collections.Generic;
static class CardExtension
{
    public static void PrintCards(this List<Card> Cards)
    {
        foreach (var card in Cards)
        {
            card.PrintCard();
        }
    }
}

/*
    Extends the PrintCards function to any list of cards anywhere
    needs to be a static class and function, also uses 'this' keyword
    
*/