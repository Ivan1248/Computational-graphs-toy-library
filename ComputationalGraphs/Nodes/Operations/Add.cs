using System;
using System.Collections.Generic;
using System.Diagnostics;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Add : ParameterlessNode
    {
        private readonly Node a, b;

        public Add(Node a, Node b, string name = null) : base(new[] { a, b }, a.Dimension, name)
        {
            Debug.Assert(a.Dimension == b.Dimension);
            this.a = a;
            this.b = b;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => a.Output + b.Output;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { error, error };
    }
}