using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworks.Optimization;

namespace NeuralNetworks
{
    public class GradientDescentOptimizer<P> : IOptimizer<P> where P : IOptimizable<P>
    {
        private double learningRate;

        public GradientDescentOptimizer(double learningRate)
        {
            this.learningRate = learningRate;
        }

        public void UpdateParameters(P parameters, P gradient) =>
            parameters.AddWeighted(gradient, -learningRate);
    }
}
