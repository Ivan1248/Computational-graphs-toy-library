using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public static class LossFunctions
    {
        public static LossFunction SquaredLoss = new LossFunction(
            function: (x, z) => (x - z).NormSquare() / 2,
            gradient: (x, z) => x - z);

        public static LossFunction CathegoricCrossEntropyLoss = new LossFunction(
            function: (x, z) =>
            {
                double loss = 0;
                for (int i = 0; i < x.Dimension; i++)
                    loss -= z[i] * Math.Log(x[i] + 1e-12);
                return loss;
            },
            gradient: (x, z) => (-z).DivBy(x));

        public static LossFunction BernoulliCrossEntropyLoss = new LossFunction(
            function: (x, z) =>
            {
                double loss = 0;
                for (int i = 0; i < x.Dimension; i++)
                    loss -= z[i] > 0.5 ? z[i] * Math.Log(x[i] + 1e-12) : (1 - z[i]) * Math.Log(1 - x[i] + 1e-12);
                return loss;
            },
            gradient: (x, z) =>
            {
                var grad = Vector.Zeros(x.Dimension);
                for (int i = 0; i < x.Dimension; i++)
                    grad[i] = z[i] > 0.5 ? -z[i] / (x[i] + 1e-1) : (1 - z[i]) / (1 - x[i] + 1e-1);
                return grad;
            });
    }
}
