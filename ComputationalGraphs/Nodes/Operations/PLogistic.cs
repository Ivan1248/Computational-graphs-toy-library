using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class PLogistic : Node
    {
        private readonly Node x;
        private readonly Vector thresholds, steepnesses;

        public PLogistic(Node x, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
            thresholds = Vector.Zeros(x.Dimension);
            steepnesses = Vector.Ones(x.Dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 4);

        public override Vector[] Parameters => new[] { thresholds, steepnesses };

        protected override Vector GetOutputProtected()
        {
            Vector input = x.Output, output = Vector.Zeros(Dimension);
            for (int i = 0; i < Dimension; i++)
                output[i] = 1 / (1 + Math.Exp(steepnesses[i] * (thresholds[i] - input[i])));
            return output;
        }

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            var logisticInputError = (1 - Output).Mul(Output).Mul(error);
            var inputError = logisticInputError * steepnesses;
            inputErrors = new[] { inputError };
            Vector thresholdError = -inputError, steepnessError = logisticInputError.Mul(x.Output - thresholds);
            parameterErrors = new[] { thresholdError, steepnessError };
        }

        protected override void ReinitializeParametersProtected()
        {
            for (int i = 0; i < Dimension; i++)
            {
                thresholds[i] = Random.NextNormal();
                steepnesses[i] = Random.NextNormal();
            }
        }
    }
}