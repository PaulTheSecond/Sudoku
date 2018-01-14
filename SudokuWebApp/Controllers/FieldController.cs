using Microsoft.AspNetCore.Mvc;
using SudokuWebApp.Helpers;
using SudokuWebApp.Services.Generation;
using SudokuWebApp.ViewModel;
using System.IO;
using Newtonsoft.Json;
using SudokuWebApp.Services.Validation;

namespace SudokuWebApp.Controllers
{
    [Route("api/[controller]")]
    public class FieldController : Controller
    {
        private readonly IGenerationFieldService _generationFieldService;
        private readonly IEmptyFieldFactory _emptyFieldFactory;
        private readonly IValidateService _validateService;

        private static DefaultSchemaViewModel[] _defaultSchemas;

        public FieldController(IGenerationFieldService generationFieldService,
                IEmptyFieldFactory emptyFieldFactory,
                IValidateService validateService)
        {
            _generationFieldService = generationFieldService;
            _emptyFieldFactory = emptyFieldFactory;
            _validateService = validateService;
            if (_defaultSchemas == null)
                LoadDefaultScemas();
        }


        // GET api/field
        [HttpGet]
        public IActionResult Get([FromQuery] int? defaultIndex = null, [FromQuery] bool generateField = false)
        {
            var result = default(GameViewModel);
            if (generateField)
            {
                var gameField = _emptyFieldFactory.Create();
                var solvedField = _generationFieldService.GenerateBaseField();

                result = new GameViewModel
                {
                    SolvedField = MapHelper.CreateCellViewModel(solvedField),
                    GameField = MapHelper.CreateCellViewModel(gameField)
                };
            }
            else
            {
                defaultIndex = defaultIndex.HasValue ? ++defaultIndex : 0;
                if (defaultIndex > _defaultSchemas.Length - 1)
                {
                    defaultIndex = 0;
                }

                result = new GameViewModel
                {
                    SolvedField = MapHelper.CreateCellViewModel(_defaultSchemas[defaultIndex.Value].SolvedField),
                    GameField = MapHelper.CreateCellViewModel(_defaultSchemas[defaultIndex.Value].GameField),
                    DefaultSchemaIndex = defaultIndex
                };
            }

            return Ok(result);
        }

        // post api/field/check
        [HttpPost("check")]
        public IActionResult Check([FromBody] CellViewModel gameField)
        {
            var field = MapHelper.CreateCell(gameField);
            var res = _validateService.Execute(field);
            if (res)
                return Ok();
            return BadRequest();
        }

        private void LoadDefaultScemas()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "api", "defaultSchemas.json");
            var fd = System.IO.File.ReadAllText(path);
            _defaultSchemas = JsonConvert.DeserializeObject<DefaultSchemaViewModel[]>(fd);
        }
    }
}
