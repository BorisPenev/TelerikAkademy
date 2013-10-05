using Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Poker.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CardTest and is intended
    ///to contain all CardTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CardTest
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
        public void ToStringAceClubsTest()
        {
            var target = new Card(CardFace.Ace, CardSuit.Clubs);
            string expected = "A♣";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringKingDiamondsTest()
        {
            var target = new Card(CardFace.King, CardSuit.Diamonds);
            string expected = "K♦";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringQueenHeartsTest()
        {
            var target = new Card(CardFace.Queen, CardSuit.Hearts);
            string expected = "Q♥";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToString5SpadesTest()
        {
            var target = new Card(CardFace.Five, CardSuit.Spades);
            string expected = "5♠";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
