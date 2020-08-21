using System;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            deck.BuildDeck();
            deck.PrintDeck();
        }
    }
}


/*
Improve printing of cards (ace, king, ect)
Hand class, dealing a hand, shuffling
Build hand, give someone cards for their hand
*/