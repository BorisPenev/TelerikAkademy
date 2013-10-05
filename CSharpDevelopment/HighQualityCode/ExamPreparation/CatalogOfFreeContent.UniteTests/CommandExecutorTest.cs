using CatalogOfFreeContent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace CatalogOfFreeContent.UniteTests
{
    
    
    /// <summary>
    ///This is a test class for CommandExecutorTest and is intended
    ///to contain all CommandExecutorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CommandExecutorTest
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
        ///A test for CommandExecutor Constructor
        ///</summary>
        [TestMethod()]
        public void CommandExecutorConstructorTest()
        {
            var target = new CommandExecutor();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ExecuteCommand
        ///</summary>
        [TestMethod()]
        public void ExecuteAddBookCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var command = new Command("Add book: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");
            var output = new StringBuilder();
            var expected = "Book added" + Environment.NewLine;
            target.ExecuteCommand(catalog, command, output);
            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod()]
        public void ExecuteAddMovieCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var command = new Command("Add movie: The Secret; Drew Heriot, Sean Byrne & others (2006); 832763834; http://t.co/dNV4d");
            var output = new StringBuilder();
            var expected = "Movie added" + Environment.NewLine;
            target.ExecuteCommand(catalog, command, output);
            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod()]
        public void ExecuteAddSongCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var command = new Command("Add song: One; Metallica; 8771120; http://goo.gl/dIkth7gs");
            var output = new StringBuilder();
            var expected = "Song added" + Environment.NewLine;
            target.ExecuteCommand(catalog, command, output);
            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod()]
        public void ExecuteAddApplicationCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var command = new Command("Add application: Firefox v.11.0; Mozilla; 16148072; http://www.mozilla.org");
            var output = new StringBuilder();
            var expected = "Application added" + Environment.NewLine;
            target.ExecuteCommand(catalog, command, output);
            Assert.AreEqual(expected, output.ToString());
        }
        
        [TestMethod()]
        public void ExecuteUpdateCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var output = new StringBuilder();
            var expected = new StringBuilder();

            var command = new Command("Add book: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book added");

            command = new Command("Add book: Intro C# 2; S.Nakov; 12763892; http://www.introprogramming.bg");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book added");

            command = new Command("Update: http://www.introprogramming.info; http://introprograming.info/en/");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("1 items updated");
            
            Assert.AreEqual(expected.ToString(), output.ToString());
        }

        [TestMethod()]
        public void ExecuteUpdateCommandFalseTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var output = new StringBuilder();
            var expected = new StringBuilder();

            var command = new Command("Add book: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book added");

            command = new Command("Add book: Intro C# 2; S.Nakov; 12763892; http://www.introprogramming.bg");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book added");

            command = new Command("Update: http://www.introprogramming.com; http://introprograming.info/en/");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("0 items updated");

            Assert.AreEqual(expected.ToString(), output.ToString());
        }

        [TestMethod()]
        public void ExecuteFindCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var output = new StringBuilder();
            var expected = new StringBuilder();

            var command = new Command("Add book: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book added");

            command = new Command("Find: Intro C#; 1");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");

            Assert.AreEqual(expected.ToString(), output.ToString());
        }

        [TestMethod()]
        public void ExecuteFindManyCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var output = new StringBuilder();
            var expected = new StringBuilder();

            var command = new Command("Add book: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book added");

            command = new Command("Add book: Intro C#; S.Nakovvvvvvv; 3453454535433224; http://www.introprogramming.bg");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book added");

            command = new Command("Add movie: Movie C#; Sssssssssssss.Nakov; 5435353333; http://www.introprogramming.info");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Movie added");

            command = new Command("Find: Intro C#; 1");
            target.ExecuteCommand(catalog, command, output);
            expected.AppendLine("Book: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");

            Assert.AreEqual(expected.ToString(), output.ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ExecuteDefaultCommandTest()
        {
            var target = new CommandExecutor();
            var catalog = new Catalog();
            var command = new Command("Add car: Intro C#; S.Nakov; 12763892; http://www.introprogramming.info");
            var output = new StringBuilder();
            target.ExecuteCommand(catalog, command, output);
        }
    }
}
