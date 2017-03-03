using System;
using System.Runtime.CompilerServices;

namespace LinAlg
{
    internal class VectorConcrete : Vector
    {
        public static VectorConcrete Null => new VectorConcrete(0, 0, 0);

        private readonly double[] _elements;

        internal VectorConcrete(int dimension)
        {
            _elements = new double[dimension];
        }

        internal VectorConcrete(params double[] elements)
        {
            _elements = elements;
        }

        public VectorConcrete(bool claimElements, params double[] elements)
        {
            _elements = (double[])(claimElements ? elements : elements.Clone());
        }

        public override double this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _elements[i]; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { _elements[i] = value; }
        }

        public override int Dimension => _elements.Length;

        public override Vector Copy() => new VectorConcrete((double[])_elements.Clone());

        public override Vector CopyPart(int n)
        {
            double[] newElements = new double[n];
            int ncopy;
            if (n > _elements.Length)
            {
                ncopy = _elements.Length;
                Array.Clear(newElements, ncopy, n - _elements.Length);
            }
            else
                ncopy = n;
            Array.Copy(_elements, newElements, ncopy);
            return new VectorConcrete(newElements);
        }

        public override Vector NewInstance(int dimension) => new VectorConcrete(dimension);

        public static VectorConcrete ParseSimple(string repr) => new VectorConcrete(Parsing.ParseDoubleArray(repr));
    }
}