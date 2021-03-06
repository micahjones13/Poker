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
            ChooseGame();
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

    public void ChooseGame()
    {
        Console.WriteLine("Which game do you want to play? 1 - Poker, 2 - BlackJack, 3 - Quit");
        var res = Console.ReadLine();
        if (res == "1")
        {
            TexasHoldEm();
        }
        else if (res == "2")
        {
            BlackJack();
        }
        else
        {
            System.Environment.Exit(1);
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
        var tableCopy = new TexasTable(new Deck());

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
        if (FullHouse(table, playerHand) != null && FullHouse(table, computerHand) == null)
        {
            DeclareWinner(FullHouse(table, playerHand), "Player", "Full House");
            return;
        }
        else if (FullHouse(table, playerHand) == null && FullHouse(table, computerHand) != null)
        {
            DeclareWinner(FullHouse(table, computerHand), "Computer", "Full House");
            return;
        }
        //              3 of a kind1                                        Pair                            Comp does not have 3 of a kind
        // if (AnyOfAKind(table, playerHand, 3) != null && AnyOfAKind(table, playerHand, 2) != null && AnyOfAKind(table, computerHand, 3) == null)
        // {
        //     //player wins
        //     DeclareWinner(AnyOfAKind(table, playerHand, 3), "Player", "Full House");
        //     return;

        // }
        // else if ((AnyOfAKind(table, computerHand, 3) != null && AnyOfAKind(table, computerHand, 2) != null && AnyOfAKind(table, playerHand, 3) == null))
        // {
        //     //comp wins
        //     DeclareWinner(AnyOfAKind(table, computerHand, 3), "Computer", "Full House");
        //     return;

        // }
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
        if (TwoPair(table, playerHand) != null && TwoPair(table, computerHand) == null)
        {
            DeclareWinner(TwoPair(table, playerHand), "Player", "2 Pair");
            return;
        }
        else if (TwoPair(table, playerHand) == null && TwoPair(table, computerHand) != null)
        {
            DeclareWinner(TwoPair(table, computerHand), "Computer", "2 Pair");
            return;
        }
        //! Problem here. 2 pairs will always be true, since anyofakind function returns the first occurence of a pair, and doesn't remove that pair when called again.
        //! Problem: this check still won't work if player has 2 pair, but computer has 1. It doesn't make it into the if, because computer has a pair.
        // if (AnyOfAKind(table, playerHand, 2) != null && AnyOfAKind(table, computerHand, 2) == null)
        // {
        //     //player has 1 pair, need to check if two. Modify playerHand to remove the first pair
        //     var returnList = AnyOfAKind(table, playerHand, 2);
        //     //copy the playerHand so we can remove the first pair
        //     var playerHandCopy = new Hand();
        //     //table copy as well so we can remove a pair from table
        //     tableCopy.TableList.AddRange(table.TableList);
        //     playerHandCopy.HandList.AddRange(playerHand.HandList);
        //     playerHandCopy.HandList.Remove(returnList[0]);
        //     tableCopy.TableList.Remove(returnList[0]);
        //     //check if a pair still reamins
        //     if (AnyOfAKind(table, playerHandCopy, 2) != null)
        //     {
        //         //player has 2 pair, wins
        //         DeclareWinner(AnyOfAKind(table, playerHandCopy, 2), "Player", "2 Pair");
        //         return;


        //     }
        // }
        // else if (AnyOfAKind(table, playerHand, 2) == null && AnyOfAKind(table, computerHand, 2) != null)
        // {
        //     //comp has 1 pair
        //     var returnList = AnyOfAKind(table, computerHand, 2);
        //     var computerHandCopy = new Hand();
        //     computerHandCopy.HandList.AddRange(computerHand.HandList);
        //     tableCopy.TableList.AddRange(table.TableList);
        //     computerHandCopy.HandList.Remove(returnList[0]);
        //     //! not actually removing from tableCopy, because it would have to be the same value and suit
        //     tableCopy.TableList.Remove(returnList[0]);
        //     //check if a pair still reamins
        //     if (AnyOfAKind(table, computerHandCopy, 2) != null)
        //     {
        //         //computer has 2 pair, wins
        //         DeclareWinner(AnyOfAKind(table, computerHand, 2), "Computer", "2 Pair");
        //         return;

        //     }
        // }
        //Pair
        if (AnyOfAKind(table, playerHand, 2) != null && AnyOfAKind(table, computerHand, 2) == null)
        {
            //player wins

            DeclareWinner(AnyOfAKind(table, playerHand, 2), "Player", "Pair");
            return;


        }
        else if (AnyOfAKind(table, playerHand, 2) == null && AnyOfAKind(table, computerHand, 2) != null)
        {
            //comp wins

            DeclareWinner(AnyOfAKind(table, computerHand, 2), "Computer", "Pair");
            return;
        }

        //high card
        //ordering each hand, comparing the first value in each. Highest one wins
        var orderedPlayer = playerHand.HandList.Order();
        var orderedComputer = computerHand.HandList.Order();



        //if player has an ace and computer doesn't, or if the value of player's high card is greater than comp's
        if (orderedPlayer[0].Value == 1 && orderedComputer[0].Value != 1)
        {
            DeclareWinner(orderedPlayer, "Player", "High Card");
            return;
        }
        else if (orderedPlayer[0].Value > orderedComputer[0].Value)
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
    public List<Card> TwoPair(TexasTable table, Hand hand)
    {
        //create new instance of hand, copy the handlist to be the same as hand passed in, run checks, remove from copy
        var playerHandCopy = new Hand();
        playerHandCopy.HandList.AddRange(hand.HandList);
        // 9 Q - Q 2 9 7 1
        // rm Q from hand
        // 9 - Q 2 9 7 1
        var returnList = AnyOfAKind(table, hand, 2);
        //if the returnList is not null, we have at least 1 pair.
        if (returnList != null)
        {
            //remove the first card in return list from hand, run check again to see if still a pair
            // need to combine with table, so that if the table has a pair
            // playerHandCopy.HandList.AddRange(table.TableList);
            playerHandCopy.HandList.Remove(returnList[0]);
            var tableCopy = new TexasTable(new Deck());
            tableCopy.TableList.AddRange(table.TableList);
            tableCopy.TableList.Remove(returnList[0]);
            if (AnyOfAKind(tableCopy, playerHandCopy, 2) != null)
            {
                return returnList;
            }
        }
        return null;
    }
    public List<Card> FullHouse(TexasTable table, Hand hand)
    {
        var playerHandCopy = new Hand();
        playerHandCopy.HandList.AddRange(hand.HandList);
        var threeOfAKind = AnyOfAKind(table, hand, 3);
        if (threeOfAKind != null)
        {
            //they have 3 of a kind, check for pair
            //need to remove at least 2 from the handList, or else the pair will return with values that were used in the 3ofakind check
            playerHandCopy.HandList.Remove(threeOfAKind[0]);
            playerHandCopy.HandList.Remove(threeOfAKind[0]);
            if (AnyOfAKind(table, playerHandCopy, 2) != null)
            {
                return threeOfAKind;
            }
        }
        return null;
    }

    public void TexasHoldEm()
    {


        do
        {
            var deck = new Deck();
            var table = new TexasTable(deck);

            deck.BuildDeck(); //need to rebuild the deck every round


            // Console.WriteLine($"The deck has {deck.DeckList.Count}");

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

    public void HitOrStay(Hand playerHand, Deck deck)
    {
        bool hit = true;
        do
        {
            Console.WriteLine($"Current total: {Bust(playerHand)}");
            Console.WriteLine("1 - Hit or 2 - stay?");
            var res = Console.ReadLine();
            if (res == "1")
            {
                //hit
                playerHand = deck.Deal(playerHand, 1);
                //print hand
                Console.WriteLine("*******************************");
                Console.WriteLine("Player Hand: ");
                playerHand.PrintHand();
                Console.WriteLine("*******************************");
                //run check if bust function
                if (Bust(playerHand) > 21)
                {
                    Console.WriteLine("*******************************");
                    Console.WriteLine($"Player Busts! Total: {Bust(playerHand)}");
                    Console.WriteLine("*******************************");
                    break;
                }

            }
            else
            {
                //stay
                Console.WriteLine($"Player stays with a total of: {Bust(playerHand)}");
                Console.WriteLine("*******************************");
                hit = false;
            }
        }
        while (hit);
    }
    public void ComputerHitOrStay(Hand computerHand, Deck deck)
    {
        //have some calculations to decide if the computer will hit or stay
        //we could make it so that the computer won't hit if the distance to 21 is small enough
        var sum = Bust(computerHand);
        do
        {
            //if computer is at 15 or less, then get another card
            if (sum <= 16)
            {
                deck.Deal(computerHand, 1);
                Console.WriteLine("Computer hand: ");
                computerHand.PrintHand();
                Console.WriteLine("*******************************");
                sum = Bust(computerHand);
            }
            else break;
        } while (sum < 21);

        if (sum > 21)
        {
            Console.WriteLine($"Computer busts! Total: {sum}");
            Console.WriteLine("*******************************");
        }
        else
        {
            Console.WriteLine($"Computer stays with {sum}");
            Console.WriteLine("*******************************");
        }
    }
    public int Bust(Hand playerHand)
    {

        //check values in hand to see if over 21
        var sum = playerHand.HandList.Sum(card => card.Value);
        // bool isAce = playerHand.HandList.Any(card => card.Value == 1);
        var aceList = playerHand.HandList.Where(card => card.Value == 1);
        var faceList = playerHand.HandList.Where(card => card.Value == 13 || card.Value == 12 || card.Value == 11);

        //getting all aces kings, queens and jacks
        var kingList = playerHand.HandList.Where(card => card.Value == 13);
        var queenList = playerHand.HandList.Where(card => card.Value == 12);
        var jackList = playerHand.HandList.Where(card => card.Value == 11);
        var aceAmount = aceList.Count();
        var faceAmount = faceList.Count();
        var kingAmount = kingList.Count();
        var queenAmount = queenList.Count();
        var jackAmount = jackList.Count();

        //if there are no aces or face cards, we can just check the sum amount
        if (sum > 21 && aceAmount == 0 && faceAmount == 0)
        {
            return sum;
        }
        //if there are face cards, we need them to all be equal to 10.
        if (faceAmount > 0)
        {
            //Subtract the correct ammount for each to make them equal to 10
            for (int i = kingAmount; i > 0; i--)
            {
                sum -= 3;
            }
            for (int i = queenAmount; i > 0; i--)
            {
                sum -= 2;
            }
            for (int i = jackAmount; i > 0; i--)
            {
                sum -= 1;
            }

        }
        //check how many aces. Add 10 to sum for each ace. If sum > 21, no longer add 10 for 1 ace.
        if (aceAmount > 0)
        {
            //add 10 to sum for each ace unless it's gonna bust
            for (int i = aceAmount; i > 0; i--)
            {
                sum += 10;
                if (sum > 21)
                {
                    sum -= 10;
                    break;
                }
            }
            if (sum > 21)
            {
                return sum;
            }
        }

        return sum;


    }
    public void BlackJackWinner(Hand playerHand, Hand computerHand)
    {
        var playerSum = Bust(playerHand);
        var computerSum = Bust(computerHand);
        //player busts
        if (playerSum > 21 && computerSum <= 21)
        {
            Console.WriteLine("Computer wins!");
        }
        //computer busts
        else if (computerSum > 21 && playerSum <= 21)
        {
            Console.WriteLine("Player wins!");
        }
        else if (playerSum <= 21 && playerSum > computerSum)
        {
            Console.WriteLine("Player wins!");
        }
        else if (computerSum <= 21 && computerSum > playerSum)
        {
            Console.WriteLine("Computer wins!");
        }
        else if (computerSum == playerSum)
        {
            Console.WriteLine("It's a tie!");
        }
        else
        {
            Console.WriteLine("Computer and player both bust!");
        }

    }
    public void BlackJack()
    {

        do
        {
            var deck = new Deck();
            deck.BuildDeck();

            deck.Shuffle();
            deck.Shuffle();
            deck.Shuffle();
            var playerHand = deck.Deal(null, 2);
            var computerHand = deck.Deal(null, 2);
            //Initial Hands
            Console.WriteLine("*******************************");
            Console.WriteLine("Player Hand: ");
            playerHand.PrintHand();
            Console.WriteLine("*******************************");
            Console.WriteLine("Computer hand: ");
            computerHand.PrintHand();
            Console.WriteLine("*******************************");
            //Ask to hit or stay
            HitOrStay(playerHand, deck);
            ComputerHitOrStay(computerHand, deck);
            BlackJackWinner(playerHand, computerHand);

        }
        while (!Quit());
    }
}

