using System;
using System.Collections.Generic;
using System.Linq;
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
            //* Run Game
            // game.TexasHoldEm();
            game.BlackJack();




            //* Testing
            var gameTest = new GameTest(game, hand, deck, table);
            // gameTest.RunTests();



            // List<int> testList = new List<int> { { 1 }, { 2 }, { 1 } };

            // var sum = testList.Sum();
            // for (int i = 2; i > 0; i--)
            // {
            //     sum += 10;
            //     if (sum > 21)
            //     {
            //         sum -= 10;
            //         break;
            //     }
            // }
            // Console.WriteLine(sum);


        }
    }
}

/*
BlackJack: 
Deal 2 cards to start
Ask player stand or hit
Function to check busts
If one busts, other person wins
If both stand, closest to 21 wins


*/

