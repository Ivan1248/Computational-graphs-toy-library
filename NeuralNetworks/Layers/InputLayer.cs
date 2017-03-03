using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public class InputLayer : ActivationLayer
    {
        private double mean, deviation;
        public InputLayer(int dimension, double mean, double deviation) : base(dimension)
        {
            this.mean = mean;
            this.deviation = deviation;
        }

        public override double PreferredInputMean => mean;
        public override double PreferredInputDeviation => deviation;
        public override double OutputMean => mean;
        public override double OutputDeviation => deviation;

        public override Vector PropagateForward(Vector s)
        {
            Out = s;
            return s;
        }
    }
}
