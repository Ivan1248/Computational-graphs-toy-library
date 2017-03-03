using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Neg : ParameterlessNode
    {
        private readonly Node a;

        public Neg(Node a, string name = null) : base(new[] { a }, a.Dimension, name)
        {
            this.a = a;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => -a.Output;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { -error };
    }
}