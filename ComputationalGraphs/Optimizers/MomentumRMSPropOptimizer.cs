using System;
using System.Collections.Generic;
using System.Linq;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class MomentumRMSPropOptimizer : Optimizer
    {
        private double gamma;
        private readonly double momentum;

        private IList<Vector> meanGrad;
        private double meanNormSqr = 0;

        public MomentumRMSPropOptimizer(double learningRate = 0.0001, double gamma = 0.9, double momentum = 0.99,
            IList<Vector> parameters = null) : base(learningRate, parameters)
        {
            this.gamma = gamma;
            this.momentum = momentum;
        }

        public override void UpdateParameters(IList<Vector> gradient, double learningRateModifier = 1)
        {
            if (meanGrad == null) meanGrad = gradient.Select(x => x * 0).ToArray();
            meanGrad.DoElementwise(gradient, (mg, g) => mg.AddWeighted(momentum, g, 1 - momentum));
            meanNormSqr = gamma * meanNormSqr + (1 - gamma) * meanGrad.Sum(mg => mg.NormSquare());
            double nmlr = -LearningRate / (Math.Sqrt(meanNormSqr) + 1e-10) * learningRateModifier;
            Parameters.DoElementwise(meanGrad, (p, mg) => p.AddWeighted(mg, nmlr));
        }
    }
}