using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace LinAlg
{
    public abstract class Matrix : IEquatable<Matrix>
    {
        public static Matrix Zeros(int rowCount, int colCount) => new MatrixConcrete(rowCount, colCount);
        public static Matrix Of(double[,] elements, bool claimElements = true) => new MatrixConcrete(elements, claimElements);
        public static Matrix Identity(int order)
        {
            var elements = new double[order, order];
            for (int i = 0; i < order; i++)
                elements[i, i] = 1;
            return new MatrixConcrete(elements, true);
        }

        public Matrix UpTriangle()
        {
            if (!IsSquareMatrix()) throw new InvalidOperationException();
            var ut = NewInstance(RowCount, RowCount);
            for (int i = 0; i < RowCount; i++)
                for (int j = i; j < ColCount; j++)
                    ut[i, j] = this[i, j];
            return ut;
        }

        public Matrix LoTriangle()
        {
            if (!IsSquareMatrix()) throw new InvalidOperationException();
            var lt = NewInstance(RowCount, RowCount);
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j <= i; j++)
                    lt[i, j] = this[i, j];
            return lt;
        }

        public Matrix DiagonalMatrix()
        {
            if (!IsSquareMatrix()) throw new InvalidOperationException();
            var lt = NewInstance(RowCount, RowCount);
            for (int i = 0; i < RowCount; i++)
                lt[i, i] = this[i, i];
            return lt;
        }

        public Matrix ToDiagonalMatrix()
        {
            Matrix lt;
            if (RowCount == 1)
            {
                lt = NewInstance(ColCount, ColCount);
                for (int i = 0; i < ColCount; i++) lt[i, i] = this[0, i];
            }
            else if (ColCount == 1)
            {
                lt = NewInstance(RowCount, RowCount);
                for (int i = 0; i < RowCount; i++) lt[i, i] = this[i, 0];
            }
            else throw new InvalidOperationException();
            return lt;
        }

        public abstract int RowCount { get; }
        public abstract int ColCount { get; }

        public abstract double this[int i, int j]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set;
        }

        public virtual Matrix Copy()
        {
            Matrix mat = NewInstance(RowCount, ColCount);
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    mat[i, j] = this[i, j];
            return mat;
        }
        
        public abstract Matrix NewInstance(int rowsCount, int colsCount);
        public Matrix NewInstance() => NewInstance(RowCount, ColCount);

        public Matrix T
        {
            get
            {
                Matrix n = NewInstance(ColCount, RowCount);
                for (int i = 0; i < RowCount; i++)
                    for (int j = 0; j < ColCount; j++)
                        n[j, i] = this[i, j];
                return n;
            }
        }

        public Matrix Tv => new MatrixTransposeView(this);

        public Matrix Add(Matrix other)
        {
            Debug.Assert(IsEquallySized(other));
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] += other[i, j];
            return this;
        }

        public Matrix Add(double s)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] += s;
            return this;
        }

        public Matrix Sub(Matrix other)
        {
            Debug.Assert(IsEquallySized(other));
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] -= other[i, j];
            return this;
        }

        public Matrix Sub(double s)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] -= s;
            return this;
        }

        public Matrix SubFrom(double s)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] = s - this[i, j];
            return this;
        }

        public Matrix Mul(Matrix a)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] *= a[i, j];
            return this;
        }

        public Matrix Mul(double s)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] *= s;
            return this;
        }

        public Matrix DivBy(double divisor)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] /= divisor;
            return this;
        }

        public Matrix DivBy(Matrix divisors)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] /= divisors[i, j];
            return this;
        }

        public Matrix Div(double dividend)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] = dividend / this[i, j];
            return this;
        }

        public Matrix Div(Matrix dividends)
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    this[i, j] = dividends[i, j] / this[i, j];
            return this;
        }

        public Matrix MatMul(Matrix other)
        {
            Matrix mat = NewInstance(RowCount, other.ColCount);
            for (int i = 0; i < mat.RowCount; i++)
                for (int j = 0; j < mat.ColCount; j++)
                    for (int k = 0; k < ColCount; k++)
                        mat[i, j] += this[i, k] * other[k, j];
            return mat;
        }

        public Vector MatMul(Vector other)
        {
            Vector r = other.NewInstance(RowCount);
            for (int i = 0; i < this.RowCount; i++)
                for (int j = 0; j < this.ColCount; j++)
                    r[i] += this[i, j] * other[j];
            return r;
        }

        public static Matrix operator +(Matrix a, Matrix b) => a.Copy().Add(b);
        public static Matrix operator +(Matrix a, double s) => a.Copy().Add(s);
        public static Matrix operator +(double s, Matrix a) => a + s;
        public static Matrix operator -(Matrix a, Matrix b) => a.Copy().Sub(b);
        public static Matrix operator -(Matrix a, double s) => a.Copy().Sub(s);
        public static Matrix operator -(double s, Matrix a) => a.Copy().SubFrom(s);
        public static Matrix operator *(Matrix a, Matrix b) => a.Copy().Mul(b);
        public static Matrix operator *(Matrix a, double s) => a.Copy().Mul(s);
        public static Matrix operator *(double s, Matrix a) => a * s;
        public static Matrix operator /(Matrix a, Matrix b) => a.Copy().DivBy(b);
        public static Matrix operator /(Matrix a, double s) => a.Copy().DivBy(s);
        public static Matrix operator /(double s, Matrix a) => a.Copy().Div(s);
        public static Matrix operator -(Matrix a) => -1 * a;

        public double Trace()
        {
            if (!IsSquareMatrix()) throw new InvalidOperationException();
            double tr = 0;
            for (int i = 0; i < RowCount; i++) tr += this[i, i];
            return tr;
        }

        public double Det()
        {
            if (!IsSquareMatrix()) throw new InvalidOperationException();
            if (RowCount == 1) return this[0, 0];
            if (RowCount == 2) return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            double det = 0;
            for (int i = 0; i < RowCount; i++)
            {
                double temp = this[0, i] * SubMatrix(0, i, true).Det();
                if ((i & 1) == 0) det += temp;
                else det -= temp;
            }
            return det;
        }

        public Matrix SubMatrix(int exclRow, int exclCol, bool liveView) => liveView
            ? new MatrixSubMatrixView(this, exclRow, exclCol)
            : new MatrixSubMatrixView(this, exclRow, exclCol).Copy();

        public Matrix Inv()
        {
            Matrix cof = NewInstance(RowCount, ColCount);
            for (int i = 0; i < cof.RowCount; i++)
                for (int j = 0; j < cof.ColCount; j++)
                {
                    cof[i, j] = SubMatrix(i, j, true).Det();
                    if (((i + j) & 1) != 0) cof[i, j] = -cof[i, j];
                }
            double det = 0;
            for (int j = 0; j < cof.RowCount; j++)
                det += this[0, j] * cof[0, j];
            return cof.Tv.Mul(1 / det);
        }

        public Matrix SwapRows(int i, int j)
        {
            for (int c = 0; c < ColCount; c++)
            {
                var temp = this[i, c];
                this[i, c] = this[j, c];
                this[j, c] = temp;
            }
            return this;
        }

        public Matrix SwapCols(int i, int j)
        {
            for (int r = 0; r < ColCount; r++)
            {
                var temp = this[r, i];
                this[r, i] = this[r, j];
                this[r, j] = temp;
            }
            return this;
        }

        public abstract Vector AsVector { get; }

        public Vector ToVector()
        {
            Vector vec;
            if (RowCount == 1)
            {
                vec = Vector.Zeros(ColCount);
                for (int i = 0; i < ColCount; i++) vec[i] = this[0, i];
            }
            else if (ColCount == 1)
            {
                vec = Vector.Zeros(RowCount);
                for (int i = 0; i < ColCount; i++) vec[i] = this[i, 0];
            }
            else throw new ArgumentOutOfRangeException();
            return vec;
        }

        public double[,] ToArray()
        {
            double[,] arr = new double[RowCount, ColCount];
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    arr[i, j] = this[i, j];
            return arr;
        }

        private bool IsEquallySized(Matrix other) => !(other.RowCount != RowCount || other.ColCount != ColCount);

        private bool IsSquareMatrix() => RowCount == ColCount;

        public string ToString(int precision, bool fixedDecimalPoint = false)
        {
            string[,] elemStrings = new string[RowCount, ColCount];
            int maxElemLen = 0;
            /*string format = "{0:0." + new string(@fixedDecimalPoint ? '0' : '#', precision) + "}";*/ // TODO: fixedDecimalPoint
            string format = $"{{0:G{precision}}}";
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                {
                    elemStrings[i, j] = string.Format(format, this[i, j]);
                    maxElemLen = Math.Max(maxElemLen, elemStrings[i, j].Length);
                }
            StringBuilder sb = new StringBuilder();
            Action<int> writeRow = delegate (int i)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    for (int k = 0; k < maxElemLen - elemStrings[i, j].Length; k++)
                        sb.Append(' ');
                    sb.Append(elemStrings[i, j]).Append(' ');
                }
                sb.Length -= 1;
            };
            sb.Append("[[");
            writeRow(0);
            sb.Append(']');
            for (int i = 1; i < RowCount; i++)
            {
                sb.AppendLine().Append(" [");
                writeRow(i);
                sb.Append("]");
            }
            sb.Append(']');
            return sb.ToString();
        }

        public bool Equals(Matrix other) => EqualsApproximately(other, 0);

        public bool EqualsApproximately(Matrix other, double delta = 1e-9)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other == null || other.RowCount != RowCount || other.ColCount != RowCount) return false;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    if (Math.Abs(this[i, j] - other[i, j]) > delta) return false;
            return true;
        }

        public override string ToString() => ToString(/*3*/9, false);
    }
}
