using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class CathegoricCrossEntropyLoss : LossNode
    {
        public CathegoricCrossEntropyLoss(Node pred, Node goal, string name = null) : base(pred, goal, name)
        {
        }

        protected override double GetLoss()
        {
            double loss = 0;
            for (int i = 0; i < Pred.Dimension; i++)
                loss -= Goal[i] * Math.Log(Pred[i] + 1e-12);
            return loss;
        }

        protected override Vector GetGradient() => (-Goal).DivBy(Pred);
    }
}