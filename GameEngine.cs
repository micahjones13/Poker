using System;
using System.Collections.Generic;
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
    public List<Card> RoyalFlush(TexasTable table, Hand hand)
    {
        // royal flush - ace(14), king(13), queen(12), jack(11), 10 all of same suit
        // check to see if there is a flush first
        var flushList = Flush(hand, table);
        if (flushList == null)
        {
            return null;
        }
        //if not null, it has the 5 values
        var orderedFlush = flushList.Order(); //ordered by decscending, but ace is treated as 14
        //check to see if the values required for a royal flush are there
        //if the first value after ordering it isn't ace, there is no royal flush
        //Have a var that starts at 14, decreases every loop to check for correct values
        var valueShouldBe = 14;
        for (int i = 0; i < orderedFlush.Count(); i++)
        {
            //if not equal at any point, there is no royal flush
            if (orderedFlush[i].Value != valueShouldBe)
            {
                return null;
            }
            valueShouldBe--;
        }
        return orderedFlush;

    }
    //Want to define a flush, so that we can use it in the royalflush
    public List<Card> Flush(Hand hand, TexasTable table)
    {
        // flush - 5 cards of same suit

        var type = hand.HandList[0].Type;
        var type2 = hand.HandList[1].Type;
        if (hand.HandList[0].Type != hand.HandList[1].Type)
        {
            return null; //use a null check to see if their was a flush or not
        }
        var matchingTypesList = table.TableList.Where(card => card.Type == type); //creates new list matchign the where condition
        //matching list needs to be at least 3,
        if (matchingTypesList.Count() < 3)
        {
            return null;
        }
        //needs to have the best 5 cards avaliable, but of the same type
        var orderedList = matchingTypesList.Order();
        var ans = orderedList.Take(3).ToList();
        ans.AddRange(hand.HandList);
        return ans;

    }
    public List<Card> Straight(Hand hand, TexasTable table)
    {
        //need to combine hand and table, but only table values that are in sequence with hand
        var playerHand = hand.HandList;
        playerHand.AddRange(table.TableList);
        var ordered = playerHand.Order();
        // need to check ordered for a sequence matching a straight (5 in a row);
        // loop through the orderd list, check if the diff between 2 values is > 1. If not, add to new list
        var newList = new List<Card>();

        //loop through orderd list, looping the count - 1 times to avoid out of bounds error
        for (int i = 0; i < ordered.Count - 1; i++)
        {
            // if the current value - next value is 1, or the current value - prev value is -1, then they are a sequence
            if (ordered[i].Value - ordered[i + 1].Value == 1 || ordered[i].Value - ordered[i - 1].Value == -1)
            {
                //store them inside newList
                newList.Add(ordered[i]);
            }
        }
        //if newlist doesn't have enough cards for a flush(5), then just return null
        if (newList.Count() < 5)
        {
            return null;
        }
        return newList;

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

/*
Winning Hands: 
Royal Flush
Straight Flush (five cards in a sequence, all one suit)
Four of a kind
Full House (3 of a kind with a pair)
Flush
Straight (5 in a sequence)
Three of a kind
Two pair (2 diff pairs)
Pair
High Card
*/