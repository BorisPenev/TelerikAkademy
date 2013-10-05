using CatalogOfFreeContent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace CatalogOfFreeContent.UniteTests
{
    
    
    /// <summary>
    ///This is a test class for ICommandExecutorTest and is intended
    ///to contain all ICommandExecutorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ICommandExecutorTest
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


        internal virtual ICommandExecutor CreateICommandExecutor()
        {
            // TODO: Instantiate an appropriate concrete class.
            ICommandExecutor target = null;
            return target;
        }

        /// <summary>
        ///A test for ExecuteCommand
        ///</summary>
        [TestMethod()]
        public void ExecuteCommandTest()
        {
            ICommandExecutor target = CreateICommandExecutor(); // TODO: Initialize to an appropriate value
            ICatalog catalog = null; // TODO: Initialize to an appropriate value
            ICommand command = null; // TODO: Initialize to an appropriate value
            StringBuilder output = null; // TODO: Initialize to an appropriate value
            target.ExecuteCommand(catalog, command, output);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
