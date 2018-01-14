using SudokuWebApp.Model;

namespace SudokuWebApp.ViewModel
{
    public class GameViewModel
    {
        public Cell SolvedField { get; set; }
        public Cell GameField { get; set; }
    }
}
