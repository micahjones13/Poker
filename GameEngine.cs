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
        //If the first value is not 1 (ace), then we can return.
        if (orderedFlush[0].Value != 1)
        {
            return null;
        }

        var valueShouldBe = 13;
        //int i = 1, because we already checked for the ace. We want to check orderedFlush[1] and on 
        for (int i = 1; i < orderedFlush.Count; i++)
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

        var playerHand = hand.HandList;
        //Need to add table cards to hand cards to see all possibilites 
        playerHand.AddRange(table.TableList);
        var ordered = playerHand.OrderBy(card => card.Value).ToList(); //* ordering the cards by ascending(smallest to biggest) card value                                             
        var orderedUnique = ordered.GroupBy(card => card.Value).Select(cards => cards.First()).ToList(); //* Takes the ordered list, filters out any duplicates

        var sets = new Dictionary<int, List<Card>>(); //initialize our dict



        //loop through orderd list, fill in sets dict
        for (int i = 0; i < orderedUnique.Count; i++)
        {
            //get the card out of orderedUnique at i
            var card = orderedUnique[i];
            //set the keyValue to the difference between card.Value and i
            var keyValue = card.Value - i;

            //if record already exists, add it to the list. Otherwise, add a new dict record
            //if the sets at the keyValue is null, there is no entry for that keyValue, so make one
            if (sets[keyValue] == null)
            {
                //initializes a list of cards at that keyValue
                sets[keyValue] = new List<Card>();
            }
            //adds cards to that list
            sets[keyValue].Add(card);


        }

        //*Dealing with aces here. 
        //For each of the lists in the sets dictionary, we want to find lists that have 5 elements, and possilby ones that contain a king
        //so that we can see if an ace would also be part of it.
        foreach (var list in sets)
        {
            //If the list has 4 or more elements and contains a king, possiblity of an ace needing ot be added too
            if (list.Value.Count() >= 4 && list.Value.Any(card => card.Value == 13))
            {
                //store if there is an ace in the playerHand(combo of hand and table at this point). Returns the card, or null if not found
                var ace = playerHand.FirstOrDefault(card => card.Value == 1);
                //If it's not null, there is an ace available. Add it to this list now
                if (ace != null)
                {
                    list.Value.Add(ace);
                }
            }
            //if it's hitting this if, there is not list with a king and 4 or more elements, so check if there is a straight anywhere else.
            if (list.Value.Count() >= 5)
            {
                //if there was a list with 5 or more elements, then we want to take the top 5
                //Order is our extension, which orders by decsending based on card.Value. We take the top 5 of those, return it
                return list.Value.Order().Take(5).ToList();
            }

        }
        //if reaching this return, there is no straight. Return null.
        return null;

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