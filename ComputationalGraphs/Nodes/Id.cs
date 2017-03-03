using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Id : ParameterlessNode
    {
        private readonly Node x;

        public Id(Node x, string name = null) : base(new[] { x }, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => x.Output;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] { error };
    }
}