using System;

namespace apr_lv
{
    public static class LinAlg
    {
        public static IMatrix ToLuMatrix(IMatrix m)
        {
            if (m.RowsCount != m.ColsCount) throw new ArgumentException("Matrica mora biti kvadratna.");
            var a = m.Copy();
            for (int p = 0; p < m.RowsCount; p++)
                for (int r = p + 1; r < m.RowsCount; r++)
                {
                    a[r, p] /= a[p, p];
                    for (int c = p + 1; c < m.RowsCount; c++)
                        a[r, c] -= a[r, p] * a[p, c];
                }
            return a;
        }

        private static void MaximizePivot(int p, IMatrix a, int[] perm)
        {
            int argmax = p;
            var max = Math.Abs(a[argmax, p]);
            for (int r = p + 1; r < a.RowsCount; r++)
            {
                if (Math.Abs(a[r, p]) <= max) continue;
                argmax = r;
                max = Math.Abs(a[argmax, p]);
            }
            var temp = perm[argmax];
            perm[argmax] = perm[p]; perm[p] = temp;
            if (argmax == p) return;
            a.SwapRows(p, argmax);
        }

        public static int[] Luppify(IMatrix a)
        {
            var perm = new int[a.RowsCount];
            for (int i = 0; i < perm.Length; perm[i] = i++) ;
            if (a.RowsCount != a.ColsCount) throw new ArgumentException("Matrica mora biti kvadratna.");
            for (int p = 0; p < a.RowsCount; p++)
            {
                MaximizePivot(p, a, perm);
                for (int r = p + 1; r < a.RowsCount; r++)
                {
                    a[r, p] /= a[p, p];
                    for (int c = p + 1; c < a.RowsCount; c++)
                        a[r, c] -= a[r, p] * a[p, c];
                }
            }
            return perm;
        }

        public static IMatrix Solve0(IMatrix a, IMatrix b)
        {
            /* solves `a` * `x` = `pb`*/
            if (a.RowsCount != b.RowsCount) throw new InvalidOperationException();
            var lu = ToLuMatrix(a);
            var y = a.NewInstance(b.RowsCount, b.ColsCount);
            for (int r = 0; r < b.RowsCount; r++) // solve Ly=pb for y
                for (int bc = 0; bc < b.ColsCount; bc++)  // for each column-vector in pb
                {
                    y[r, bc] = b[r, bc];
                    for (int c = 0; c < r; c++)
                        y[r, bc] -= lu[r, c] * y[c, bc];
                }
            var x = a.NewInstance(b.RowsCount, b.ColsCount);
            for (int r = b.RowsCount - 1; r >= 0; r--) // solve Ux=y for x
                for (int bc = 0; bc < b.ColsCount; bc++)  // for each column-vector in pb
                {
                    x[r, bc] = y[r, bc];
                    for (int c = a.RowsCount - 1; c > r; c--)
                        x[r, bc] -= lu[r, c] * x[c, bc];
                    x[r, bc] /= lu[r, r];
                }
            return x;
        }

        public static IMatrix Solve(IMatrix a, IMatrix b)
        {
            /* solves `a` * `x` = `pb`*/
            if (a.RowsCount != b.RowsCount) throw new InvalidOperationException();

            IMatrix plu = a.Copy();
            var perm = Luppify(plu);

            var y = a.NewInstance(b.RowsCount, b.ColsCount);
            for (int r = 0, permr = perm[r]; r < b.RowsCount; r++) // solve Ly=pb for y (fw-subs)
                for (int bc = 0; bc < b.ColsCount; bc++) // for each column-vector in pb
                {
                    y[r, bc] = b[permr, bc];
                    for (int c = 0; c < r; c++)
                        y[r, bc] -= plu[r, c] * y[c, bc];
                }

            var x = a.NewInstance(y.RowsCount, y.ColsCount);
            for (int r = y.RowsCount - 1; r >= 0; r--) // solve Ux=y for x (bw-subs)
                for (int bc = 0; bc < y.ColsCount; bc++)  // for each column-vector in y
                {
                    x[r, bc] = y[r, bc];
                    for (int c = a.RowsCount - 1; c > r; c--)
                        x[r, bc] -= plu[r, c] * x[c, bc];
                    x[r, bc] /= plu[r, r];
                }
            return x;
        }
    }
}