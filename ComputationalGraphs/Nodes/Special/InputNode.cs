using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace ComputationalGraphs
{
    public class InputNode : ParameterlessNode
    {
        private Vector data;

        public InputNode(int dimension, string name = null) : base(new Node[] {}, dimension, name)
        {
            data = Vector.Zeros(dimension);
        }

        public void Set(Vector data)
        {
            Debug.Assert(data.Dimension == this.Dimension);
            this.data = data;
            SetNeedsEvaluation();
        }

        protected override Vector GetOutputProtected() => data;

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors) =>
            inputErrors = emptyVectorArray;
    }
}