using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Min : ParameterlessNode
    {
        private int[] argMins;
        public Min(string name = null, params Node[] nodes) : base(nodes.ToArray(), nodes[0].Dimension, name)
        {
            for (int i = 1; i < nodes.Length; i++)
                Debug.Assert(nodes[i].Dimension == Dimension);
            argMins = new int[Dimension];
        }

        public Min(params Node[] nodes) : this(null, nodes) { }

        public override (double mean, double deviation) InputCharacteristics => (0, 2);

        protected override Vector GetOutputProtected()
        {
            var mins = Vector.Zeros(Dimension);
            for (int d = 0; d < Dimension; d++)
            {
                argMins[d] = 0;
                mins[d] = Producers[0].Output[d];
            }
            for (int p = 1; p < Producers.Count; p++)
            {
                var po = Producers[p].Output;
                for (int i = 0; i < Dimension; i++)
                    if (po[i] < mins[i])
                    {
                        argMins[i] = p;
                        mins[i] = po[i];
                    }
            }
            return mins;
        }

        protected override void GetErrorGradient(Vector error, out Vector[] inputErrors)
        {
            inputErrors = new Vector[Producers.Count];
            for (int p = 0; p < Producers.Count; p++)
                inputErrors[p] = Vector.Zeros(Dimension);
            for (int i = 0; i < Dimension; i++)
                inputErrors[argMins[i]][i] = error[i];
        }
    }
}