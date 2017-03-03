using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class PowCScal : ParameterlessNode
    {
        private readonly Node x;
        private double p;

        public PowCScal(Node x, double p, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
            this.p = p;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => x.Output.Mapped(x => Math.Pow(x, p));

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { (Output / x.Output).Mul(p).Mul(error) };
    }
}