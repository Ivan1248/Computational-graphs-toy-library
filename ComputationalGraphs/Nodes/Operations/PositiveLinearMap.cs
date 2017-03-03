using System;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class PositiveLinearMap : Node
    {
        protected readonly Node x;
        protected readonly Matrix weights; // out×in
        protected bool validateWeights = false;

        public PositiveLinearMap(Node x, int dimension, string name = null) : base(new[] { x }, dimension, name)
        {
            this.x = x;
            weights = Matrix.Zeros(dimension, x.Dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => ConsumerInputCharacteristics;

        protected override Vector GetOutputProtected()
        {
            //if (validateWeights)
            {
                //validateWeights = false;
                for (int i = 0; i < weights.RowCount; i++)
                    for (int j = 0; j < weights.ColCount; j++)
                        if (weights[i, j] < 0) weights[i, j] = 0;
            }
            return weights.MatMul(x.Output);
        }

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            inputErrors = new[] { weights.Tv.MatMul(error) };
            parameterErrors = new[] { error.OutMul(x.Output).AsVector };
            validateWeights = true;
        }

        public override Vector[] Parameters => new[] { weights.AsVector };

        protected override void ReinitializeParametersProtected()
        {
            var cic = ConsumerInputCharacteristics;
            var dev = cic.deviation / Math.Sqrt(x.Dimension) / 4;
            for (int r = 0; r < weights.RowCount; r++)
                for (int c = 0; c < weights.ColCount; c++)
                    weights[r, c] = Math.Abs(Random.NextNormal()) * dev; // TODO
        }
    }
}