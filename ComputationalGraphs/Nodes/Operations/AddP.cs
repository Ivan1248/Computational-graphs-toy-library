using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class AddP : Node
    {
        private readonly Node x;
        private readonly Vector biases;

        public AddP(Node x, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
            biases = Vector.Zeros(x.Dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => ConsumerInputCharacteristics;

        public override Vector[] Parameters => new[] { biases };

        protected override Vector GetOutputProtected() => x.Output + biases;

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            inputErrors = new[] { error };
            parameterErrors = new[] { error };
        }

        protected override void ReinitializeParametersProtected()
        {
            var cic = ConsumerInputCharacteristics;
            var dev = cic.deviation / Math.Sqrt(x.Dimension) / 4;
            for (int i = 0; i < Dimension; i++)
                biases[i] = Random.NextNormal() * dev + cic.mean;
        }
    }
}