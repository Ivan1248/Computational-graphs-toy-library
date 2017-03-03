using System;

namespace LinAlg
{
    internal class MatrixTransposeView : Matrix
    {
        private Matrix _original;

        internal MatrixTransposeView(Matrix original)
        {
            _original = original;
        }

        public override int RowCount => _original.ColCount;

        public override int ColCount => _original.RowCount;

        public override double this[int i, int j]
        {
            get { return _original[j, i]; }
            set { _original[j, i] = value; }
        }

        public override Matrix NewInstance(int rowsCount, int colsCount) => _original.NewInstance(rowsCount, colsCount);
        public override Vector AsVector
        {
            get { throw new InvalidOperationException(); }
        }
    }
}
