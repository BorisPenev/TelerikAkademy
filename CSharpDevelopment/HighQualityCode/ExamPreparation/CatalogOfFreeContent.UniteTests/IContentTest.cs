using CatalogOfFreeContent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CatalogOfFreeContent.UniteTests
{
    
    
    /// <summary>
    ///This is a test class for IContentTest and is intended
    ///to contain all IContentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IContentTest
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


        internal virtual IContent CreateIContent()
        {
            // TODO: Instantiate an appropriate concrete class.
            IContent target = null;
            return target;
        }

        /// <summary>
        ///A test for Author
        ///</summary>
        [TestMethod()]
        public void AuthorTest()
        {
            IContent target = CreateIContent(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Author = expected;
            actual = target.Author;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Size
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            IContent target = CreateIContent(); // TODO: Initialize to an appropriate value
            long expected = 0; // TODO: Initialize to an appropriate value
            long actual;
            target.Size = expected;
            actual = target.Size;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TextRepresentation
        ///</summary>
        [TestMethod()]
        public void TextRepresentationTest()
        {
            IContent target = CreateIContent(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.TextRepresentation = expected;
            actual = target.TextRepresentation;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [TestMethod()]
        public void TitleTest()
        {
            IContent target = CreateIContent(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Types
        ///</summary>
        [TestMethod()]
        public void TypesTest()
        {
            IContent target = CreateIContent(); // TODO: Initialize to an appropriate value
            ContentTypes expected = new ContentTypes(); // TODO: Initialize to an appropriate value
            ContentTypes actual;
            target.Types = expected;
            actual = target.Types;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Url
        ///</summary>
        [TestMethod()]
        public void UrlTest()
        {
            IContent target = CreateIContent(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Url = expected;
            actual = target.Url;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
