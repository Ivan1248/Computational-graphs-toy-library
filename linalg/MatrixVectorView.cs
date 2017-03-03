using System;

namespace LinAlg
{
    internal class MatrixVectorView : Matrix
    {
        private readonly Vector _original;
        private readonly Func<int, int, int> _convertIndex;

        internal MatrixVectorView(Vector original, bool asRowMatrix)
        {
            if (!asRowMatrix)
            {
                RowCount = original.Dimension;
                ColCount = 1;
                _convertIndex = delegate (int i, int j)
                    {
                        if (j == 0) return i;
                        else throw new ArgumentOutOfRangeException();
                    };
            }
            else
            {
                RowCount = 1;
                ColCount = original.Dimension;
                _convertIndex = delegate (int i, int j)
                    {
                        if (i == 0) return j;
                        else throw new ArgumentOutOfRangeException();
                    };
            }
            _original = original;
        }

        public override int RowCount { get; }

        public override int ColCount { get; }

        public override double this[int i, int j]
        {
            get { return _original[_convertIndex(i, j)]; }
            set { _original[_convertIndex(i, j)] = value; }
        }

        public override Matrix NewInstance(int rowsCount, int colsCount) => Matrix.Zeros(rowsCount, colsCount);
        public override Vector AsVector => _original;
    }
}
