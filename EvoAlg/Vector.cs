using System;
using System.Text;

namespace EvoAlg
{
    public abstract class Vector
    {
        public static Vector New(int dimension) => new VectorConcrete(dimension);
        public static Vector New(params double[] elements) => new VectorConcrete(elements);

        public void SetMultiple(params double[] values)
        {
            for (int i = 0; i < Dimension; i++)
                this[i] = values[i];
        }

        public abstract double this[int i] { get; set; }

        public abstract int Dimension { get; }
        public virtual Vector Copy()
        {
            Vector vec = NewInstance(Dimension);
            for (int i = 0; i < Dimension; i++)
                vec[i] = this[i];
            return vec;
        }
        public virtual Vector CopyPart(int n)
        {
            Vector vec = NewInstance(n);
            int ncopy = n > Dimension ? Dimension : n;
            for (int i = 0; i < ncopy; i++)
                vec[i] = this[i];
            return vec;
        }
        public abstract Vector NewInstance(int dimension);

        public double NormSquare()
        {
            double ns = 0;
            for (var i = 0; i < Dimension; i++) ns += this[i] * this[i];
            return ns;
        }

        public double Norm() => Math.Sqrt(NormSquare());

        public Vector Normalize() => Multiply(1 / Norm());

        public Vector NNormalize() => Copy().Normalize();

        public double Cosine(Vector other) => ScalarProduct(other) / Math.Sqrt(NormSquare() * other.NormSquare());

        public double ScalarProduct(Vector other)
        {
            if (Dimension != other.Dimension) throw new InvalidOperationException("Nije podržan skalarni produkt vektora različitih dimenzija.");
            double sp = 0;
            for (var i = 0; i < Dimension; i++) sp += this[i] * other[i];
            return sp;
        }

        public Vector NVectorProduct(Vector other)
        {
            if (Dimension != 3) throw new ArgumentOutOfRangeException();
            var n = NewInstance(3);
            n[0] = this[1] * other[2] - this[2] * other[1];
            n[1] = -this[0] * other[2] + this[2] * other[0];
            n[2] = this[0] * other[1] - this[1] * other[0];
            return n;
        }

        public Vector NFromHomogenous() => CopyPart(Dimension - 1).Multiply(1 / this[Dimension - 1]);

        public double[] ToArray()
        {
            var arr = new double[Dimension];
            for (var i = 0; i < Dimension; i++) arr[i] = this[i];
            return arr;
        }

        public Vector Add(Vector other)
        {
            if (Dimension != other.Dimension) throw new InvalidOperationException();
            for (var i = 0; i < Dimension; i++) this[i] += other[i];
            return this;
        }

        public Vector Sub(Vector other)
        {
            if (Dimension != other.Dimension) throw new InvalidOperationException();
            for (var i = 0; i < Dimension; i++) this[i] -= other[i];
            return this;
        }

        public Vector Multiply(double m)
        {
            for (var i = 0; i < Dimension; i++) this[i] *= m;
            return this;
        }

        public Vector DivBy(double divisor)
        {
            for (int i = 0; i < Dimension; i++)
                this[i] /= divisor;
            return this;
        }

        public Vector DivBy(Vector divisor)
        {
            for (int i = 0; i < Dimension; i++)
                this[i] /= divisor[i];
            return this;
        }

        public static Vector operator +(Vector v1, Vector v2) => v1.Copy().Add(v2);
        public static Vector operator -(Vector v1, Vector v2) => v1.Copy().Sub(v2);
        public static Vector operator *(Vector v1, double m) => v1.Copy().Multiply(m);
        public static Vector operator *(double m, Vector v1) => v1.Copy().Multiply(m);
        public static Vector operator /(Vector v1, double m) => v1.Copy().DivBy(m);
        public static Vector operator /(Vector v1, Vector v2) => v1.Copy().DivBy(v2);
        public static Vector operator -(Vector v1) => v1.NewInstance(v1.Dimension).Sub(v1);

        public string ToString(int precision, bool showTrailingZeros = true)
        {
            StringBuilder sb = new StringBuilder();
            string format = "{0:0." + new string(showTrailingZeros ? '0' : '#', precision) + "} ";
            sb.Append("[");
            for (int i = 0; i < Dimension; i++)
                sb.AppendFormat(format, this[i]);
            sb.Length -= 1;
            sb.Append("]");
            return sb.ToString();
        }

        public override string ToString() => ToString(3, false);
    }
}