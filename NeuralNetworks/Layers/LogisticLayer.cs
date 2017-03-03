using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public class LogisticLayer : ActivationLayer
    {
        public LogisticLayer(int dimension) : base(dimension) { }

        public override double PreferredInputMean => 0;
        public override double PreferredInputDeviation => 4;
        public override double OutputMean => 0.5;
        public override double OutputDeviation => 0.5;

        public override Vector PropagateForward(Vector s)
        {
            Out = ActivationFunctions.LogisticFunction.Map(s);
            if (TrainMode) Gradient = (1 - Out).Mul(Out);
            return Out;
        }
    }
}
