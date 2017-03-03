using System;
using System.Runtime.CompilerServices;

namespace LinAlg
{
    internal class MatrixConcrete : Matrix
    {
        private double[,] _elements;

        internal MatrixConcrete(int rowCount, int colCount)
        {
            _elements = new double[rowCount, colCount];
        }

        internal MatrixConcrete(double[,] elements, bool claimElements = true)
        {
            _elements = (double[,])(claimElements ? elements : elements.Clone());
        }

        public override int RowCount => _elements.GetUpperBound(0) + 1;

        public override int ColCount => _elements.GetUpperBound(1) + 1;

        public override double this[int i, int j]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _elements[i, j]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { _elements[i, j] = value; }
        }



        public override Matrix Copy()
        {
            var newElements = new double[RowCount, ColCount];
            Array.Copy(_elements, newElements, _elements.Length);
            return new MatrixConcrete(newElements, true);
        }

        public override Matrix NewInstance(int rowsCount, int colsCount) => new MatrixConcrete(rowsCount, colsCount);
        public override Vector AsVector => new VectorFlatMatrixView(_elements);
    }
}
