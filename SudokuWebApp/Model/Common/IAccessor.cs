namespace SudokuWebApp.Model.Common
{
    interface IAccessor<T>
    {
        T this[int i, int j] { get; set; }
    }
}
