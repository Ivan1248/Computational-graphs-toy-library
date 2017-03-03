using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public class TanhLayer : ActivationLayer
    {
        public TanhLayer(int dimension) : base(dimension) { }

        public override double PreferredInputMean => 0;
        public override double PreferredInputDeviation => 2;
        public override double OutputMean => 0;
        public override double OutputDeviation => 1;

        public override Vector PropagateForward(Vector s)
        {
            Out = ActivationFunctions.Tanh.Map(s);
            if (TrainMode) Gradient = (Out * Out).SubFrom(1);
            return Out;
        }
    }
}
