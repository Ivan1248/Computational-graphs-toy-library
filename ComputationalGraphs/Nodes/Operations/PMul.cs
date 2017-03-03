using System;
using System.Collections.Generic;
using System.Diagnostics;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class PMul : Node
    {
        private readonly Node a;
        private readonly Vector m;

        public PMul(Node a, string name = null) : base(new[] { a }, a.Dimension, name)
        {
            this.a = a;
            this.m = Vector.Ones(a.Dimension);
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected() => a.Output * m;

        protected override void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors)
        {
            inputErrors = new[] { m * error };
            parameterErrors = new[] { a.Output * error };
        }

        public override Vector[] Parameters => new[] { m };
        protected override void ReinitializeParametersProtected()
        {
            for (int i = 0; i < m.Dimension; i++)
                m[i] = Random.NextNormal();
        }
    }
}