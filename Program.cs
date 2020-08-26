using System;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            // var hand = new Hand();
            deck.BuildDeck();
            deck.Shuffle();
            deck.Shuffle();
            deck.Shuffle();
            // deck.PrintDeck();
            // hand.AddCards(deck.DeckList[0]);
            // hand.AddCards(deck.DeckList[1]);
            var playerHand = deck.Deal();
            var computerHand = deck.Deal();

            Console.WriteLine("************************");
            playerHand.PrintHand();
            computerHand.PrintHand();
            Console.WriteLine("************************");
            deck.PrintDeck();



        }
    }
}


/*

have a plan on how to handle keeping track of cards in deck after dealing. 

Need to pass AddCards a Card obj in order to run it. Need to get that from the deck. 
Need to either get rid of the Card that is dealt inside of the deck, or just keep track of what was dealt (don't want a 7 of hearts being delt but also played on flop)

Currently removing cards after they are dealt, so we will need to re-build the deck everytime a new round starts.

*/