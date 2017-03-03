using System;
using LinAlg;

namespace NeuralNetworks
{
    public class LossFunction
    {
        private readonly Func<Vector, Vector, double> function;
        private readonly Func<Vector, Vector, Vector> gradient;

        public LossFunction(Func<Vector, Vector, double> function, Func<Vector, Vector, Vector> gradient)
        {
            this.function = function;
            this.gradient = gradient;
        }

        public double Function(Vector pred, Vector label) => function(pred, label);
        public Vector Gradient(Vector pred, Vector label) => gradient(pred, label);

        public static implicit operator Func<Vector, Vector, double>(LossFunction f)
            => f.Function;
    }
}
