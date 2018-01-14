using SudokuWebApp.Model;

namespace SudokuWebApp.Services.Generation
{
    public interface IEmptyFieldFactory
    {
        Cell Create(Cell area = null);
    }
}
