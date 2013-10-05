using School;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace School.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for UniqueIdTest and is intended
    ///to contain all UniqueIdTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UniqueIdTest
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
        ///A test for NewId
        ///</summary>
        [TestMethod()]
        public void NewIdTest()
        {
            UniqueId.currentId = UniqueId.MinId;
            int expected = UniqueId.MinId;
            int actual = UniqueId.NewId();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NewIdOutOfRangeTest()
        {
            int expected = UniqueId.MinId;
            for (int i = 0; i < UniqueId.MaxId; i++)
            {
                int actual = UniqueId.NewId();
            }
        }

        /// <summary>
        ///A test for Shrink
        ///</summary>
        [TestMethod()]
        public void ShrinkTest()
        {
            UniqueId.currentId = UniqueId.MinId;
            List<Student> source = new List<Student>();
            for (int i = UniqueId.MinId; i < UniqueId.MaxId; i++)
            {
                if (i%2 == 0)
                {
                    source.Add((new Student(i.ToString())));
                }
            }

            bool expected = true;
            bool actual = UniqueId.Shrink(source);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShrinkOutOfRangeTest()
        {
            UniqueId.currentId = UniqueId.MinId;
            List<Student> source = new List<Student>();
            for (int i = UniqueId.MinId; i < UniqueId.MaxId; i++)
            {
                source.Add((new Student(i.ToString())));
            }

            UniqueId.Shrink(source);
        }
    }
}
