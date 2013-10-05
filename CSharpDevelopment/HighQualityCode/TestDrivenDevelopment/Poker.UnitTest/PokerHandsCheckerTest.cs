using System.Collections.Generic;
using Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Poker.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PokerHandsCheckerTest and is intended
    ///to contain all PokerHandsCheckerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PokerHandsCheckerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for PokerHandsChecker Constructor
        ///</summary>
        [TestMethod()]
        public void PokerHandsCheckerConstructorTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CompareHands
        ///</summary>
        //[TestMethod()]
        //public void CompareHandsTest()
        //{
        //    PokerHandsChecker target = new PokerHandsChecker(); // TODO: Initialize to an appropriate value
        //    IHand firstHand = null; // TODO: Initialize to an appropriate value
        //    IHand secondHand = null; // TODO: Initialize to an appropriate value
        //    int expected = 0; // TODO: Initialize to an appropriate value
        //    int actual;
        //    actual = target.CompareHands(firstHand, secondHand);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for IsFlush
        ///</summary>
        [TestMethod()]
        public void IsFlushTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotFlushTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Jack, CardSuit.Clubs));
            cards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotFlushTest2()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsFourOfAKind
        ///</summary>
        [TestMethod()]
        public void IsFourOfAKindTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Four, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Four, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Four, CardSuit.Clubs));
            cards.Add(new Card(CardFace.Four, CardSuit.Spades));
            cards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsFourOfAKind(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotFourOfAKindTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            cards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Eight, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsFourOfAKind(hand);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsFullHouse
        ///</summary>
        [TestMethod()]
        public void IsFullHouseTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            cards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Eight, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsFullHouse(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotFullHouseTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            cards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Eight, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsFullHouse(hand);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsHighCard
        ///</summary>
        [TestMethod()]
        public void IsHighCardTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Nine, CardSuit.Spades));
            cards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            cards.Add(new Card(CardFace.Four, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsHighCard(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotHighCardTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Two, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Six, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsHighCard(hand);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsOnePair
        ///</summary>
        [TestMethod()]
        public void IsOnePairTest()
        {
            PokerHandsChecker target = new PokerHandsChecker(); // TODO: Initialize to an appropriate value
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Two, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Six, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsOnePair(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotOnePairTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Two, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Six, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsOnePair(hand);
            Assert.AreEqual(expected, actual);
        }

        #region IsStraight

        /// <summary>
        ///A test for IsStraight
        ///</summary>
        [TestMethod()]
        public void IsStraightTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ten, CardSuit.Spades));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsStraight(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsStraightTest2()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Four, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsStraight(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsStraightTest3()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsStraight(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotStraightTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsStraight(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotStraightTest2()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsStraight(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotStraightTest3()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsStraight(hand);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region IsStraightFlush
        /// <summary>
        ///A test for IsStraightFlush
        ///</summary>
        [TestMethod()]
        public void IsStraightFlushTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsStraightFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsStraightFlushTest2()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Four, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Three, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Two, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsStraightFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsStraightFlushTest3()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsStraightFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotStraightFlushTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsStraightFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotStraightFlushTest2()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsStraightFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotStraightFlushTest3()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsStraightFlush(hand);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        /// <summary>
        ///A test for IsThreeOfAKind
        ///</summary>
        [TestMethod()]
        public void IsThreeOfAKindTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Three, CardSuit.Spades));
            cards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Three, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsThreeOfAKind(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotThreeOfAKindTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Three, CardSuit.Spades));
            cards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsThreeOfAKind(hand);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsTwoPair
        ///</summary>
        [TestMethod()]
        public void IsTwoPairTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Jack, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsTwoPair(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotTwoPairTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsTwoPair(hand);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValidHand
        ///</summary>
        [TestMethod()]
        public void IsValidHandTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = true;
            bool actual;
            actual = target.IsValidHand(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotValidHandTest()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsValidHand(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotValidHandTest2()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Hearts));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Clubs));
            cards.Add(new Card(CardFace.King, CardSuit.Spades));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsValidHand(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotValidHandTest3()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Nine, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Clubs));
            IHand hand = new Hand(cards);
            bool expected = false;
            bool actual;
            actual = target.IsValidHand(hand);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotValidHandTest4()
        {
            PokerHandsChecker target = new PokerHandsChecker();
            IHand hand = new Hand(null);
            bool expected = false;
            bool actual;
            actual = target.IsValidHand(hand);
            Assert.AreEqual(expected, actual);
        }
    }
}
