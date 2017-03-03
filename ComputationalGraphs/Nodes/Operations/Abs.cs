using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Abs : ParameterlessNode
    {
        private readonly Node x;

        public Abs(Node x, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => x.Output.Mapped(Math.Abs);

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            var result = Vector.Zeros(Output.Dimension);
            for (int i = 0; i < Output.Dimension; i++)
                result[i] = Output[i] >= 0 ? error[i] : -error[i];
            inputErrors = new[] { result };
        }
    }
}