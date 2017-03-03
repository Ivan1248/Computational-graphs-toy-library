using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public class LeakyRectifierLayer : ActivationLayer
    {
        public LeakyRectifierLayer(int dimension) : base(dimension) { }

        public override double PreferredInputMean => 1;
        public override double PreferredInputDeviation => 1;
        public override double OutputMean => 1;
        public override double OutputDeviation => 1;

        public override Vector PropagateForward(Vector s)
        {
            Out = ((Func<double, double>)(x => x > 0 ? x : 0)).Map(s);
            if (TrainMode) Gradient = ((Func<double, double>)(x => x > 0 ? 1 : 0.001)).Map(s);
            return Out;
        }
    }
}
