using System;
using System.Collections.Generic;
using System.Diagnostics;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class AddCScal1 : ParameterlessNode
    {
        private readonly Node a;

        public AddCScal1(Node a, string name = null) : base(new[] { a }, a.Dimension, name)
        {
            this.a = a;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => a.Output + 1;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { error };
    }
}