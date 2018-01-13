using SudokuWebApp.Model;

namespace SudokuWebApp.Services
{
    public interface IEmptyFieldFactory
    {
        Cell Create(Cell area = null);
    }
}
