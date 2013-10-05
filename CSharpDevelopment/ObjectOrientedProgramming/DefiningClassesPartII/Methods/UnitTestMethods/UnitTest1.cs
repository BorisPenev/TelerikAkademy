using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Methods;

namespace UnitTestMethods
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCountNumberInArray()
        {
            Assert.AreEqual(3, Program.CountNumberInArray(new int[] { 3, 2, 3, 4, 2, 2, 4 }, 2));
        }
    }
}
