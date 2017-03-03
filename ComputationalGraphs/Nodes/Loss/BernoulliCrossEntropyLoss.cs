using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class BernoulliCrossEntropyLoss : LossNode
    {
        public BernoulliCrossEntropyLoss(Node pred, Node goal, string name = null) : base(pred, goal, name)
        {
        }

        protected override double GetLoss()
        {
            double loss = 0;
            Vector h = Pred, y = Goal;
            for (int i = 0; i < h.Dimension; i++)
                loss -= y[i] > 0.5 ? y[i] * Math.Log(h[i] + 1e-12) : (1 - y[i]) * Math.Log(1 - h[i] + 1e-12);
            return loss;
        }

        protected override Vector GetGradient()
        {
            Vector h = Pred, y = Goal;
            var grad = Vector.Zeros(h.Dimension);
            for (int i = 0; i < h.Dimension; i++)
                grad[i] = y[i] > 0.5 ? -y[i] / (h[i] + 1e-12) : (1 - y[i]) / (1 - h[i] + 1e-12);
            return grad;
        }
    }
}