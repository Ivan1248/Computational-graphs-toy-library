using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LinAlg
{
    public abstract class Vector
    {
        public static Vector Zeros(int dimension) => new VectorConcrete(dimension);
        public static Vector Of(params double[] elements) => new VectorConcrete(elements);

        public static Vector Filled(int dimension, double value)
        {
            var x = new VectorConcrete(dimension);
            for (int i = 0; i < dimension; i++)
                x[i] = value;
            return x;
        }

        public static Vector Ones(int dimension) => Filled(dimension, 1);

        public static Vector OneHot(int value, int dimension)
        {
            var x = Zeros(dimension);
            x[value] = 1;
            return x;
        }

        public void SetAll(params double[] values)
        {
            for (int i = 0; i < Dimension; i++)
                this[i] = values[i];
        }

        public void SetAll(Vector values)
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
        public virtual Vector Repeat(int n)
        {
            Vector vec = NewInstance(Dimension * n);
            for (int i = 0; i < Dimension * n; i++)
                vec[i] = this[i % Dimension];
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

        public double DistanceSquare(Vector other)
        {
            double ds = 0;
            for (var i = 0; i < Dimension; i++)
            {
                double a = this[i] - other[i];
                ds += a * a;
            }
            return ds;
        }

        public double Distance(Vector other) => Math.Sqrt(DistanceSquare(other));

        public double Sum()
        {
            double sum = 0;
            for (int i = 0; i < this.Dimension; i++)
                sum += this[i];
            return sum;
        }

        public int ArgMax()
        {
            int argmax = 0;
            double max = this[0];
            for (int i = 1; i < Dimension; i++)
            {
                if (this[i] <= max) continue;
                max = this[argmax = i];
            }
            return argmax;
        }
        public int ArgMin()
        {
            int argmin = 0;
            double min = this[0];
            for (int i = 1; i < Dimension; i++)
            {
                if (this[i] >= min) continue;
                min = this[argmin = i];
            }
            return argmin;
        }

        public double Max()
        {
            double max = this[0];
            for (int i = 1; i < Dimension; i++)
                if (this[i] > max)
                    max = this[i];
            return max;
        }

        public double Min()
        {
            double min = this[0];
            for (int i = 1; i < Dimension; i++)
                if (this[i] < min)
                    min = this[i];
            return min;
        }

        public Vector Normalize() => Mul(1 / Norm());

        public Vector NNormalize() => Copy().Normalize();

        public double Cosine(Vector other) => MatMul(other) / Math.Sqrt(NormSquare() * other.NormSquare());

        public Vector NVectorProduct(Vector other)
        {
            Debug.Assert(Dimension == 3);
            var n = NewInstance(3);
            n[0] = this[1] * other[2] - this[2] * other[1];
            n[1] = -this[0] * other[2] + this[2] * other[0];
            n[2] = this[0] * other[1] - this[1] * other[0];
            return n;
        }

        public Vector NFromHomogenous() => CopyPart(Dimension - 1).Mul(1 / this[Dimension - 1]);

        public Vector Slice(int from, int to)
        {
            var r = NewInstance(to - from);
            for (int i = from; i < to; i++)
                r[i - from] = this[i];
            return r;
        }

        public static Vector Concat(params Vector[] vectors)
        {
            var r = Zeros(vectors.Sum(x => x.Dimension));
            int i = 0;
            foreach (Vector v in vectors)
                for (int k = 0; k < v.Dimension; k++)
                    r[i++] = v[k];
            return r;
        }

        public Matrix ToRowMatrix(bool liveView)
        {
            if (liveView)
                return new MatrixVectorView(this, true);
            Matrix mat = Matrix.Zeros(1, Dimension);
            for (var i = 0; i < Dimension; i++) mat[0, i] = this[i];
            return mat;
        }

        public Matrix ToColumnMatrix(bool liveView)
        {
            if (liveView)
                return new MatrixVectorView(this, false);
            Matrix mat = Matrix.Zeros(Dimension, 1);
            for (var i = 0; i < Dimension; i++) mat[i, 0] = this[i];
            return mat;
        }

        public double[] ToArray()
        {
            var arr = new double[Dimension];
            for (var i = 0; i < Dimension; i++) arr[i] = this[i];
            return arr;
        }

        public Vector Add(Vector other)
        {
            Debug.Assert(Dimension == other.Dimension);
            for (var i = 0; i < Dimension; i++) this[i] += other[i];
            return this;
        }

        public Vector AddWeighted(double thisWeight, Vector other, double otherWeight)
        {
            Debug.Assert(Dimension == other.Dimension);
            for (var i = 0; i < Dimension; i++) this[i] = this[i] * thisWeight + other[i] * otherWeight;
            return this;
        }

        public Vector AddWeighted(Vector other, double otherWeight)
        {
            Debug.Assert(Dimension == other.Dimension);
            for (var i = 0; i < Dimension; i++) this[i] += other[i] * otherWeight;
            return this;
        }

        public Vector AddWeighted(double thisWeight, Vector other)
        {
            Debug.Assert(Dimension == other.Dimension);
            for (var i = 0; i < Dimension; i++) this[i] = this[i] * thisWeight + other[i];
            return this;
        }

        public Vector Add(double a)
        {
            for (var i = 0; i < Dimension; i++) this[i] += a;
            return this;
        }

        public Vector Sub(Vector other)
        {
            Debug.Assert(Dimension == other.Dimension);
            for (var i = 0; i < Dimension; i++) this[i] -= other[i];
            return this;
        }

        public Vector Sub(double a)
        {
            for (var i = 0; i < Dimension; i++) this[i] -= a;
            return this;
        }

        public Vector SubFrom(double a)
        {
            for (var i = 0; i < Dimension; i++) this[i] = a - this[i];
            return this;
        }

        public Vector Mul(Vector other)
        {
            for (var i = 0; i < Dimension; i++) this[i] *= other[i];
            return this;
        }

        public Vector Mul(double m)
        {
            for (var i = 0; i < Dimension; i++) this[i] *= m;
            return this;
        }

        public Vector DivBy(Vector divisor)
        {
            for (int i = 0; i < Dimension; i++)
                this[i] /= divisor[i];
            return this;
        }

        public Vector DivBy(double divisor)
        {
            for (int i = 0; i < Dimension; i++)
                this[i] /= divisor;
            return this;
        }

        public Vector MatMul(Matrix other)
        {
            Vector r = this.NewInstance(other.ColCount);
            for (int i = 0; i < other.ColCount; i++)
                for (int j = 0; j < other.RowCount; j++)
                    r[i] += this[j] * other[j, i];
            return r;
        }

        public double MatMul(Vector other)
        {
            double r = 0;
            for (int i = 0; i < Dimension; i++) r += this[i] * other[i];
            return r;
        }

        public Matrix OutMul(Vector other)
        {
            Matrix r = Matrix.Zeros(this.Dimension, other.Dimension);
            for (int i = 0; i < this.Dimension; i++)
                for (int j = 0; j < other.Dimension; j++)
                    r[i, j] = this[i] * other[j];
            return r;
        }

        public static Vector operator +(Vector v1, Vector v2) => v1.Copy().Add(v2);
        public static Vector operator +(double a, Vector v) => v.Copy().Add(a);
        public static Vector operator +(Vector v, double a) => v.Copy().Add(a);
        public static Vector operator -(Vector v1, Vector v2) => v1.Copy().Sub(v2);
        public static Vector operator -(double a, Vector v) => v.Copy().SubFrom(a);
        public static Vector operator -(Vector v, double a) => v.Copy().Sub(a);
        public static Vector operator *(Vector v1, Vector v2) => v1.Copy().Mul(v2);
        public static Vector operator *(Vector v, double m) => v.Copy().Mul(m);
        public static Vector operator *(double m, Vector v1) => v1.Copy().Mul(m);
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