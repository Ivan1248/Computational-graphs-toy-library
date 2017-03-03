using LinAlg;

namespace ComputationalGraphs
{
    public class ReduceMin : ParameterlessNode
    {
        private readonly Node x;
        private int argMin;

        public ReduceMin(Node x, string name = null) : base(new[] { x }, 1, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => Vector.Of(x.Output[argMin = x.Output.ArgMin()]);

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            var inputError = Vector.Zeros(x.Output.Dimension);
            inputError[argMin] = error[0];
            inputErrors = new[] { inputError };
        }
    }
}