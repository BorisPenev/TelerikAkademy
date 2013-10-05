using School;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace School.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CourseTest and is intended
    ///to contain all CourseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CourseTest
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
        ///A test for Course Constructor
        ///</summary>
        [TestMethod()]
        public void CourseConstructorTest()
        {
            string name = "CSharp"; 
            Course target = new Course(name);
            Assert.IsNotNull(target);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CourseConstructorEmptyTest()
        {
            string name = string.Empty;
            Course target = new Course(name);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CourseConstructorNullTest()
        {
            string name = null;
            Course target = new Course(name);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            string name = "JavaScript";
            Course target = new Course(name);
            string actual = target.Name;
            Assert.AreEqual(name, actual);
        }
    }
}
