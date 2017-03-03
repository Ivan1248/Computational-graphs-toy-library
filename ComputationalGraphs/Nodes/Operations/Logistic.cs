using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Logistic : ParameterlessNode
    {
        private readonly Node x;

        public Logistic(Node x, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 4);

        protected override Vector GetOutputProtected() => x.Output.Mapped(x => 1 / (1 + Math.Exp(-x)));

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            var inputError = Vector.Zeros(Dimension);
            for (int i = 0; i < Output.Dimension; i++)
                inputError[i] = Output[i] * (1 - Output[i]) * error[i];
            inputErrors = new[] {inputError};
        }
    }
}