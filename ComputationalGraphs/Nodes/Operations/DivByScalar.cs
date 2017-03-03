using System;
using System.Collections.Generic;
using System.Diagnostics;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class DivByScalar : ParameterlessNode
    {
        private readonly Node a, b;

        public DivByScalar(Node a, Node b, string name = null) : base(new[] { a, b }, a.Dimension, name)
        {
            Debug.Assert(b.Dimension == 1);
            this.a = a;
            this.b = b;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => a.Output / b.Output[0];

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            double bo = b.Output[0];
            inputErrors = new[] { error / bo, Vector.Of(a.Output.MatMul(error)).DivBy(-bo * bo) };
        }
    }
}