using WalkInMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WalkInMatrix.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for WalkExtensionsTest and is intended
    ///to contain all WalkExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WalkExtensionsTest
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
        ///A test for IsOutOfBorders
        ///</summary>
        [TestMethod()]
        public void IsOutOfBordersTest()
        {
            Matrix matrix = new Matrix(new int[5, 5]);
            matrix.CurrentPosition.Row = 0;
            matrix.CurrentPosition.Col = 0;
            matrix.Direction.Row = -1;
            matrix.Direction.Col = -1;
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = WalkExtensions.IsOutOfBorders(matrix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotOutOfBordersTest()
        {
            Matrix matrix = new Matrix(new int[5, 5]);
            matrix.CurrentPosition.Row = 0;
            matrix.CurrentPosition.Col = 0;
            matrix.Direction.Row = 1;
            matrix.Direction.Col = 1;
            bool expected = false;
            bool actual;
            actual = WalkExtensions.IsOutOfBorders(matrix);
            Assert.AreEqual(expected, actual);
        }

        

        /// <summary>
        ///A test for IsEmptyField
        ///</summary>
        [TestMethod()]
        public void IsEmptyFieldTest()
        {
            Matrix matrix = new Matrix(new int[5, 5]);
            matrix.CurrentPosition.Row = 0;
            matrix.CurrentPosition.Col = 0;
            bool expected = true;
            bool actual;
            actual = WalkExtensions.IsEmptyField(matrix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNotEmptyFieldTest()
        {
            Matrix matrix = new Matrix(new int[5, 5]);
            matrix.GameMatrix[1, 1] = 1;
            matrix.CurrentPosition.Row = 0;
            matrix.CurrentPosition.Col = 0;
            bool expected = false;
            bool actual;
            actual = WalkExtensions.IsEmptyField(matrix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MoveTest()
        {
            Matrix matrix = new Matrix(new int[5, 5]);
            matrix.CurrentPosition.Row = 0;
            matrix.CurrentPosition.Col = 0;
            matrix.Direction.Row = 1;
            matrix.Direction.Col = 1;
            int expected = 1;
            WalkExtensions.Move(matrix);
            int actual;
            actual = matrix.GameMatrix[matrix.CurrentPosition.Row, matrix.CurrentPosition.Col]; 
            Assert.AreEqual(expected, actual);
        }
    }
}