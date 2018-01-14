using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SudokuWebApp.Services.Generation;
using SudokuWebApp.ViewModel;

namespace SudokuWebApp.Controllers
{
    [Route("api/[controller]")]
    public class FieldController : Controller
    {
        private readonly IGenerationFieldService _generationFieldService;
        public FieldController(IGenerationFieldService generationFieldService)
        {
            _generationFieldService = generationFieldService;
        }


        // GET api/field
        [HttpGet]
        public IActionResult Get()
        {
            var solvedField = _generationFieldService.GenerateBaseField();

            var result = new GameViewModel
            {
                SolvedField = solvedField,
                GameField = solvedField
            };

            return Ok(result);
        }
    }
}
