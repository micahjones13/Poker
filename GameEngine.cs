using System;

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
    public void TexasHoldEm()
    {
        var deck = new Deck();
        var table = new TexasTable(deck);

        do
        {
            deck.BuildDeck(); //need to rebuild the deck every round

            // Console.WriteLine($"The deck has {deck.DeckList.Count}");
            Quit();
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