using System.Linq;
using LinAlg;

namespace ComputationalGraphs
{
    public class Concat : ParameterlessNode
    {
        private int[] inputDimensionSums;
        public Concat(string name = null, params Node[] nodes) : base(nodes.ToArray(), nodes.Sum(node => node.Dimension), name)
        {
            inputDimensionSums = new int[nodes.Length];
            inputDimensionSums[0] = nodes[0].Dimension;
            for (int i = 1; i < nodes.Length; i++)
                inputDimensionSums[i] = inputDimensionSums[i - 1] + nodes[i].Dimension;
        }

        public Concat(params Node[] nodes) : this(null, nodes) { }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected()
        {
            var r = Vector.Zeros(Dimension);
            int i = 0;
            foreach (var p in Producers)
            {
                var v = p.Output;
                for (int k = 0; k < v.Dimension; k++)
                    r[i++] = v[k];
            }
            return r;
        }

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            inputErrors = new Vector[Producers.Count];
            int start = 0;
            for (int p = 0; p < Producers.Count; p++)
            {
                inputErrors[p] = error.Slice(start, inputDimensionSums[p]);
                start = inputDimensionSums[p];
            }
        }
    }
}