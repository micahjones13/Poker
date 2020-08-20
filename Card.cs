using System;
class Card
{
    public int Value { get; set; }
    public CardType Type { get; set; }

    public Card(int cardValue, CardType cardType)
    {
        Value = cardValue;
        Type = cardType;
    }
    public void PrintCard()
    {
        Console.WriteLine($"{Value} of {Type}");
    }
}