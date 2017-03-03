using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Dropout : ParameterlessNode
    {
        private readonly Node x;
        private double rate;
        private Vector outputMask;

        public Dropout(Node x, double dropoutRate, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
            rate = dropoutRate;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected()
        {
            var output = x.Output;
            outputMask = Vector.Ones(output.Dimension);
            for (int i = 0; i < output.Dimension; i++)
                if (Random.NextDouble() < rate) outputMask[i] = 0;
            return output * outputMask;
        }

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] {error * outputMask};
    }
}