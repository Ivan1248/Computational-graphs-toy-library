using LinAlg;

namespace ComputationalGraphs
{
    public class ReduceSum : ParameterlessNode
    {
        private readonly Node x;

        public ReduceSum(Node x, string name = null) : base(new[] {x}, 1, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected() => Vector.Of(x.Output.Sum());

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = new[] {Vector.Filled(x.Dimension, error[0])};
    }
}