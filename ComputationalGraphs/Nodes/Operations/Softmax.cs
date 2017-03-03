using System;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Softmax : ParameterlessNode
    {
        private readonly Node x;

        public Softmax(Node x, string name = null) : base(new[] {x}, x.Dimension, name)
        {
            this.x = x;
        }

        public override (double mean, double deviation) InputCharacteristics => (0, 1);

        protected override Vector GetOutputProtected()
        {
            var s = x.Output.Mapped(Math.Exp);
            return s.DivBy(s.Sum());
        }

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            /*double e = 1;
            for (int i = 0; i < Dimension; i++)
                e -= Output[i] * error[i];
            inputErrors = new[] { Output * e };*/

            var fe = error * Output;
            inputErrors = new[] {fe.Sub(fe.Sum()).Mul(Output)};

            // inputErrors = new[] { (1 - Output).Mul(Output).Mul(error) };
        }
    }
}