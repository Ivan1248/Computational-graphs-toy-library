using System;

namespace LinAlg
{
    internal class MatrixSubMatrixView : Matrix
    {
        private Matrix _original;
        private int[] _rInds;
        private int[] _cInds;

        internal MatrixSubMatrixView(Matrix original, int excludedRow, int excludedCol)
        {
            _original = original;
            Func<int, int, int[]> createIndexArray = delegate (int max, int excl)
            {
                int[] indexArr = new int[max - 1];
                for (int i = 0; i < excl; i++) indexArr[i] = i;
                for (int i = excl; i < max - 1; i++) indexArr[i] = i + 1;
                return indexArr;
            };
            _rInds = createIndexArray(original.RowCount, excludedRow);
            _cInds = createIndexArray(original.ColCount, excludedCol);
        }

        public MatrixSubMatrixView(Matrix original, int[] rowIndexes, int[] colIndexes)
        {
            _original = original;
            _rInds = rowIndexes;
            _cInds = colIndexes;
        }

        public override int RowCount => _rInds.Length;

        public override int ColCount => _cInds.Length;

        public override double this[int i, int j]
        {
            get { return _original[_rInds[i], _cInds[j]]; }
            set { _original[_rInds[i], _cInds[j]] = value; }
        }

        public override Matrix NewInstance(int rowsCount, int colsCount) => _original.NewInstance(rowsCount, colsCount);
        public override Vector AsVector
        {
            get { throw new InvalidOperationException(); }
        }
    }
}
