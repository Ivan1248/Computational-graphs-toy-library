using System;
using LinAlg;

namespace NeuralNetworks
{
    public static class VectorizationExtensions
    {
        delegate double RealFunction(double x);
        public static Vector Map(this Func<double, double> f, Vector x)
        {
            Vector y = Vector.Zeros(x.Dimension);
            for (int i = 0; i < x.Dimension; i++) y[i] = f(x[i]);
            return y;
        }

        public static Matrix Map(this Func<double, double> f, Matrix x)
        {
            var y = x.NewInstance(x.RowCount, x.ColCount);
            for (int i = 0; i < x.RowCount; i++)
                for (int j = 0; j < x.ColCount; j++)
                    y[i, j] = f(x[i, j]);
            return y;
        }

        public static Vector Map(this ActivationFunction f, Vector x) =>
            f.Function.Map(x);
    }
}