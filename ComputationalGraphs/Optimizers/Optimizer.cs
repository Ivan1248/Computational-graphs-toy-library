using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public abstract class Optimizer
    {
        protected readonly Node LossNode;
        protected IList<Vector> Parameters;
        public double LearningRate { get; set; } = 0;

        protected Optimizer(double learningRate, IList<Vector> parameters = null)
        {
            LearningRate = learningRate;
            Parameters = parameters;
        }

        public void SetParameters(IList<Vector> parameters) => Parameters = parameters;

        public abstract void UpdateParameters(IList<Vector> gradient, double learningRateModifier = 1);
    }
}