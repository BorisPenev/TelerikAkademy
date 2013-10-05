using CatalogOfFreeContent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CatalogOfFreeContent.UniteTests
{
    
    
    /// <summary>
    ///This is a test class for ICatalogTest and is intended
    ///to contain all ICatalogTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ICatalogTest
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


        internal virtual ICatalog CreateICatalog()
        {
            // TODO: Instantiate an appropriate concrete class.
            ICatalog target = null;
            return target;
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            var target = new Catalog();
            var content = new Content(ContentTypes.Book, "Intro C#; S.Nakov; 12763892; http://www.introprogramming.info".Split(';'));
            target.Add(content);
            Assert.AreEqual(1, target.Count);
        }

        /// <summary>
        ///A test for GetContentByTitle
        ///</summary>
        [TestMethod()]
        public void GetContentByTitleTest()
        {
            ICatalog target = CreateICatalog(); // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            int maxResult = 0; // TODO: Initialize to an appropriate value
            IEnumerable<IContent> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<IContent> actual;
            actual = target.GetContentByTitle(title, maxResult);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateUrl
        ///</summary>
        [TestMethod()]
        public void UpdateUrlTest()
        {
            ICatalog target = CreateICatalog(); // TODO: Initialize to an appropriate value
            string oldUrl = string.Empty; // TODO: Initialize to an appropriate value
            string newUrl = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdateUrl(oldUrl, newUrl);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
