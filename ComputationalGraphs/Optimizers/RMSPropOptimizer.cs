using System;
using System.Collections.Generic;
using System.Linq;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class RMSPropOptimizer : Optimizer
    {
        private double gamma;
        private double meanNormSqr = 0;

        public RMSPropOptimizer(double learningRate = 0.001, double gamma = 0.9, IList<Vector> parameters = null) :
            base(learningRate, parameters)
        {
            this.gamma = gamma;
        }

        public override void UpdateParameters(IList<Vector> gradient, double learningRateModifier = 1)
        {
            meanNormSqr = gamma * meanNormSqr + (1 - gamma) * gradient.Sum(x => x.NormSquare());
            double nmlr = -LearningRate / (Math.Sqrt(meanNormSqr) + 1e-10) * learningRateModifier;
            Parameters.DoElementwise(gradient, (p, g) => p.AddWeighted(g, nmlr));
        }
    }
}