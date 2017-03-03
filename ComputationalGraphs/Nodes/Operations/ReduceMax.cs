using LinAlg;

namespace ComputationalGraphs
{
    public class ReduceMax : ParameterlessNode
    {
        private readonly Node x;
        private int argMax;

        public ReduceMax(Node x, string name = null) : base(new[] { x }, 1, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => Vector.Of(x.Output[argMax = x.Output.ArgMax()]);

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            var inputError = Vector.Zeros(x.Output.Dimension);
            inputError[argMax] = error[0];
            inputErrors = new[] { inputError };
        }
    }
}