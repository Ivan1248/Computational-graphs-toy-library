using System;
using System.Runtime.CompilerServices;

namespace LinAlg
{
    internal class VectorFlatMatrixView : Vector
    {
        private readonly double[,] _elements;

        internal VectorFlatMatrixView(double[,] elements)
        {
            _elements = elements;
        }

        public override double this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                //if (i < 0 || i >= Dimension) throw new ArgumentException();
                unsafe { fixed (double* p = _elements) { return p[i]; } }
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (i < 0 || i >= Dimension) throw new ArgumentException();
                unsafe { fixed (double* p = _elements) { p[i] = value; } }
            }
        }

        public override int Dimension
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _elements.Length; }
        }

        public override Vector NewInstance(int dimension) => new VectorConcrete(dimension);
    }
}
