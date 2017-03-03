using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public static class Evaluation
    {
        public static double MeanError(this NeuralNetwork ann, IList<Vector> x, IList<Vector> y)
        {
            double error = 0;
            for (int i = 0; i < x.Count; i++)
                error += ann.LossFunction.Function(ann.Predict(x[i]), y[i]);
            return error / x.Count;
        }
    }
}
