using System;
using LinAlg;

namespace NeuralNetworks
{
    public class ActivationFunction
    {

        public ActivationFunction(Func<double, double> function, Func<double, double> derivative)
        {
            this.Function = function;
            this.Derivative = derivative;
        }

        public Func<double, double> Function { get; protected set; }
        public Func<double, double> Derivative { get; protected set; }

        public static implicit operator Func<double, double>(ActivationFunction f)
            => f.Function;
    }
}
