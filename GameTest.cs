using System;
using System.Collections.Generic;
class GameTest
{
    public GameEngine TestGame { get; set; }
    public Hand TestHand { get; set; }
    public Hand ComputerTestHand { get; set; }
    public Deck TestDeck { get; set; }
    public TexasTable TestTable { get; set; }
    public GameTest(GameEngine gametest, Hand testhand, Deck testdeck, TexasTable testtable)
    {
        TestGame = gametest;
        TestHand = testhand;
        TestDeck = testdeck;
        TestTable = testtable;
    }
    public void RoyalFlushTest()
    {
        //Fill in hand list and table list with desired cards for testing
        TestHand.HandList = new List<Card> { new Card { Value = 1, Type = CardType.Hearts }, new Card { Value = 13, Type = CardType.Hearts } };
        TestTable.TableList = new List<Card> { new Card { Value = 12, Type = CardType.Hearts }, new Card { Value = 11, Type = CardType.Hearts }, new Card { Value = 10, Type = CardType.Hearts } };


        //Run RoyalFlush function, expect it to return a hand
        var royalFlushReturn = TestGame.RoyalFlush(TestTable, TestHand);
        if (royalFlushReturn != null)
        {
            Console.WriteLine("SUCCESS: Royal Flush working");
            Console.WriteLine("Royal Flush Return Hand:");
            royalFlushReturn.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("Royal Flush returned null");
            Console.WriteLine("*************************");
        }
    }

