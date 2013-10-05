using School;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace School.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for StudentTest and is intended
    ///to contain all StudentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StudentTest
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
        ///A test for Student Constructor
        ///</summary>
        [TestMethod()]
        public void StudentConstructorTest()
        {
            string name = "Anonymous";
            Student target = new Student(name);
            Assert.IsNotNull(target);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StudentConstructorEmptyTest()
        {
            string name = string.Empty;
            Student target = new Student(name);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StudentConstructorNullTest()
        {
            string name = string.Empty;
            Student target = new Student(name);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            string name = "Anon";
            Student target = new Student(name);
            string actual = target.Name;
            Assert.AreEqual(name, actual);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IdTest()
        {
            string name = "Anon2";
            Student target = new Student(name);
            target.Id = 2;
        }
    }
}
