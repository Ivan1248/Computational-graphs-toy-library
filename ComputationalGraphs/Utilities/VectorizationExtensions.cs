using System;
using System.Collections.Generic;
using LinAlg;

namespace ComputationalGraphs.Utilities
{
    public static class VectorizationExtensions
    {
        public static Vector Mapped(this Vector x, Func<double, double> f)
        {
            Vector y = Vector.Zeros(x.Dimension);
            for (int i = 0; i < x.Dimension; i++) y[i] = f(x[i]);
            return y;
        }

        public static void DoElementwise<T1, T2>(this IList<T1> a, IList<T2> b, Action<T1, T2> action)
        {
            for (int i = 0; i < a.Count; i++) action(a[i], b[i]);
        }

        public static void DoElementwise<T>(this IList<T> a, Action<T> action)
        {
            for (int i = 0; i < a.Count; i++) action(a[i]);
        }
    }
}