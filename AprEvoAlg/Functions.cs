using System;
using LinAlg;

namespace AprEvoAlg
{
    public class Functions
    {
        public static double Pow(double x, double y) => Math.Pow(x, y);

        public static double Rosenbrock(Vector x)  // x0=(-1.9,2), xmin=(1,1), fmin=0
            => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);

        public static double F2(Vector x)  // x0=(0.1,0.3), xmin=(4,2), fmin=0
            => 100 * Pow(x[0] - 4, 2) + 4 * Pow(x[1] - 2, 2);

        public static double F3(Vector x)  // x0=(0,0,...,0), xmin = (1,2,...,n), fmin = 0
        {
            double y = 0;
            for (int i = 0; i < x.Dimension; i++) y += Pow(x[i] - i - 1, 2);
            return y;
        }

        public static double Jakobović(Vector x) =>  // x0=(5.1,1.1), xmin=(0,0), fmin=0
            Math.Abs((x[0] - x[1]) * (x[0] + x[1])) + Math.Sqrt(x[0] * x[0] + x[1] * x[1]);

        public static double ShafferF6(Vector x)  // xmin=(0,0,...,0), fmin = 0
        {
            double ns = x.NormSquare(), n = Math.Sqrt(ns);
            return 0.5 + (Pow(Math.Sin(n), 2) - 0.5) / Pow(1 + 0.001 * ns, 2);
        }

        public static double ShafferF7ish(Vector x)  // xmin=(0,0,...,0), fmin = 0
        {
            double ns = x.NormSquare();
            return Pow(ns, 0.25) * (1 + Pow(Math.Sin(Pow(50 * ns, 0.1)), 2));
        }
    }
}