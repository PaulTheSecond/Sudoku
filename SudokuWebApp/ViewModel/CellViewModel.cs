using SudokuWebApp.Model;
using System.Collections.Generic;

namespace SudokuWebApp.ViewModel
{
    public class CellViewModel
    {
        public CellViewModel()
        {
            Children = new List<CellViewModel[]>(3)
            {
                new CellViewModel[3],
                new CellViewModel[3],
                new CellViewModel[3]
            };
        }

        public int? Value { get; set; }

        public CellViewModel Parent { get; set; }

        public CellIndex Index { get; set; }

        public CellIndex RealIndex { get; set; }

        public bool IsField { get; set; }

        public bool IsArea { get; set; }

        public List<CellViewModel[]> Children { get; set; }        
    }
}
