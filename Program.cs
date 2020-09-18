using System;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {

            var deck = new Deck();
            var game = new GameEngine();
            var hand = new Hand();
            var table = new TexasTable(deck);
            game.TexasHoldEm();




            //* Testing
            var gameTest = new GameTest(game, hand, deck, table);
            // gameTest.RunTests();



        }
    }
}