    public void StraightFlushTest()
    {
        //5 cards in sequence, all 1 suit. Using 10, 9, 8, 7, 6 of hearts for the test.
        TestHand.HandList = new List<Card> { new Card { Value = 10, Type = CardType.Hearts }, new Card { Value = 9, Type = CardType.Hearts } };
        TestTable.TableList = new List<Card> { new Card { Value = 8, Type = CardType.Hearts }, new Card { Value = 7, Type = CardType.Hearts }, new Card { Value = 6, Type = CardType.Hearts } };

        //Run Straight Flush function, expect a non-null return
        var straightFlushReturn = TestGame.StraightFlush(TestTable, TestHand);
        if (straightFlushReturn != null)
        {
            Console.WriteLine("SUCCESS: Straight flush Working");
            Console.WriteLine("Straight flush return: ");
            straightFlushReturn.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("FAIL: Straight Flush returned null");
            Console.WriteLine("*************************");
        }

    }
    public void FourOfAKindTest()
    {
        //4 matches, this case 2's of each suit, extraneous 6 so that there are 5 cards still
        TestHand.HandList = new List<Card> { new Card { Value = 2, Type = CardType.Hearts }, new Card { Value = 2, Type = CardType.Clubs } };
        TestTable.TableList = new List<Card> { new Card { Value = 2, Type = CardType.Diamonds }, new Card { Value = 2, Type = CardType.Spades }, new Card { Value = 6, Type = CardType.Hearts } };
        var fourOfAKindReturn = TestGame.AnyOfAKind(TestTable, TestHand, 4);
        if (fourOfAKindReturn != null)
        {
            Console.WriteLine("SUCCESS: 4 of a kind Working");
            Console.WriteLine("4 of a kind return: ");
            fourOfAKindReturn.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("FAIL: 4 of a kind returned null");
            Console.WriteLine("*************************");
        }

    }
    public void FullHouseTest()
    {
        // 3 of a kind with a pair. This case: 2 of hearts, clubs, diamonds & 3 of hearts, spades
        TestHand.HandList = new List<Card> { new Card { Value = 2, Type = CardType.Hearts }, new Card { Value = 2, Type = CardType.Clubs } };
        TestTable.TableList = new List<Card> { new Card { Value = 2, Type = CardType.Diamonds }, new Card { Value = 3, Type = CardType.Spades }, new Card { Value = 3, Type = CardType.Hearts } };
        var fullHouseReturn1 = TestGame.AnyOfAKind(TestTable, TestHand, 3); // Check for 3 of a kind first
        var fullHouseReturn2 = TestGame.AnyOfAKind(TestTable, TestHand, 2); // pair after
        if (fullHouseReturn1 != null && fullHouseReturn2 != null)
        {
            Console.WriteLine("SUCCESS: Full House Working");
            Console.WriteLine("Full house return: ");
            fullHouseReturn1.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("FAIL: Full hosue returned null");
            Console.WriteLine("*************************");
        }
    }
    public void FlushTest()
    {
        //Flush - all one suit, this case all hearts
        TestHand.HandList = new List<Card> { new Card { Value = 2, Type = CardType.Hearts }, new Card { Value = 3, Type = CardType.Hearts } };
        TestTable.TableList = new List<Card> { new Card { Value = 8, Type = CardType.Hearts }, new Card { Value = 6, Type = CardType.Hearts }, new Card { Value = 10, Type = CardType.Hearts } };
        var flushReturn = TestGame.Flush(TestHand, TestTable);
        if (flushReturn != null)
        {
            Console.WriteLine("SUCCESS: Flush Working");
            Console.WriteLine("Flush return: ");
            flushReturn.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("FAIL: Flush returned null");
            Console.WriteLine("*************************");
        }
    }
    public void StraightTest()
    {
        //Straight - 5 in sequence, this case 2 - 6 of hearts
        TestHand.HandList = new List<Card> { new Card { Value = 2, Type = CardType.Hearts }, new Card { Value = 3, Type = CardType.Hearts } };
        TestTable.TableList = new List<Card> { new Card { Value = 4, Type = CardType.Hearts }, new Card { Value = 5, Type = CardType.Hearts }, new Card { Value = 6, Type = CardType.Hearts } };
        var straightReturn = TestGame.Straight(TestHand, TestTable);
        if (straightReturn != null)
        {
            Console.WriteLine("SUCCESS: Straight working");
            Console.WriteLine("Straight return: ");
            straightReturn.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("FAIL: Straight returned null");
            Console.WriteLine("*************************");
        }
    }
    public void ThreeOfAKindTest()
    {
        //3 matching, this case 2 of hearts, diamonds, clubs
        TestHand.HandList = new List<Card> { new Card { Value = 2, Type = CardType.Hearts }, new Card { Value = 2, Type = CardType.Clubs } };
        TestTable.TableList = new List<Card> { new Card { Value = 2, Type = CardType.Diamonds }, new Card { Value = 3, Type = CardType.Spades }, new Card { Value = 4, Type = CardType.Hearts } };
        var threeOfAKindReturn = TestGame.AnyOfAKind(TestTable, TestHand, 3);
        if (threeOfAKindReturn != null)
        {
            Console.WriteLine("SUCCESS: 3 of a kind working");
            Console.WriteLine("3 of a kind return: ");
            threeOfAKindReturn.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("FAIL: 3 of a kind returned null");
            Console.WriteLine("*************************");
        }
    }
    public void TwoPairTest()
    {
        //2 diff pairs, this case 2 2's and 2 3's 
        TestHand.HandList = new List<Card> { new Card { Value = 2, Type = CardType.Hearts }, new Card { Value = 3, Type = CardType.Clubs } };
        TestTable.TableList = new List<Card> { new Card { Value = 2, Type = CardType.Diamonds }, new Card { Value = 3, Type = CardType.Spades }, new Card { Value = 4, Type = CardType.Hearts } };
        var testHandCopy = new Hand();
        testHandCopy.HandList.AddRange(TestHand.HandList);
        var firstPair = TestGame.AnyOfAKind(TestTable, TestHand, 2);
        var handWithoutRemoval = TestGame.AnyOfAKind(TestTable, TestHand, 2);


        if (firstPair != null)
        {
            // TestHand.HandList.Remove(firstPair[0]);
            testHandCopy.HandList.Remove(firstPair[0]);
            TestTable.TableList.Remove(firstPair[0]);
            var secondPair = TestGame.AnyOfAKind(TestTable, testHandCopy, 2); //! Needs to use 
            if (secondPair != null)
            {
                Console.WriteLine("SUCCESS: 2 pair Worked");
                Console.WriteLine("2 pair return");
                handWithoutRemoval.PrintCards();
                Console.WriteLine("*************************");
            }
            else
            {
                Console.WriteLine("FAIL: 2 pair returned only 1 pair");
                Console.WriteLine("*************************");
            }
        }
        else
        {
            Console.WriteLine("FAIL: 2 pair returned null during first pair check");
            Console.WriteLine("*************************");
        }
    }
    public void PairTest()
    {
        //2 matching, this case 2 2's
        TestHand.HandList = new List<Card> { new Card { Value = 2, Type = CardType.Hearts }, new Card { Value = 9, Type = CardType.Clubs } };
        TestTable.TableList = new List<Card> { new Card { Value = 2, Type = CardType.Diamonds }, new Card { Value = 5, Type = CardType.Spades }, new Card { Value = 8, Type = CardType.Hearts } };
        var pairReturn = TestGame.AnyOfAKind(TestTable, TestHand, 2);
        if (pairReturn != null)
        {
            Console.WriteLine("SUCCESS: Pair Worked");
            Console.WriteLine("Pair Return: ");
            pairReturn.PrintCards();
            Console.WriteLine("*************************");
        }
        else
        {
            Console.WriteLine("FAIL: Pair returned null");
            Console.WriteLine("*************************");
        }
    }
    // No test for high card becuase it's not actually a GameEngine function, it's doing the check in the DecideWinner. 
    public void RunPokerTests()
    {
        RoyalFlushTest();
        StraightFlushTest();
        FourOfAKindTest();
        FullHouseTest();
        FlushTest();
        StraightTest();
        ThreeOfAKindTest();
        TwoPairTest();
        PairTest();

    }
    //* Blackjack Tests
    public void BothBust()
    {
        //initializes hands for player(testhand) and computer, each with a sum of 25
        TestHand.HandList = new List<Card> { new Card { Value = 10, Type = CardType.Hearts }, new Card { Value = 10, Type = CardType.Diamonds }, new Card { Value = 5, Type = CardType.Diamonds } };
        ComputerTestHand.HandList = new List<Card> { new Card { Value = 10, Type = CardType.Clubs }, new Card { Value = 10, Type = CardType.Spades }, new Card { Value = 5, Type = CardType.Clubs } };
        //Run bust function on both, and expect the return to be >21
        if (TestGame.Bust(TestHand) > 21 && TestGame.Bust(ComputerTestHand) > 21)
        {
            Console.WriteLine("SUCCESS: BothBust");
        }
        else
        {
            Console.WriteLine("FAIL: BothBust");
            Console.WriteLine($"Comp sum: {TestGame.Bust(ComputerTestHand)}");
            Console.WriteLine($"Player sum: {TestGame.Bust(TestHand)}");
        }
    }
    public void AcesTests()
    {
        //Tests interactions with aces. 
        //2 Aces, expect value to be 11
        TestHand.HandList = new List<Card> { new Card { Value = 1, Type = CardType.Hearts }, new Card { Value = 1, Type = CardType.Diamonds } };
        //3 Aces, Expect value to be 12
        TestHand.HandList = new List<Card> { new Card { Value = 1, Type = CardType.Hearts }, new Card { Value = 1, Type = CardType.Diamonds } };
        //4 aces, expect value to be 13
        TestHand.HandList = new List<Card> { new Card { Value = 1, Type = CardType.Hearts }, new Card { Value = 1, Type = CardType.Diamonds } };




    }
    public void RunBlackJackTests()
    {

    }
}