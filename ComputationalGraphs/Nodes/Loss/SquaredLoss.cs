using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class SquaredLoss : LossNode
    {
        public SquaredLoss(Node prediction, Node goal, string name = null) : base(prediction, goal, name)
        {
        }

        protected override double GetLoss() => (Pred - Goal).NormSquare() / 2;

        protected override Vector GetGradient() => Pred - Goal;
    }
}