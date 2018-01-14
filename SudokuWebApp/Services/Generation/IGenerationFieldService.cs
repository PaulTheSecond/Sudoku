using SudokuWebApp.Model;

namespace SudokuWebApp.Services.Generation
{
    public interface IGenerationFieldService
    {
        Cell GenerateBaseField();
        Cell MixAreasRows(int iterations = 1, Cell src = null);
        Cell MixAreasColumns(int iterations = 1, Cell src = null);
        Cell MixRows(int iterations = 1, Cell src = null);
        Cell MixColumns(int iterations = 1, Cell src = null);
    }
}
