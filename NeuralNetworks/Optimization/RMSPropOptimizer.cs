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
    public class RMSPropOptimizer<P> : IOptimizer<P> where P : IOptimizable<P>
    {
        private double learningRate;
        private double gamma;

        private double meanNormSqr = 0;

        public RMSPropOptimizer(double learningRate = 0.001, double gamma = 0.9)
        {
            this.learningRate = learningRate;
            this.gamma = gamma;
        }

        public void UpdateParameters(P parameters, P gradient)
        {
            meanNormSqr = gamma * meanNormSqr + (1 - gamma) * gradient.NormSquare();
            parameters.AddWeighted(gradient.Copy(), -learningRate / (Math.Sqrt(meanNormSqr) + 1e-10));
        }
    }
}
