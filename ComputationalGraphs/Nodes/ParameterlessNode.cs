using System.Collections.Generic;
using LinAlg;

namespace ComputationalGraphs
{
    public abstract class ParameterlessNode : Node
    {
        protected ParameterlessNode(Node[] producers, int dimension, string name) : base(producers, dimension, name)
        {
        }

        public override Vector[] Parameters => emptyVectorArray;

        protected override void ReinitializeParametersProtected()
        {
        }

        protected sealed override void GetErrorGradients(Vector error, out Vector[] inputErrors,
            out Vector[] parameterErrors)
        {
            GetErrorGradient(error, out inputErrors);
            parameterErrors = emptyVectorArray;
        }

        protected abstract void GetErrorGradient(Vector error, out Vector[] inputErrors);
    }
}