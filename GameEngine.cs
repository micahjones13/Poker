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
        // start at highest possible (royal flush), and as soon as one person has it and the other does not, they win
        var playerList = RoyalFlush(table, playerHand);
        var computerList = RoyalFlush(table, computerHand);
        if (playerList != null && computerList == null)
        {
            //player wins
            DeclareWinner(playerList, "Player", "Royal Flush");
            return;

        }
        else if (RoyalFlush(table, playerHand) == null && RoyalFlush(table, computerHand) != null)
        {
            //comuter wins
            DeclareWinner(computerList, "Computer", "Royal Flush");
            return;
        }

        //straight flush
        if (StraightFlush(table, playerHand) != null && StraightFlush(table, computerHand) == null)
        {
            //player wins
            DeclareWinner(StraightFlush(table, playerHand), "Player", "Striaght Flush");
            return;

        }
        else if (StraightFlush(table, playerHand) == null && StraightFlush(table, computerHand) != null)
        {
            //comp wins
            DeclareWinner(StraightFlush(table, computerHand), "Computer", "Striaght Flush");
            return;

        }
        //4 of a kind
        if (AnyOfAKind(table, playerHand, 4) != null && AnyOfAKind(table, computerHand, 4) == null)
        {
            //player wins
            DeclareWinner(AnyOfAKind(table, playerHand, 4), "Player", "4 of a kind");
            return;

        }
        else if (AnyOfAKind(table, playerHand, 4) == null && AnyOfAKind(table, computerHand, 4) != null)
        {
            //comp wins
            DeclareWinner(AnyOfAKind(table, computerHand, 4), "Computer", "4 of a kind");
            return;

        }
        //Full House (3 of a kind, pair)
        //              3 of a kind1                                        Pair                            Comp does not have 3 of a kind
        if (AnyOfAKind(table, playerHand, 3) != null && AnyOfAKind(table, playerHand, 2) != null && AnyOfAKind(table, computerHand, 3) == null)
        {
            //player wins
            DeclareWinner(AnyOfAKind(table, playerHand, 3), "Player", "Full House");
            return;

        }
        else if ((AnyOfAKind(table, computerHand, 3) != null && AnyOfAKind(table, computerHand, 2) != null && AnyOfAKind(table, playerHand, 3) == null))
        {
            //comp wins
            DeclareWinner(AnyOfAKind(table, computerHand, 3), "Computer", "Full House");
            return;

        }
        //Flush
        if (Flush(playerHand, table) != null && Flush(computerHand, table) == null)
        {
            //player wins
            DeclareWinner(Flush(playerHand, table), "Player", "Flush");
            return;

        }
        else if (Flush(playerHand, table) == null && Flush(computerHand, table) != null)
        {
            //comp wins
            DeclareWinner(Flush(computerHand, table), "Computer", "Flush");
            return;


        }
        //straight 
        if (Straight(playerHand, table) != null && Straight(computerHand, table) == null)
        {
            //player wins
            DeclareWinner(Straight(playerHand, table), "Player", "Straight");
            return;


        }
        else if (Straight(playerHand, table) == null && Straight(computerHand, table) != null)
        {
            //comp wins
            DeclareWinner(Straight(computerHand, table), "Computer", "Straight");
            return;


        }
        //3 of a kind
        if (AnyOfAKind(table, playerHand, 3) != null && AnyOfAKind(table, computerHand, 3) == null)
        {
            //player wins
            DeclareWinner(AnyOfAKind(table, playerHand, 3), "Player", "3 of a kind");
            return;

        }
        else if (AnyOfAKind(table, playerHand, 3) == null && AnyOfAKind(table, computerHand, 3) != null)
        {
            //comp wins
            DeclareWinner(AnyOfAKind(table, computerHand, 3), "Computer", "3 of a kind");
            return;


        }
        // 2 pair (2 diff pair)
        //! Problem here. 2 pairs will always be true, since anyofakind function returns the first occurence of a pair, and doesn't remove that pair when called again.
        if (AnyOfAKind(table, playerHand, 2) != null && AnyOfAKind(table, computerHand, 2) == null)
        {
            //player has 1 pair, need to check if two. Modify playerHand to remove the first pair
            var returnList = AnyOfAKind(table, playerHand, 2);
            //! Not sure if this copy will work, since it's copying an instance of the hand class
            var playerHandCopy = new Hand();
            playerHandCopy.HandList.AddRange(playerHand.HandList);
            playerHandCopy.HandList.Remove(returnList[0]);
            //check if a pair still reamins
            if (AnyOfAKind(table, playerHandCopy, 2) != null)
            {
                //player has 2 pair, wins
                DeclareWinner(AnyOfAKind(table, playerHandCopy, 2), "Player", "2 Pair");
                return;


            }
        }
        else if (AnyOfAKind(table, playerHand, 2) == null && AnyOfAKind(table, computerHand, 2) != null)
        {
            //comp has 1 pair
            var returnList = AnyOfAKind(table, computerHand, 2);
            var computerHandCopy = new Hand();
            computerHandCopy.HandList.AddRange(computerHand.HandList);
            computerHandCopy.HandList.Remove(returnList[0]);
            //check if a pair still reamins
            if (AnyOfAKind(table, computerHandCopy, 2) != null)
            {
                //computer has 2 pair, wins
                DeclareWinner(AnyOfAKind(table, computerHand, 2), "Computer", "2 Pair");
                return;

            }
        }
        //Pair
        var pairPlayerReturn = AnyOfAKind(table, playerHand, 2);
        var pairCompReturn = AnyOfAKind(table, computerHand, 2);
        if (AnyOfAKind(table, playerHand, 2) != null && AnyOfAKind(table, computerHand, 2) == null)
        {
            //player wins
            Console.WriteLine("INSIDE WIN FOR PAIR");
            DeclareWinner(AnyOfAKind(table, playerHand, 2), "Player", "Pair");
            return;


        }
        else if (AnyOfAKind(table, playerHand, 2) == null && AnyOfAKind(table, computerHand, 2) != null)
        {
            //comp wins
            Console.WriteLine("INSIDE WIN FOR PAIR");
            DeclareWinner(AnyOfAKind(table, computerHand, 2), "Computer", "Pair");
            return;
        }

        //high card
        //ordering each hand, comparing the first value in each. Highest one wins
        var orderedPlayer = playerHand.HandList.Order();
        var orderedComputer = computerHand.HandList.Order();
        //Making copies of handlist for player and computer
        // var combinedPlayer = new List<Card>(playerHand.HandList);
        // var combinedComputer = new List<Card>(computerHand.HandList);
        // combining the table cards with each hand cards, in order to return the full list for the declarewinner function
        // combinedComputer.AddRange(table.TableList);
        // combinedPlayer.AddRange(table.TableList);


        //if player has an ace and computer doesn't, or if the value of player's high card is greater than comp's
        if (orderedPlayer[0].Value == 1 && orderedComputer[0].Value != 1 || orderedPlayer[0].Value > orderedComputer[0].Value)
        {
            DeclareWinner(orderedPlayer, "Player", "High Card");
            return;
        }
        else if (orderedComputer[0].Value == orderedPlayer[0].Value)
        {
            Console.WriteLine("It's a tie for high card! What happens now??");
            return;
        }
        else
        {
            DeclareWinner(orderedComputer, "Computer", "High Card");
            return;

        }


    }
    //* Declare winner function, just returns the winning hand and maybe a message of who won
    public List<Card> DeclareWinner(List<Card> hand, string winner, string winningHand)
    {
        Console.WriteLine($"{winner} wins with a {winningHand}!");
        hand.PrintCards();
        return hand;
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
    public List<Card> StraightFlush(TexasTable table, Hand hand)
    {
        var flushReturn = Flush(hand, table); //going to either return list that meets flush reqs, or null
        var straightReturn = Straight(hand, table); //going to either return list that meets straight reqs, or null.
        // if either function returns null, there is no straight flush, return null.
        if (flushReturn == null || straightReturn == null)
        {
            return null;
        }
        // if both do return a list, then there is a striaght flush. Need to return that hand.
        return straightReturn;

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

        var playerHand = new List<Card>(hand.HandList);
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
            if (!sets.ContainsKey(keyValue))
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

    public List<Card> AnyOfAKind(TexasTable table, Hand hand, int amountOfAKind)
    {
        //combine table and hand
        var playerHand = new List<Card>(hand.HandList);
        playerHand.AddRange(table.TableList);
        //order the list

        var returnHand = new List<Card>();

        // Select card.value, Count(*) as Dups
        // From card
        // GroupBy card.value having Count > 1
        //find any pairs, 3 of a kind, 4 of a kind
        //                  group by the card value,   where groupings have more than 1,     select the values: 
        var groupings = playerHand.GroupBy(card => card.Value)
            .Where(card => card.Count() == amountOfAKind)
            .Select(cardGrouping => new { cards = cardGrouping.ToList(), cardValue = cardGrouping.Key, Amount = cardGrouping.Count() })
            .OrderByDescending(card => (card.cardValue != 1 ? card.cardValue : 14))
            .ToList();
        //we have a list of values and the amount that they occur
        // need to go through the list, find the highest occurence 
        //order, look for first record that is 2 or more

        return groupings.FirstOrDefault()?.cards; // ?. only go into object if it is not null and exists
    }


    public void TexasHoldEm()
    {


        do
        {
            var deck = new Deck();
            var table = new TexasTable(deck);

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
            Console.WriteLine("Player hand:");
            playerHand.PrintHand();
            Console.WriteLine("Computer's Hand:");
            computerHand.PrintHand();
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



            DecideWinner(table, playerHand, computerHand);




        } while (!Quit());
    }
}

