using SudokuWebApp.Model;

namespace SudokuWebApp.ViewModel
{
    public class GameViewModel
    {
        public CellViewModel SolvedField { get; set; }
        public CellViewModel GameField { get; set; }
    }
}
