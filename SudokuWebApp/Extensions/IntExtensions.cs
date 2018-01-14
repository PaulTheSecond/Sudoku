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
        public static int GetRealCellIndex(this int cellIndex, int areaIndex)
        {
            switch (areaIndex)
            {
                case 1:
                    switch (cellIndex)
                    {
                        case 1:
                            return 4;
                        case 2:
                            return 5;
                        case 0:
                        default:
                            return 3;
                    }
                case 2:
                    switch (cellIndex)
                    {
                        case 1:
                            return 7;
                        case 2:
                            return 8;
                        case 0:
                        default:
                            return 6;
                    }
                case 0:
                default:
                    switch (cellIndex)
                    {
                        case 1:
                            return 1;
                        case 2:
                            return 2;
                        case 0:
                        default:
                            return 0;
                    }
            }
        }
    }
}
