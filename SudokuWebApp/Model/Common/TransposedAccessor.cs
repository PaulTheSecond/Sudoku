namespace SudokuWebApp.Model.Common
{
    class TransposedAccessor<T> : IAccessor<T>
    {
        public TransposedAccessor(T[,] payload)
        {
            m_Payload = payload;
        }

        public T this[int i, int j]
        {
            get { return m_Payload[j, i]; }
            set { m_Payload[j, i] = value; }
        }

        T[,] m_Payload;
    }
}
