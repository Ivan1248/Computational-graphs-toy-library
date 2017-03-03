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
    public class MomentumRMSPropOptimizer<P> : IOptimizer<P> where P : IOptimizable<P>
    {
        private double learningRate;
        private double gamma;
        private readonly double momentum;

        //private double p;

        private P meanGrad;
        private double meanNormSqr = 0;

        public MomentumRMSPropOptimizer(double learningRate = 0.0001, double gamma = 0.9, double momentum = 0.99)
        {
            this.learningRate = learningRate;
            this.gamma = gamma;
            this.momentum = momentum;
        }

        public void UpdateParameters(P parameters, P gradient)
        {
            if (meanGrad == null) meanGrad = gradient.Multiply(0);
            meanGrad.AddWeighted(momentum, gradient, 1 - momentum);
            meanNormSqr = gamma * meanNormSqr + (1 - gamma) * meanGrad.NormSquare();
            parameters.AddWeighted(meanGrad, -learningRate /**(Math.Sin(p+=1e-4)+1.1)*// (Math.Sqrt(meanNormSqr) + 1e-10));
        }
    }
}
