using System;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            var table = new TexasTable(deck);
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
            // deck.PrintDeck();
            Console.WriteLine("FLOP");

            table.Flop();
            table.PrintTable();
            Console.WriteLine("************************");
            Console.WriteLine("FLOP + RiiiIVER");
            table.TurnRiver();
            table.PrintTable();



        }
    }
}


/*

have a plan on how to handle keeping track of cards in deck after dealing. 

Need to pass AddCards a Card obj in order to run it. Need to get that from the deck. 
Need to either get rid of the Card that is dealt inside of the deck, or just keep track of what was dealt (don't want a 7 of hearts being delt but also played on flop)

Currently removing cards after they are dealt, so we will need to re-build the deck everytime a new round starts.

Game starts, phase 1 shows 3 cards, phase 2 shows one, phase 3 shows one
Test out new functions, print them and see where we're at
Write out a game-flow and what we expect to happen in the game 
*/