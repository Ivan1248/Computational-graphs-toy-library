using System;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class WeightedAverages : Node
    {
        protected readonly Node x;
        protected readonly Matrix weights; // out×in
        protected bool needToCheckWeights = false;

        public WeightedAverages(Node x, int dimension, string name = null) : base(new[] { x }, dimension, name)
        {
            this.x = x;
            weights = Matrix.Zeros(dimension, x.Dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => ConsumerInputCharacteristics;

        protected override Vector GetOutputProtected()
        {
            Vector weightSums = Vector.Zeros(Dimension);
            if (needToCheckWeights)
            {
                for (int i = 0; i < Dimension; i++)
                    for (int j = 0; j < x.Dimension; j++)
                        if (weights[i, j] < 0) weights[i, j] = 0;
                        else weightSums[i] += weights[i, j];
                for (int i = 0; i < Dimension; i++)
                    for (int j = 0; j < x.Dimension; j++)
                        weights[i, j] /= weightSums[i];
                needToCheckWeights = false;
            }
            return weights.MatMul(x.Output);
        }

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            inputErrors = new[] { weights.Tv.MatMul(error) };
            parameterErrors = new[] { error.OutMul(x.Output).AsVector };
            needToCheckWeights = true;
        }

        public override Vector[] Parameters => new[] { weights.AsVector };

        protected override void ReinitializeParametersProtected()
        {
            var cic = ConsumerInputCharacteristics;
            var dev = cic.deviation / Math.Sqrt(x.Dimension) / 4;
            for (int r = 0; r < weights.RowCount; r++)
                for (int c = 0; c < weights.ColCount; c++)
                    weights[r, c] = Math.Abs(Random.NextNormal() * dev); // TODO
        }
    }
}