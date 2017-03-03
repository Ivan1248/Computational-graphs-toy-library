using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class TanhLeCun : ParameterlessNode
    {
        private readonly Node x;

        public TanhLeCun(Node x, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => x.Output.Mapped(x => 1.7159 * Math.Tanh(x * (2.0 / 3)));

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            var result = Output.Copy();
            for (int i = 0; i < Output.Dimension; i++)
                result[i] = (1.7159 * 2 / 3) * (1 - result[i] * result[i]) * error[i];
            inputErrors = new[] {result};
        }
    }
}