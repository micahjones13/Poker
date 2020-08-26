using System;
using System.Collections.Generic;

//This whole class is specific to Texas hold em
class TexasTable
{
    public List<Card> TableList { get; private set; }
    public Deck Deck { get; set; }

    public TexasTable(Deck deck)
    {
        Deck = deck;
        TableList = new List<Card>(); //initialize

    }
    public void BuildTable()
    {

    }

    //Flop and TurnRiver doing the same thing. Maybe combine later
    public void Flop()
    {
        var sendList = Deck.SendCard(3);
        TableList.AddRange(sendList);
    }
    public void TurnRiver()
    {
        var sendList = Deck.SendCard(1);
        TableList.AddRange(sendList);
    }
    public void PrintTable()
    {
        TableList.PrintCards();
    }
}