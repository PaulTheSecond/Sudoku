using SudokuWebApp.Model.Common;

namespace SudokuWebApp.Model
{
    public class Cell
    {
        private const int CheckSUm = 45;

        private int? _value;
        private Cell[,] _children;
        private IAccessor<Cell> _accessor;
        
        public int? Value {
            get => _value;
            set => _value = value;
        }

        public int Size()
        {
            return _children?.GetLength(0) ?? 0;
        }

        public Cell Parent { get; private set; }

        public CellIndex Index { get; set; }

        public bool IsField => Parent == null;

        public bool IsArea => !IsField && !Value.HasValue;

        public Cell this[int i, int j]
        {
            get { if (_children == null)
                    return null;
                return _accessor[i, j];
            }
            set {
                if (_children == null)
                {
                    _children = new Cell[3, 3];
                    _accessor = new DirectAccessor<Cell>(_children);
                }

                value.SetParent(this);
                value.Index = new CellIndex { I = i, J = j };

                _accessor[i, j] = value;
            }
        }
        
        public bool Validate()
        {
            //continue validate in low layer
            if (IsField && !IsArea)
                return true;
            //check value
            if (Value.HasValue)
            {
                if (Value <= 0 || Value > 9)
                    return false;
                return true;
            }
            //if this is area
            var sum = 0;
            for (int i = 0; i < Size(); i++)
            {
                for (int j = 0; j < Size(); j++)
                {
                    sum += this[i, j]?.Value ?? 0;
                }
            }
            return sum == CheckSUm;
        }

        public void Transpose()
        {            
            if(IsField || IsArea)
            {
                for (int i = 0; i < Size(); i++)
                {
                    for (int j = 0; j < Size(); j++)
                    {
                        this[i, j]?.Transpose();
                    }
                }
            }

            if (_accessor is DirectAccessor<Cell>)
                _accessor = new TransposedAccessor<Cell>(_children);
            else
                _accessor = new DirectAccessor<Cell>(_children);
        }

        public void SetParent(Cell parent)
        {
            this.Parent = parent;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? (Parent != null ? $"Area {Index}" : "This is game field");
        }
    }    
}
