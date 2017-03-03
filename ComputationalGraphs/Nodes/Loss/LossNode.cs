using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public abstract class LossNode : ParameterlessNode
    {
        private readonly Node pred, goal;
        protected Vector Pred => pred.Output;
        protected Vector Goal => goal.Output;

        protected LossNode(Node pred, Node goal, string name = null) : base(new[] { pred }, 1, name)
        {
            this.pred = pred;
            this.goal = goal;
        }

       protected sealed override Vector GetOutputProtected() =>
            Vector.Of(GetLoss());

        protected sealed override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { GetGradient() };

        protected abstract double GetLoss();

        protected abstract Vector GetGradient();
    }
}