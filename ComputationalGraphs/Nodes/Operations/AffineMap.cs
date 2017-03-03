using System;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class AffineMap : LinearMap
    {
        private readonly Vector biases;

        public AffineMap(Node x, int dimension, string name = null) : base(x, dimension, name)
        {
            biases = Vector.Zeros(dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => ConsumerInputCharacteristics;

        protected override Vector GetOutputProtected() => base.GetOutputProtected().Add(biases);

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            base.GetErrorGradients(error, out inputErrors, out parameterErrors);
            parameterErrors = new[] { parameterErrors[0], error };
        }

        public override Vector[] Parameters => new[] { weights.AsVector, biases };

        protected override void ReinitializeParametersProtected()
        {
            var cic = ConsumerInputCharacteristics;
            var dev = cic.deviation / Math.Sqrt(x.Dimension) / 4;
            for (int r = 0; r < weights.RowCount; r++)
                for (int c = 0; c < weights.ColCount; c++)
                    weights[r, c] = Random.NextNormal() * dev; // TODO
            for (int i = 0; i < Dimension; i++)
                biases[i] = Random.NextNormal() * dev + cic.mean;
        }
    }
}