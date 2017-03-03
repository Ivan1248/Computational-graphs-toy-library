using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public class LinearLayer : ActivationLayer
    {
        public LinearLayer(int dimension) : base(dimension) { }

        public override double PreferredInputMean => 0;
        public override double PreferredInputDeviation => 1;
        public override double OutputMean => 0;
        public override double OutputDeviation => 1;

        public override Vector PropagateForward(Vector s)
        {
            Out = s;
            if (TrainMode) Gradient = Vector.Ones(Out.Dimension);
            return Out;
        }
    }
}
