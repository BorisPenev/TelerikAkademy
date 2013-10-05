using System.Linq;
using System.Text;
using Phonebook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Phonebook.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for PhonebookRepositoryTest and is intended
    ///to contain all PhonebookRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhonebookRepositoryTest
    {


        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

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

        #region AddPhone

        /// <summary>
        ///A test for AddPhone
        ///</summary>
        [TestMethod()]
        public void AddSinglePhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();
            
            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+359899777235");

            target.AddPhone(name, numbers);
            Assert.AreEqual(1, target.Count);
        }

        [TestMethod()]
        public void AddDuplicatePhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            Assert.AreEqual(1, target.Count);
        }

        [TestMethod()]
        public void AddMultiplePhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("++35945346433");
            target.AddPhone(name, numbers);

            name = "Peshev";
            numbers = new List<string>();
            numbers.Add("++3594545454545");
            target.AddPhone(name, numbers);

            Assert.AreEqual(3, target.Count);
        }

        #endregion

        #region ListPhone

        /// <summary>
        ///A test for ListEntries
        ///</summary>
        [TestMethod()]
        public void ListSingleEntriesTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            Content[] expected = new Content[1];
            expected[0] = new Content()
                {
                    Name = "Kalina",
                    Numbers = new SortedSet<string>()
                        {
                            {"+359899777235"}
                        }
                };

            Content[] actual = target.ListEntries(0, 1);

            var sbExpected = new StringBuilder();
            foreach (var content in expected)
            {
                sbExpected.AppendLine(content.ToString());
            }
            
            var sbActual = new StringBuilder();
            foreach (var content in actual)
            {
                sbActual.AppendLine(content.ToString());
            }

            Assert.AreEqual(sbExpected.ToString(), sbActual.ToString());
        }

        [TestMethod()]
        public void ListDuplicateEntriesTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            Content[] expected = new Content[1];
            expected[0] = new Content()
            {
                Name = "Kalina",
                Numbers = new SortedSet<string>()
                        {
                            {"+35989943242235"},
                            {"+359899777235"}
                        }
            };
            Content[] actual = target.ListEntries(0, 1);

            var sbExpected = new StringBuilder();
            foreach (var content in expected)
            {
                sbExpected.AppendLine(content.ToString());
            }

            var sbActual = new StringBuilder();
            foreach (var content in actual)
            {
                sbActual.AppendLine(content.ToString());
            }

            Assert.AreEqual(sbExpected.ToString(), sbActual.ToString());
        }

        [TestMethod()]
        public void ListMultipleEntriesTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35945346433");
            target.AddPhone(name, numbers);

            name = "Peshev";
            numbers = new List<string>();
            numbers.Add("+3594545454545");
            target.AddPhone(name, numbers);

            Content[] expected = new Content[3];
            expected[0] = new Content()
            {
                Name = "Kalina",
                Numbers = new SortedSet<string>()
                        {
                            {"+35989943242235"},
                            {"+359899777235"}
                        }
            };
            expected[1] = new Content()
            {
                Name = "Peshev",
                Numbers = new SortedSet<string>()
                        {
                            {"+3594545454545"}
                        }
            };
            expected[2] = new Content()
            {
                Name = "Pesho",
                Numbers = new SortedSet<string>()
                        {
                            {"+35945346433"}
                        }
            };
            
            Content[] actual = target.ListEntries(0, 3);

            var sbExpected = new StringBuilder();
            foreach (var content in expected)
            {
                sbExpected.AppendLine(content.ToString());
            }

            var sbActual = new StringBuilder();
            foreach (var content in actual)
            {
                sbActual.AppendLine(content.ToString());
            }

            Assert.AreEqual(sbExpected.ToString(), sbActual.ToString());
        }

        [TestMethod()]
        public void ListMultipleMiddleEntriesTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35945346433");
            target.AddPhone(name, numbers);

            name = "Peshev";
            numbers = new List<string>();
            numbers.Add("+3594545454545");
            target.AddPhone(name, numbers);

            Content[] expected = new Content[2];
            expected[0] = new Content()
            {
                Name = "Peshev",
                Numbers = new SortedSet<string>()
                        {
                            {"+3594545454545"}
                        }
            };
            expected[1] = new Content()
            {
                Name = "Pesho",
                Numbers = new SortedSet<string>()
                        {
                            {"+35945346433"}
                        }
            };
            
            Content[] actual = target.ListEntries(1, 2);

            var sbExpected = new StringBuilder();
            foreach (var content in expected)
            {
                sbExpected.AppendLine(content.ToString());
            }

            var sbActual = new StringBuilder();
            foreach (var content in actual)
            {
                sbActual.AppendLine(content.ToString());
            }

            Assert.AreEqual(sbExpected.ToString(), sbActual.ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void ListMultipleOutOfRangeEntriesTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35945346433");
            target.AddPhone(name, numbers);

            name = "Peshev";
            numbers = new List<string>();
            numbers.Add("+3594545454545");
            target.AddPhone(name, numbers);

            Content[] expected = new Content[3];
            expected[0] = new Content()
                {
                    Name = "Kalina",
                    Numbers = new SortedSet<string>()
                        {
                            {"+35989943242235"},
                            {"+359899777235"}
                        }
                };
            expected[1] = new Content()
                {
                    Name = "Peshev",
                    Numbers = new SortedSet<string>()
                        {
                            {"+3594545454545"}
                        }
                };
            expected[2] = new Content()
                {
                    Name = "Pesho",
                    Numbers = new SortedSet<string>()
                        {
                            {"+35945346433"}
                        }
                };
            Content[] actual = target.ListEntries(0, 5);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ListMultipleOutOfRangeNegativeEntriesTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35945346433");
            target.AddPhone(name, numbers);

            name = "Peshev";
            numbers = new List<string>();
            numbers.Add("+3594545454545");
            target.AddPhone(name, numbers);

            Content[] expected = new Content[3];
            expected[0] = new Content()
            {
                Name = "Kalina",
                Numbers = new SortedSet<string>()
                        {
                            {"+35989943242235"},
                            {"+359899777235"}
                        }
            };
            expected[1] = new Content()
            {
                Name = "Peshev",
                Numbers = new SortedSet<string>()
                        {
                            {"+3594545454545"}
                        }
            };
            expected[2] = new Content()
            {
                Name = "Pesho",
                Numbers = new SortedSet<string>()
                        {
                            {"+35945346433"}
                        }
            };
            Content[] actual = target.ListEntries(-3, 5);
        }

        [TestMethod()]
        public void ListSortEntriesTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            name = "Ana";
            numbers = new List<string>();
            numbers.Add("+35945346433");
            target.AddPhone(name, numbers);

            name = "Peshev";
            numbers = new List<string>();
            numbers.Add("+3594545454545");
            target.AddPhone(name, numbers);

            Content[] expected = new Content[3];
            expected[0] = new Content()
            {
                Name = "Ana",
                Numbers = new SortedSet<string>()
                        {
                            {"+35945346433"}
                        }
            };
            expected[1] = new Content()
            {
                Name = "Kalina",
                Numbers = new SortedSet<string>()
                        {
                            {"+35989943242235"},
                            {"+359899777235"}
                        }
            };
            expected[2] = new Content()
            {
                Name = "Peshev",
                Numbers = new SortedSet<string>()
                        {
                            {"+3594545454545"}
                        }
            };

            Content[] actual = target.ListEntries(0, 3);

            var sbExpected = new StringBuilder();
            foreach (var content in expected)
            {
                sbExpected.AppendLine(content.ToString());
            }

            var sbActual = new StringBuilder();
            foreach (var content in actual)
            {
                sbActual.AppendLine(content.ToString());
            }

            Assert.AreEqual(sbExpected.ToString(), sbActual.ToString());
        }

        #endregion

        #region ChangePhone

        /// <summary>
        ///A test for ChangePhone
        ///</summary>
        [TestMethod()]
        public void ChangeSinglePhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);
            
            var actual = target.ChangePhone("+35989943242235", "+0000000000000");
            Assert.AreEqual(1, actual);
        }

        [TestMethod()]
        public void ChangeNoMatchesPhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Kalina";
            numbers = new List<string>();
            numbers.Add("+359899777235");
            target.AddPhone(name, numbers);

            var actual = target.ChangePhone("+0000000000000", "+11111111111111");
            Assert.AreEqual(0, actual);
        }

        [TestMethod()]
        public void ChangeMultiplePhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            var actual = target.ChangePhone("+35989943242235", "+0000000000000");
            Assert.AreEqual(2, actual);
        }

        [TestMethod()]
        public void ChangeMultipleRotatePhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            var actual = target.ChangePhone("+35989943242235", "+0000000000000");
            actual = target.ChangePhone("+0000000000000", "+35989943242235");
            Assert.AreEqual(2, actual);
        }

        [TestMethod()]
        public void ChangeDoubleMultiplePhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            var actual = target.ChangePhone("+35989943242235", "+0000000000000");
            actual = target.ChangePhone("+35989943242235", "+0000000000000");
            Assert.AreEqual(0, actual);
        }

        [TestMethod()]
        public void ChangeEmptyPhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            var actual = target.ChangePhone("", "+0000000000000");
            Assert.AreEqual(0, actual);
        }

        [TestMethod()]
        public void ChangeNullPhoneTest()
        {
            PhonebookRepository target = new PhonebookRepository();

            var name = "Kalina";
            var numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            name = "Pesho";
            numbers = new List<string>();
            numbers.Add("+35989943242235");
            target.AddPhone(name, numbers);

            var actual = target.ChangePhone("", "+0000000000000");
            Assert.AreEqual(0, actual);
        }

        #endregion

    }
}
