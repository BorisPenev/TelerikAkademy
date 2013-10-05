using System.Linq;
using LongestSubsequence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LongestSubsequence.Tests
{
    
    
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProgramTest
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
        ///A test for GetLongestSeubsequence
        ///</summary>
        [TestMethod()]
        public void GetLongestSeubsequenceTestBegin()
        {
            var numbers = 4444385652224335.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> expected = 4444.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> actual;
            actual = Program.GetLongestSeubsequence(numbers);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetLongestSeubsequence
        ///</summary>
        [TestMethod()]
        public void GetLongestSeubsequenceTestEnd()
        {
            var numbers = 24444343355555.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> expected = 55555.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> actual;
            actual = Program.GetLongestSeubsequence(numbers);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetLongestSeubsequence
        ///</summary>
        [TestMethod()]
        public void GetLongestSeubsequenceTestMiddle()
        {
            var numbers = 234444343355.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> expected = 4444.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> actual;
            actual = Program.GetLongestSeubsequence(numbers);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetLongestSeubsequence
        ///</summary>
        [TestMethod()]
        public void GetLongestSeubsequenceTestDouble()
        {
            var numbers = 23444434333355.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> expected = 4444.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            List<int> actual;
            actual = Program.GetLongestSeubsequence(numbers);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
