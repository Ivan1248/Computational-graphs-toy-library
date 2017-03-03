using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class GradientDescentOptimizer : Optimizer
    {
        public GradientDescentOptimizer(double learningRate, List<Vector> parameters = null)
            : base(learningRate, parameters)
        {
        }

        public override void UpdateParameters(IList<Vector> gradient, double learningRateModifier)
        {
            double nmlr = -LearningRate * learningRateModifier;
            Parameters.DoElementwise(gradient, (p, g) => p.AddWeighted(g, nmlr));
        }
    }
}