using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class ReLU : ParameterlessNode
    {
        private readonly Node x;

        public ReLU(Node x, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (1, 1);

        protected override Vector GetOutputProtected() => x.Output.Mapped(d => Math.Max(0, d));

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            var result = Vector.Zeros(Output.Dimension);
            for (int i = 0; i < Output.Dimension; i++)
                if (Output[i] > 0) result[i] = error[i];
            inputErrors = new[] {result};
        }
    }
}