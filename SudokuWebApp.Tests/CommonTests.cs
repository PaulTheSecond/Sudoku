using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuWebApp.Extensions;
using SudokuWebApp.Helpers;
using SudokuWebApp.Services.Generation;

namespace SudokuWebApp.Tests
{
    [TestClass]
    public class CommonTests
    {
        private int[] baseArray;
        private IGenerationFieldService _generationFieldService;

        [TestInitialize]
        public void Initialize()
        {
            baseArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            _generationFieldService = new GenerationFieldService(new EmptyFieldFactory());
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

        [TestMethod]
        public void MapHelperCreateViewModelTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            var vm = MapHelper.CreateCellViewModel(field);


            Assert.IsNotNull(vm.Children[0][0].Children[0][0].Value);
            Assert.AreEqual(field[1, 1][1, 1].Value, vm.Children[1][1].Children[1][1].Value);
        }

        [TestMethod]
        public void MapHelperCreateCellTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            var vm = MapHelper.CreateCellViewModel(field);
            var mappedField = MapHelper.CreateCell(vm);

            Assert.IsNotNull(mappedField[0,0][0,0].Value);
            Assert.AreEqual(field[1, 1][1, 1].Value, mappedField[1, 1][1, 1].Value);
        }
    }
}
