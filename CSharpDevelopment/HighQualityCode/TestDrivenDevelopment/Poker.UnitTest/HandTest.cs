using Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Poker.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for HandTest and is intended
    ///to contain all HandTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HandTest
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
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Five, CardSuit.Spades));
            cards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            Hand target = new Hand(cards);
            string expected = "A♣K♦Q♥5♠A♥";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Hand Constructor
        ///</summary>
        [TestMethod()]
        public void HandConstructorTest()
        {
            IList<ICard> cards = new List<ICard>();
            cards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            cards.Add(new Card(CardFace.Five, CardSuit.Spades));
            cards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            var target = new Hand(cards);
            Assert.IsNotNull(target);
        }
        
    }
}
