namespace SudokuWebApp.Extensions
{
    public static class IntExtensions
    {
        public static int GetAreaIndexFromCell(this int cellIndex)
        {
            switch (cellIndex)
            {
                case 0:
                case 1:
                case 2:
                    return 0;
                case 3:
                case 4:
                case 5:
                    return 1;
                case 6:
                case 7:
                case 8:
                    return 2;
                default:
                    return 0;
            }
        }

        public static int GetCellInAreaIndex(this int cellIndex)
        {
            switch (cellIndex)
            {
                case 0:
                case 3:
                case 6:
                    return 0;
                case 1:
                case 4:
                case 7:
                    return 1;
                case 2:
                case 5:
                case 8:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
