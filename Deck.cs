using System;
using System.Collections.Generic;

class Deck
{
    public List<Card> DeckList { get; private set; }
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
                switch (j)
                {
                    case 4:
                        DeckList.Add(new Card(i, CardType.Hearts));
                        break;
                    case 3:
                        DeckList.Add(new Card(i, CardType.Diamonds));
                        break;
                    case 2:
                        DeckList.Add(new Card(i, CardType.Spades));
                        break;
                    case 1:
                        DeckList.Add(new Card(i, CardType.Clubs));
                        break;
                }
            }

        }
    }

    public void PrintDeck()
    {
        Console.WriteLine($"The deck has {DeckList.Count}");
        for (int i = 0; i < DeckList.Count; i++)
        {
            Console.WriteLine($"{DeckList[i].Value} of {DeckList[i].Type}");
        }
    }

    public void Shuffle()
    {

    }


}


//bulid deck, display all cards in deck