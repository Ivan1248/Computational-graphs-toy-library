using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class PowP : Node
    {
        private readonly Node x;
        private Vector p;

        public PowP(Node x, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
            p = Vector.Zeros(Dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected()
        {
            Vector input = x.Output, result = Vector.Zeros(Dimension);
            for (int i = 0; i < Dimension; i++)
                result[i] = Math.Pow(input[i], p[i] + 1);
            return result;
        }

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            inputErrors = new[] { (Output / x.Output).Mul(p).Mul(error) };
            parameterErrors = new[] { x.Output.Mapped(Math.Log).Mul(Output).Mul(error) };
        }

        public override Vector[] Parameters => new[] { p };
        protected override void ReinitializeParametersProtected() { for (int i = 0; i < Dimension; i++) p[i] = 0; }
    }
}