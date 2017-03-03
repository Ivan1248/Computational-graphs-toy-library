using LinAlg;

namespace ComputationalGraphs
{
    public class Slice : ParameterlessNode
    {
        private readonly Node x;
        private readonly int from, to;

        public Slice(Node x, int from, int to, string name = null) : base(new[] { x }, to - from, name)
        {
            this.x = x;
            this.from = from;
            this.to = to;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => x.Output.Slice(from, to);

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            var r = Vector.Zeros(x.Dimension);
            for (int i = 0; i < to - from; i++)
                r[from + i] = error[i];
            inputErrors = new[] { r };
        }
    }
}