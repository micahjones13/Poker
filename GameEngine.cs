using System;
using System.Linq;

class GameEngine
{

    public bool WantToFold = false;

    public bool Quit()
    {
        Console.WriteLine("Play? 1- yes 2 - no");
        var res = Console.ReadLine();
        if (res == "1")
        {
            return false;
        }
        else
        {

            return true;
        }
    }
    public void Fold()
    {
        //Maybe use this function for betting in the future
        Console.WriteLine("Would you like to fold? 1 - Yes 2 - No");
        var res = Console.ReadLine();
        if (res == "1")
        {
            WantToFold = false;
        }
        else
        {
            WantToFold = true;
        }
    }

    public void DecideWinner(TexasTable table, Hand playerHand, Hand computerHand)
    {
        // needs to use TableList and HandList to determine the hand(straight, flush, ect)
        // start at highest possible (royal flush), and as soon as one person has it and the other does not, they win
        // need to compare HandList to TableList see if the cards are there to meet the royal flush reqs


        // compare player's hand and computer's hand to see who's is better


    }
    public void RoyalFlush(TexasTable table, Hand hand)
    {
        // royal flush - ace(1), king, queen, jack, 10 all of same suit
        bool isMatching = false;
        var type = hand.HandList[0].Type;
        if (hand.HandList[0].Type == hand.HandList[1].Type)
        {
            isMatching = true;
        }
        else
        {
            return;
        }
        var filtered = table.TableList.Where(card => card.Type == type); //creates new list matchign the where condition

    }
    public void TexasHoldEm()
    {
        var deck = new Deck();
        var table = new TexasTable(deck);

        do
        {
            deck.BuildDeck(); //need to rebuild the deck every round

            // Console.WriteLine($"The deck has {deck.DeckList.Count}");
            if (Quit())
            {
                break;
            }
            deck.Shuffle();
            deck.Shuffle();
            deck.Shuffle();
            var playerHand = deck.Deal();
            var computerHand = deck.Deal();
            Console.WriteLine("You're hand:");
            playerHand.PrintHand();
            // Fold();
            table.Flop();
            Console.WriteLine("*****FLOP*****");
            table.PrintTable();

            //Fold();
            table.TurnRiver(); // 2nd phase
            Console.WriteLine("****RIVER****");

            table.PrintTable();

            //fold
            Console.WriteLine("****TURN****");
            table.TurnRiver(); // 3rd phase
            table.PrintTable();
            Console.WriteLine("********");



            Console.WriteLine("Computers hand: ");
            computerHand.PrintHand();




        } while (!Quit());
    }
}