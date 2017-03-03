using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public partial class Gaussian : ParameterlessNode
    {
        private readonly Node x;

        public Gaussian(Node x, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => x.Output.Mapped(x => Math.Exp(-x * x));

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            inputErrors = new[] {(-2 * x.Output).Mul(Output).Mul(error)};
        }
    }
}