using SudokuWebApp.Model;

namespace SudokuWebApp.Services
{
    public interface IValidateService
    {
        bool Execute(Cell gameField);
    }
}
