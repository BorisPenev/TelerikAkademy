using System.Collections.Generic;
using System.IO;
using Phonebook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phonebook.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for PhonebookProgramTest and is intended
    ///to contain all PhonebookProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhonebookProgramTest
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
        
        private string filePath = "../../../Phonebook.UnitTests/Test/";

        private string fileExtension = ".txt";

        private string fileInput = ".in";
        private string fileOutput = ".out";
        private string filePrefix = "test.";

        [TestMethod(), Timeout(2000)]
        public void MainTest000001()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "000.001" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }
            
            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "000.001" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest001()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "001" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "001" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest002()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "002" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "002" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest003()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "003" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "003" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest004()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "004" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "004" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest005()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "005" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "005" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest006()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "006" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "006" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest007()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "007" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "007" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest008()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "008" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "008" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest009()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "009" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "009" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod(), Timeout(2000)]
        public void MainTest010()
        {
            var expected = string.Empty;
            using (var reader = new StreamReader(filePath + filePrefix + "010" + fileOutput + fileExtension))
            {
                expected = reader.ReadToEnd();
            }

            var output = new StringWriter();
            using (output)
            {
                var input = new StreamReader(filePath + filePrefix + "010" + fileInput + fileExtension);
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    PhonebookProgram.Main();

                    var actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
