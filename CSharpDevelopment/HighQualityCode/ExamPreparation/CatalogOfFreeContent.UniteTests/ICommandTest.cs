using CatalogOfFreeContent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CatalogOfFreeContent.UniteTests
{
    
    
    /// <summary>
    ///This is a test class for ICommandTest and is intended
    ///to contain all ICommandTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ICommandTest
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


        internal virtual ICommand CreateICommand()
        {
            // TODO: Instantiate an appropriate concrete class.
            ICommand target = null;
            return target;
        }

        /// <summary>
        ///A test for ParseCommandType
        ///</summary>
        [TestMethod()]
        public void ParseCommandTypeTest()
        {
            ICommand target = CreateICommand(); // TODO: Initialize to an appropriate value
            string commandName = string.Empty; // TODO: Initialize to an appropriate value
            CommandTypes expected = new CommandTypes(); // TODO: Initialize to an appropriate value
            CommandTypes actual;
            actual = target.ParseCommandType(commandName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ParseName
        ///</summary>
        [TestMethod()]
        public void ParseNameTest()
        {
            ICommand target = CreateICommand(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ParseName();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ParseParameters
        ///</summary>
        [TestMethod()]
        public void ParseParametersTest()
        {
            ICommand target = CreateICommand(); // TODO: Initialize to an appropriate value
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = target.ParseParameters();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            ICommand target = CreateICommand(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OriginalForm
        ///</summary>
        [TestMethod()]
        public void OriginalFormTest()
        {
            ICommand target = CreateICommand(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.OriginalForm = expected;
            actual = target.OriginalForm;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Parameters
        ///</summary>
        [TestMethod()]
        public void ParametersTest()
        {
            ICommand target = CreateICommand(); // TODO: Initialize to an appropriate value
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            target.Parameters = expected;
            actual = target.Parameters;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            ICommand target = CreateICommand(); // TODO: Initialize to an appropriate value
            CommandTypes expected = new CommandTypes(); // TODO: Initialize to an appropriate value
            CommandTypes actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
