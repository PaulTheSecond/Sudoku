using System;
using SudokuWebApp.Extensions;
using SudokuWebApp.Model;

namespace SudokuWebApp.Services.Generation
{
    public class GenerationFieldService : IGenerationFieldService
    {
        private readonly int[] baseArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private readonly IEmptyFieldFactory _emptyFieldFactory;

        public GenerationFieldService(IEmptyFieldFactory emptyFieldFactory)
        {
            _emptyFieldFactory = emptyFieldFactory;
        }

        public Cell GenerateBaseField()
        {
            var field = _emptyFieldFactory.Create();
            var arr = baseArray;

            var baseIndex = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int ci = 0; ci < 3; ci++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        field[i, j] = field[i, j] ?? new Cell();

                        for (int c = 0; c < 3; c++)
                        {
                            field[i, j][ci, c] = new Cell
                            {
                                Value = arr[baseIndex++]
                            };
                        }
                    }
                    arr = arr.ShiftLeft(3);
                    baseIndex = 0;
                }
                arr = arr.ShiftLeft();
                baseIndex = 0;
            }

            return field;
        }

        #region MixAreas

        public Cell MixAreasRows(int iterations = 1, Cell src = null)
        {
            var field = src ?? GenerateBaseField();
            var rnd = new Random();
            for (int iteration = 0; iteration < iterations; iteration++)
            {
                var firstInd = rnd.Next(0, 2);
                int secondInd = 0;
                do
                {
                    secondInd = rnd.Next(0, 2);
                }
                while (secondInd == firstInd);

                for (int j = 0; j < 3; j++)
                {
                    var tmp = field[secondInd, j];
                    field[secondInd, j] = field[firstInd, j];
                    field[firstInd, j] = tmp;
                }
            }
            return field;
        }

        public Cell MixAreasColumns(int iterations = 1, Cell src = null)
        {
            var field = src ?? GenerateBaseField();
            var rnd = new Random();
            field.Transpose();
            field = MixAreasRows(iterations, field);
            field.Transpose();
            return field;
        }

        #endregion
        #region MixRowsColumns

        public Cell MixRows(int iterations = 1, Cell src = null)
        {
            var field = src ?? GenerateBaseField();
            var rnd = new Random();

            for (int ineration = 0; ineration < iterations; ineration++)
            {
                int areaRowIndex = rnd.Next(0, 2);
                int firstInd = rnd.Next(0, 2);
                int secondInd = 0;
                do
                {
                    secondInd = rnd.Next(0, 2);
                }
                while (secondInd == firstInd);
                for (int j = 0; j < 3; j++)
                {
                    for (int cj = 0; cj < 3; cj++)
                    {
                        var tmp = field[areaRowIndex, j][secondInd, cj];
                        field[areaRowIndex, j][secondInd, cj] = field[areaRowIndex, j][firstInd, cj];
                        field[areaRowIndex, j][firstInd, cj] = tmp;
                    }
                }
            }

            return field;
        }

        public Cell MixColumns(int iterations = 1, Cell src = null)
        {
            var field = src ?? GenerateBaseField();
            var rnd = new Random();
            field.Transpose();
            field = MixRows(iterations, field);
            field.Transpose();
            return field;
        }

        #endregion
    }
}
