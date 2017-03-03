using System;

namespace LinAlg
{
    internal class VectorMatrixView : Vector
    {
        private readonly Matrix _original;
        private readonly bool _isRowMatrix;

        internal VectorMatrixView(Matrix original)
        {
            _isRowMatrix = original.RowCount == 1;
            if (_isRowMatrix) Dimension = original.ColCount;
            else if (original.ColCount == 1) Dimension = original.RowCount;
            else throw new ArgumentOutOfRangeException();
            _original = original;
        }

        public override double this[int i]
        {
            get { return _isRowMatrix ? _original[0, i] : _original[i, 0]; }
            set
            {
                if (_isRowMatrix) _original[0, i] = value;
                else _original[i, 0] = value;
            }
        }

        public override int Dimension { get; }

        public override Vector NewInstance(int dimension) => new VectorConcrete(dimension);
    }
}
