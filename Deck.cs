using System;
using System.Collections.Generic;

class Deck
{
    public List<Card> DeckList { get; private set; } //list of type Card
    public int DeckSize = 52;

    public void BuildDeck()
    {
        DeckList = new List<Card>(); //initialize the list

        //for loop to add all cards to the DeckList
        //nested for loops multiply by the amount it runs the nest, so here 13 * 4 = 52
        for (int i = 13; i > 0; i--)
        {
            //nested for loop to add typing 
            //4 suits, 13 of each card in each suit. 
            for (int j = 4; j > 0; j--)
            {
                //add 1 of each of the 4 suits
                switch (j)
                {
                    case 4:
                        //This way adds values and types to the card, without the constructor
                        DeckList.Add(new Card() { Value = i, Type = CardType.Hearts });
                        break;
                    case 3:
                        DeckList.Add(new Card() { Value = i, Type = CardType.Diamonds });
                        break;
                    case 2:
                        DeckList.Add(new Card() { Value = i, Type = CardType.Spades });
                        break;
                    case 1:
                        DeckList.Add(new Card() { Value = i, Type = CardType.Clubs });
                        break;
                }
            }

        }

    }

    public void PrintDeck()
    {
        // Console.WriteLine($"The deck has {DeckList.Count} cards.");
        // for (int i = 0; i < DeckList.Count; i++)
        // {
        //     Console.WriteLine($"{DeckList[i].Value} of {DeckList[i].Type}");
        // }
        foreach (var Card in DeckList)
        {
            Card.PrintCard();
        }
    }


    public void Shuffle()
    {
        Random rng = new Random();
        int n = DeckList.Count; //n = 52
        while (n > 1)
        {
            n--; //n = 51
            int k = rng.Next(n + 1); //k = 0 - 52 (random)
            Card value = DeckList[k]; // new variable value (of card type), now stores DeckList[k], k being random(0-52)
            DeckList[k] = DeckList[n]; //now taking DeckList[k], and storing DeckList[n] inside 
            DeckList[n] = value; //DeckList[n] now has the value that was previously at DeckList[k]

            //DeckList[k] is 2 of hearts to start. (example, not actually 2 of hearts)
            //DeckList[n] is ace of spades to start.
            // value = 2 of hearts (decklist[k])
            // (decklist[k])2 of hearts = decklist[n]ace of spades, so now decklist[k] is ace of spades, now 2 of hearts.
            //decklist[n] used to be ace of spades, is now 2 of hearts
        }
    }

    public Hand Deal()
    {
        var hand = new Hand();
        hand.AddCards(DeckList[0]);
        hand.AddCards(DeckList[1]);

        DeckList.RemoveRange(0, 2);
        // DeckList.RemoveAt(1);
        return hand;

    }


}

/*




*/
