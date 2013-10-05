using WalkInMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WalkInMatrix.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for MatrixTest and is intended
    ///to contain all MatrixTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MatrixTest
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
        ///A test for Matrix Constructor
        ///</summary>
        [TestMethod()]
        public void MatrixConstructorTest()
        {
            int[,] gameMatrix = new int[5,5]; // TODO: Initialize to an appropriate value
            Matrix target = new Matrix(gameMatrix);
            Assert.IsNotNull(target.GameMatrix);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Matrix target = new Matrix(new int[3, 3]);
            string expected = "  0  0  0" + Environment.NewLine +
                            "  0  0  0" + Environment.NewLine +
                            "  0  0  0" + Environment.NewLine;
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DirectionType
        ///</summary>
        [TestMethod()]
        public void DirectionTypeTest()
        {
            Matrix target = new Matrix(new int[3, 3]);
            DirectionTypes expected = DirectionTypes.Northwest;
            DirectionTypes actual;
            target.DirectionType = expected;
            actual = target.DirectionType;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MaxValue
        ///</summary>
        [TestMethod()]
        public void MaxValueTest()
        {
            Matrix target = new Matrix(new int[3, 3]);
            int expected = 8; 
            int actual;
            target.MaxValue = expected;
            actual = target.MaxValue;
            Assert.AreEqual(expected, actual);
        }
    }
}
