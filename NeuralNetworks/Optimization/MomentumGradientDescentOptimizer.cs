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
    public class MomentumGradientDescentOptimizer<P> : IOptimizer<P> where P : IOptimizable<P>
    {
        private readonly double learningRate;
        private readonly double momentum;
        private P meanGrad;

        public MomentumGradientDescentOptimizer(double learningRate = 0.01, double momentum = 0.9)
        {
            this.learningRate = learningRate;
            this.momentum = momentum;
        }

        public void UpdateParameters(P parameters, P gradient)
        {
            if (meanGrad == null) meanGrad = gradient.Multiply(0);
            meanGrad.AddWeighted(momentum, gradient, 1 - momentum);
            parameters.AddWeighted(meanGrad, -learningRate);
        }
    }
}
