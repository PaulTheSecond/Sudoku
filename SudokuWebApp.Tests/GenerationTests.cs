using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuWebApp.Services;

namespace SudokuWebApp.Tests
{
    [TestClass]
    public class GenerationTests
    {
        private IGenerationFieldService _generationGieldService;
        private IEmptyFieldFactory _emptyFieldFactory;

        [TestInitialize]
        public void Initialize()
        {
            _emptyFieldFactory = new EmptyFieldFactory();
            _generationGieldService = new GenerationFieldService(_emptyFieldFactory);
        }
        
        [TestMethod]
        public void CreateEmptyFieldTest()
        {
            var field = _emptyFieldFactory.Create();

            Assert.IsNotNull(field[0, 0]?[1, 1]?.Value);
            Assert.AreEqual(0, field[0, 0][0, 0].Value);
            Assert.AreEqual(0, field[1, 1][1, 1].Value);
            Assert.AreEqual(0, field[2, 2][2, 2].Value);
        }

        [TestMethod]
        public void GenerateBaseFieldTest()
        {
            var field = _generationGieldService.GenerateBaseField();
            
            Assert.IsNotNull(field[0, 0]?[1, 1]?.Value);

            Assert.AreEqual(1, field[0, 0][0, 0].Value);
            Assert.AreEqual(9, field[1, 1][1, 1].Value);
            Assert.AreEqual(8, field[2, 2][2, 2].Value);
        }

        [TestMethod]
        public void AreTransposeValidTest()
        {
            var field = _generationGieldService.GenerateBaseField();
            field.Transpose();

            Assert.IsNotNull(field[0, 0]?[1, 1]?.Value);
            Assert.AreEqual(1, field[0, 0][0, 0].Value);
            Assert.AreEqual(9, field[1, 1][1, 1].Value);
            Assert.AreEqual(9, field[2, 0][2, 0].Value);
        }
    }
}
