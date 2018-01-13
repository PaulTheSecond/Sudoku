namespace SudokuWebApp.Model
{
    public class CellIndex
    {
        public int I { get; set; }
        public int J { get; set; }

        public override string ToString()
        {
            return $"[{I}:{J}]";
        }
    }
}
