using System;
using System.Collections.Generic;
using System.Diagnostics;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class DivBy : ParameterlessNode
    {
        private readonly Node a, b;

        public DivBy(Node a, Node b, string name = null) : base(new[] { a, b }, a.Dimension, name)
        {
            Debug.Assert(a.Dimension == b.Dimension);
            this.a = a;
            this.b = b;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => a.Output / b.Output;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            Vector bo = b.Output, bError = a.Output * error;
            for (int i = 0; i < Dimension; i++)
                bError[i] /= -bo[i] * bo[i];
            inputErrors = new[] { error / b.Output, bError };
        }
    }
}