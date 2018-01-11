namespace SudokuWebApp.Model.Common
{
    class DirectAccessor<T> : IAccessor<T>
    {
        public DirectAccessor(T[,] payload)
        {
            m_Payload = payload;
        }

        public T this[int i, int j]
        {
            get { return m_Payload[i, j]; }
            set { m_Payload[i, j] = value; }
        }

        T[,] m_Payload;
    }
}
