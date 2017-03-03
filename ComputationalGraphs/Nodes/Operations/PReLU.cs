using System;
using System.Collections.Generic;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class PReLU : Node
    {
        private readonly Node x;
        private readonly Vector parameters;

        public PReLU(Node x, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
            this.parameters = Vector.Zeros(x.Dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => (1, 1);

        protected override Vector GetOutputProtected()
        {
            Vector inp = this.x.Output, result = Vector.Zeros(Dimension);
            for (int i = 0; i < Dimension; i++)
                result[i] = inp[i] < 0 ? parameters[i] * inp[i] : inp[i];
            return result;
        }

        public override Vector[] Parameters => new[] {parameters};

        protected override void ReinitializeParametersProtected()
        {
            parameters.Mul(0);
        }

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            var inputError = error.Copy();
            var parameterError = Vector.Zeros(Output.Dimension);
            for (int i = 0; i < Output.Dimension; i++)
                if (Output[i] < 0)
                {
                    inputError[i] *= parameters[i];
                    parameterError[i] = x.Output[i] * error[i];
                }
            inputErrors = new[] {inputError};
            parameterErrors = new[] {parameterError};
        }
    }
}