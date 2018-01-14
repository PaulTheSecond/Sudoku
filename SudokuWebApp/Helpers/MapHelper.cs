using SudokuWebApp.Extensions;
using SudokuWebApp.Model;
using SudokuWebApp.ViewModel;

namespace SudokuWebApp.Helpers
{
    public static class MapHelper
    {
        public static CellViewModel CreateCellViewModel(Cell src)
        {
            var result = new CellViewModel
            {
                Value = src.Value,
                Index = src.Index,
                IsArea = src.IsArea,
                IsField = src.IsField,
                RealIndex = src.IsField ? null : src.IsArea ? src.Index : new CellIndex { I = src.Index.I.GetRealCellIndex(src.Parent.Index.I), J = src.Index.J.GetRealCellIndex(src.Parent.Index.J) }
            };

            if (src.IsField || src.IsArea)
            {
                result.Children.Add(new CellViewModel[3]);
                result.Children.Add(new CellViewModel[3]);
                result.Children.Add(new CellViewModel[3]);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        result.Children[i][j] = CreateCellViewModel(src[i, j]);
                        if (!result.Children[i][j].IsField)
                            result.Children[i][j].Parent = result;
                    }
                }
            }

            return result;
        }

        public static CellViewModel CreateCellViewModel(int[,] src)
        {
            var field = new Cell();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    field[i.GetAreaIndexFromCell(), j.GetAreaIndexFromCell()] = field[i.GetAreaIndexFromCell(), j.GetAreaIndexFromCell()] ?? new Cell();
                    field[i.GetAreaIndexFromCell(), j.GetAreaIndexFromCell()][i.GetCellInAreaIndex(), j.GetCellInAreaIndex()] = new Cell { Value = src[i, j] };
                }
            }
            return CreateCellViewModel(field);
        }

        public static Cell CreateCell(CellViewModel src)
        {
            var result = new Cell
            {
                Value = src.Value,
                Index = src.Index
            };

            

            if (src.IsField || src.IsArea)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        result[i, j] = CreateCell(src.Children[i][j]);
                    }
                }
            }

            return result;
        }
    }
}
