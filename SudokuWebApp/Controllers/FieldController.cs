using Microsoft.AspNetCore.Mvc;
using SudokuWebApp.Helpers;
using SudokuWebApp.Services.Generation;
using SudokuWebApp.ViewModel;

namespace SudokuWebApp.Controllers
{
    [Route("api/[controller]")]
    public class FieldController : Controller
    {
        private readonly IGenerationFieldService _generationFieldService;
        private readonly IEmptyFieldFactory _emptyFieldFactory;
        public FieldController(IGenerationFieldService generationFieldService,
                IEmptyFieldFactory emptyFieldFactory)
        {
            _generationFieldService = generationFieldService;
            _emptyFieldFactory = emptyFieldFactory;
        }


        // GET api/field
        [HttpGet]
        public IActionResult Get()
        {
            var solvedField = _emptyFieldFactory.Create();// _generationFieldService.GenerateBaseField();

            var result = new GameViewModel
            {
                SolvedField = MapHelper.CreateCellViewModel(solvedField),
                GameField = MapHelper.CreateCellViewModel(solvedField)
            };

            return Ok(result);
        }
    }
}
