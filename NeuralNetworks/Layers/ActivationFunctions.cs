using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    public static class ActivationFunctions
    {
        public static ActivationFunction LogisticFunction = new ActivationFunction(
            function: x => 1 / (1 + Math.Exp(-x)),
            derivative: x => { var fx = 1 / (1 + Math.Exp(-x)); return fx * (1 - fx); });

        public static ActivationFunction SoftPlus = new ActivationFunction(
          function: x => Math.Log(1 + Math.Exp(x)),
          derivative: LogisticFunction.Function);

        public static ActivationFunction Rectifier = new ActivationFunction(
          function: x => Math.Max(0, x),
          derivative: x => x < 0 ? 0 : 1);

        public static ActivationFunction LeakyRectifier(double a) => new ActivationFunction(
          function: x => Math.Max(a * x, x),
          derivative: x => x < 0 ? a : 1);

        public static ActivationFunction Tanh = new ActivationFunction(
            function: Math.Tanh,
            derivative: x => { var fx = Math.Tanh(x); return 1 - fx * fx; });

        public static ActivationFunction Arctan = new ActivationFunction(
            function: Math.Atan,
            derivative: x => 1 / (1 + x * x));

        public static ActivationFunction Exp = new ActivationFunction(
            function: Math.Exp,
            derivative: Math.Exp);
    }
}
