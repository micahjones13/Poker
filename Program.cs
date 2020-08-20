using System;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            deck.BuildDeck();
            Console.WriteLine(deck.DeckList.Count);
            deck.PrintDeck();
        }
    }
}