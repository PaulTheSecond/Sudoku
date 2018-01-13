using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuWebApp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuWebApp.Tests
{
    [TestClass]
    public class CommonTests
    {
        private int[] baseArray;

        [TestInitialize]
        public void Initialize()
        {
            baseArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        [TestMethod]
        public void ArrayLeftShiftTest()
        {
            var res = baseArray.ShiftLeft();
            Assert.AreEqual(1, res[8]);
        }

        [TestMethod]
        public void ArrayLeftShiftBy3Test()
        {
            var res = baseArray.ShiftLeft(3);
            Assert.AreEqual(3, res[8]);
        }

        [TestMethod]
        public void ArrayRightShiftTest()
        {
            var res = baseArray.ShiftRight();
            Assert.AreEqual(8, res[8]);
        }

        [TestMethod]
        public void ArrayRightShiftBy3Test()
        {
            var res = baseArray.ShiftRight(3);
            Assert.AreEqual(6, res[8]);
        }
    }
}
