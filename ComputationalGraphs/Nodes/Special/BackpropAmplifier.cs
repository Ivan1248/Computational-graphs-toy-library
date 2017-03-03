using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class BackpropAmplifier : ParameterlessNode
    {
        private readonly Node x;
        private readonly double a;

        public BackpropAmplifier(Node x, double a, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
            this.a = a;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => x.Output;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { error * a };
    }
}