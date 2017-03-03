using System;
using System.Collections.Generic;
using System.Diagnostics;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class AddCScal : ParameterlessNode
    {
        private readonly Node a;
        private readonly double scalar;

        public AddCScal(Node a, double scalar, string name = null) : base(new[] { a }, a.Dimension, name)
        {
            this.a = a;
            this.scalar = scalar;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => a.Output + scalar;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { error };
    }
}