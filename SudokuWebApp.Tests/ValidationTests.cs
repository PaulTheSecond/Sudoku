using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuWebApp.Model;
using SudokuWebApp.Services;

namespace SudokuWebApp.Tests
{
    [TestClass]
    public class ValidationTests
    {
        private IValidateService _validateService;
        private IGenerationFieldService _generationFieldService;

        [TestInitialize]
        public void Initialize()
        {
            _validateService = new ValidateService();
            _generationFieldService = new GenerationFieldService(new EmptyFieldFactory());
        }

        [TestMethod]
        public void CellValidationIsValidTest()
        {
            var validCell = new Cell();
            FillValidArea(validCell);
            
            Assert.IsTrue(validCell.Validate());
        }

        [TestMethod]
        public void CellValidationNotValidTest()
        {
            var invalidCell = new Cell();
            FillValidArea(invalidCell);

            invalidCell[1, 1] = new Cell { Value = 9 };

            Assert.IsFalse(invalidCell.Validate());
        }

        [TestMethod]
        public void FieldValidationValidTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            Assert.IsTrue(_validateService.Execute(field));
        }

        [TestMethod]
        public void FieldValidationNotValidTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            field[1, 1][1, 1].Value = 0;
            Assert.IsFalse(_validateService.Execute(field));
        }

        [TestMethod]
        public void FieldValidationAfterMixAreasRowsTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            field = _generationFieldService.MixAreasRows(3, field);
            Assert.IsTrue(_validateService.Execute(field));
        }

        [TestMethod]
        public void FieldValidationAfterMixAreasColumnsTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            field = _generationFieldService.MixAreasColumns(9, field);
            Assert.IsTrue(_validateService.Execute(field));
        }

        [TestMethod]
        public void FieldValidationAfterMixRowsTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            field = _generationFieldService.MixRows(3, field);
            Assert.IsTrue(_validateService.Execute(field));
        }

        [TestMethod]
        public void FieldValidationAfterMixColumnsTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            field = _generationFieldService.MixColumns(9, field);
            Assert.IsTrue(_validateService.Execute(field));
        }

        [TestMethod]
        public void FieldValidationAfterComplexMixIngTest()
        {
            var field = _generationFieldService.GenerateBaseField();
            field = _generationFieldService.MixColumns(9, field);
            field = _generationFieldService.MixRows(10, field);
            field = _generationFieldService.MixAreasColumns(11, field);
            field = _generationFieldService.MixColumns(12, field);
            field = _generationFieldService.MixColumns(29, field);

            Assert.IsTrue(_validateService.Execute(field));
        }

        private void FillValidArea(Cell src)
        {
            src.SetParent(new Cell());
            var counter = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    src[i, j] = new Cell
                    {
                        Value = counter++
                    };
                }
            }
        }
    }
}
