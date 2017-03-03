using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Exp : ParameterlessNode
    {
        private readonly Node x;

        public Exp(Node x, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => x.Output.Mapped(Math.Exp);

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] {Output * error};
    }
}