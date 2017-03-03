using System;

namespace LinAlg
{
    public static class LinAlg
    {
        public class LinAlgException : Exception
        {
            public LinAlgException() { }
            public LinAlgException(string message) : base(message) { }
            public LinAlgException(string message, Exception inner) : base(message, inner) { }
        }

        private static bool IsCloseToZero(double a, double delta = 1e-9) => Math.Abs(a) < delta;

        /// Solves AX=Y for X if A is an upper triangular matrix
        public static Matrix BackwardSubstitute(Matrix a, Matrix y)
        {
            var x = y.NewInstance(y.RowCount, y.ColCount);
            //Debug.WriteLine("\nStarting back substitution for X (AX = Y).");
            //Debug.WriteLine($"A =\n{a}\nY =\n{y}");
            for (int r = y.RowCount - 1; r >= 0; r--)
            {
                for (int yc = 0; yc < y.ColCount; yc++)  // for each column-vector in y
                {
                    x[r, yc] = y[r, yc];
                    for (int c = y.RowCount - 1; c > r; c--)
                        x[r, yc] -= a[r, c] * x[c, yc];
                    x[r, yc] /= a[r, r];
                }
                //Debug.WriteLine($"Step {y.RowCount - r}:\nX =\n{x}");
            }
            return x;
        }

        /// Solves AX=Y for X if A is a lower triangular matrix with unitary diagonal elements
        public static Matrix ForwardSubstituteI(Matrix a, Matrix y)
        {   // Solves AX=Y, where A is an upper triangular matrix
            var x = a.NewInstance(y.RowCount, y.ColCount);
            //Debug.WriteLine("\nStarting forward substitution for X (AX = Y).");
            //Debug.WriteLine($"A =\n{a}\nY =\n{y}");
            for (int r = 0; r < y.RowCount; r++)
            {
                for (int yc = 0; yc < y.ColCount; yc++)  // for each column-vector in y
                {
                    x[r, yc] = y[r, yc];
                    for (int c = 0; c < r; c++)
                        x[r, yc] -= a[r, c] * x[c, yc];
                }
                //Debug.WriteLine($"Step {r + 1}:\nX =\n{x}");
            }
            return x;
        }

        /// Creates a LU matrix from A
        public static void Luify(Matrix a)
        {
            //Debug.WriteLine("\nStarting creation of the LU matrix of A.");
            //Debug.WriteLine($"A =\n{a}");
            if (a.RowCount != a.ColCount) throw new ArgumentException("Matrica mora biti kvadratna.");

            for (int p = 0; p < a.RowCount - 1; p++)
            {
                if (IsCloseToZero(a[p, p])) throw new LinAlgException("Stožerni element je preblizu nuli.");
                for (int r = p + 1; r < a.RowCount; r++)
                {
                    a[r, p] /= a[p, p];
                    for (int c = p + 1; c < a.RowCount; c++)
                        a[r, c] -= a[r, p] * a[p, c];
                }
                //Debug.WriteLine($"Step {p + 1}:\nA =\n{a}");
            }
            if (IsCloseToZero(a[a.RowCount - 1, a.RowCount - 1])) throw new LinAlgException("Matrica je singularna ili 'blizu' singularnoj.");
        }

        /// Solves AX=Y by LU decomposition
        public static Matrix SolveLu(Matrix a, Matrix b)
        {
            //Debug.WriteLine("Starting LU decomposition.");
            if (a.RowCount != b.RowCount) throw new InvalidOperationException();
            var lu = a.Copy();
            Luify(lu);
            var y = ForwardSubstituteI(lu, b);
            return BackwardSubstitute(lu, y);
        }

        private static void MaximizePivot(int p, Matrix a, int[] perm)
        {
            int argmax = p;
            var max = Math.Abs(a[argmax, p]);
            for (int r = p + 1; r < a.RowCount; r++)
            {
                if (Math.Abs(a[r, p]) <= max) continue;
                max = Math.Abs(a[argmax = r, p]);
            }
            var temp = perm[argmax];
            perm[argmax] = perm[p]; perm[p] = temp;
            if (argmax == p) return;
            a.SwapRows(p, argmax);
        }

        /// Creates a PLU matrix from A and returns the permutation vector
        public static int[] Luppify(Matrix a)
        {
            //Debug.WriteLine("\nStarting creation of the PLU matrix of A.");
            //Debug.WriteLine($"A =\n{a}");
            var perm = new int[a.RowCount];
            for (int i = 0; i < perm.Length; perm[i] = i++) ;
            if (a.RowCount != a.ColCount) throw new ArgumentException("Matrica mora biti kvadratna.");
            for (int p = 0; p < a.RowCount - 1; p++)
            {
                MaximizePivot(p, a, perm);
                if (IsCloseToZero(a[p, p])) throw new LinAlgException("Matrica je singularna ili 'blizu' singularnoj.");
                //Debug.WriteLine($"Step {p + 1}a:\nA =\n{a}\nperm = [{string.Join(" ", perm)}]");
                for (int r = p + 1; r < a.RowCount; r++)
                {
                    a[r, p] /= a[p, p];
                    for (int c = p + 1; c < a.RowCount; c++)
                        a[r, c] -= a[r, p] * a[p, c];
                }
                //Debug.WriteLine($"Step {p + 1}b:\nA =\n{a}\nperm = [{string.Join(" ", perm)}]");
            }
            if (IsCloseToZero(a[a.RowCount - 1, a.RowCount - 1])) throw new LinAlgException("Matrica je singularna ili 'blizu' singularnoj.");
            return perm;
        }

        /// Solves AX=Y by LUP decomposition
        public static Matrix SolveLup(Matrix a, Matrix b)
        {
            if (a.RowCount != b.RowCount) throw new InvalidOperationException();

            //Debug.WriteLine("Starting LUP decomposition.");
            Matrix plu = a.Copy(), pb = b.NewInstance(b.RowCount, b.ColCount);
            var perm = Luppify(plu);
            for (int r = 0; r < b.RowCount; r++)
                for (int c = 0; c < b.ColCount; c++)
                    pb[r, c] = b[perm[r], c];
            var y = ForwardSubstituteI(plu, pb);
            return BackwardSubstitute(plu, y);
        }

        /// Solves AX=Y
        public static Matrix Solve(Matrix a, Matrix b) => SolveLup(a, b);
        public static Vector Solve(Matrix a, Vector b) => SolveLup(a, b.ToColumnMatrix(true)).AsVector;

    }
}