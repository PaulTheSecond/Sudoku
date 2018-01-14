using SudokuWebApp.Model;

namespace SudokuWebApp.Services.Validation
{
    public interface IValidateService
    {
        bool Execute(Cell gameField);
    }
}
