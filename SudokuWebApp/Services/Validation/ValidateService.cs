using SudokuWebApp.Extensions;
using SudokuWebApp.Model;

namespace SudokuWebApp.Services.Validation
{

    public class ValidateService : IValidateService
    {
        public bool Execute(Cell gameField)
        {
            if(!ValidateAreas(gameField))
                return false;
            if (!ValidateRows(gameField))
                return false;
            if (!ValidateColumns(gameField))
                return false;
            return true;
        }

        private bool ValidateAreas(Cell gameField)
        {
            if (!gameField.Validate())
                return false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(!gameField[i, j].Validate())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ValidateRows(Cell gameField)
        {
            var r = 0;
            for (int i = 0; i < 9; i++)
            {
                if (i > 0 && i % 3 == 0)
                    r++;
                var cell = MakeCellFromRow(new[] { gameField[r, 0], gameField[r, 1], gameField[r, 2] }, i.GetCellInAreaIndex());
                if (!cell.Validate())
                    return false;
            }
            return true;
        }

        private bool ValidateColumns(Cell gameField)
        {
            gameField.Transpose();
            var res = ValidateRows(gameField);
            gameField.Transpose();
            return res;
        }

        private Cell MakeCellFromRow(Cell[] row, int index)
        {
            var res = new Cell();
            res.SetParent(new Cell());
            var resRow = 0;
            foreach (var item in row)
            {
                for (int c = 0; c < 3; c++)
                {
                    res[resRow, c] = item[index, c];
                }
                resRow++;
            }
            return res;
        }
    }
}
