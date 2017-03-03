using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class PowCScalM1 : ParameterlessNode
    {
        private readonly Node x;

        public PowCScalM1(Node x, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => x.Output.Mapped(x => 1 / x);

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { (-Output).Mul(Output).Mul(error) };
    }
}