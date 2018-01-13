using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SudokuWebApp.Model;

namespace SudokuWebApp.Services
{
    public class EmptyFieldFactory : IEmptyFieldFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Cell Create(Cell area = null)
        {
            var result = area ?? new Cell();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var c = new Cell();
                    if (result.Parent == null)
                    {
                        c.SetParent(result);
                    }
                    else
                    {
                        c.Value = 0;
                    }
                    result[i, j] = c.Value.HasValue ? c : Create(c);
                }
            }
            return result;
        }
    }
}
