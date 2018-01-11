using System;

namespace SudokuWebApp.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] ShiftRight<T>(this T[] src, int shiftingCount = 1)
        {
            if(shiftingCount > src.Length)
            {
                throw new IndexOutOfRangeException();
            }
            var res = new T[src.Length];
            for (int i = shiftingCount; i < src.Length; i++)
            {
                res[i] = src[i - shiftingCount];
            }

            for (int i = 0; i < shiftingCount; i++)
            {
                res[i] = src[res.Length - (shiftingCount - i)];
            }

            return res;
        }

        public static T[] ShiftLeft<T>(this T[] src, int shiftingCount = 1)
        {
            if (shiftingCount > src.Length)
            {
                throw new IndexOutOfRangeException();
            }
            var res = new T[src.Length];
            for (int i = 0; i < src.Length - shiftingCount; i++)
            {
                res[i] = src[i + shiftingCount];
            }

            for (int i = 0; i < shiftingCount; i++)
            {
                res[res.Length - (shiftingCount - i)] = src[i];
            }

            return res;
        }
    }
}
