using System;
class Card
{
    public int Value { get; set; }
    public CardType Type { get; set; }

    //*Built out the cards being added without constructor
    // public Card(int cardValue, CardType cardType)
    // {
    //     Value = cardValue;
    //     Type = cardType;
    // }
    public void PrintCard()
    {

        switch (Value)
        {
            case 11:
                Console.WriteLine($"Jack of {Type}");
                break;
            case 12:
                Console.WriteLine($"Queen of {Type}");
                break;
            case 13:
                Console.WriteLine($"King of {Type}");
                break;
            case 1:
                Console.WriteLine($"Ace of {Type}");
                break;
            default:
                Console.WriteLine($"{Value} of {Type}");
                break;
        }
    }
}