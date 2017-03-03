using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Repeat : ParameterlessNode
    {
        private readonly Node x;
        private readonly int n;

        public Repeat(Node x, int n, string name = null) : base(new[] { x }, x.Dimension * n, name)
        {
            this.x = x;
            this.n = n;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => x.Output.Repeat(n);

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            Vector inputError = error.CopyPart(x.Dimension);
            for (int i = x.Dimension; i < error.Dimension; i++)
                inputError[i % x.Dimension] += error[i];
            inputErrors = new[] { error };
        }
    }
}