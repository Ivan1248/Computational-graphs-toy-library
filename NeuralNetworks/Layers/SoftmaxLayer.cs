using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public class SoftmaxLayer : ActivationLayer
    {
        public SoftmaxLayer(int dimension) : base(dimension) { }

        public override double PreferredInputMean => 0.5;
        public override double PreferredInputDeviation => 2;
        public override double OutputMean => 1.0 / Dimension;
        public override double OutputDeviation => 0.5;

        public override Vector PropagateForward(Vector s)
        {
            Out = ActivationFunctions.Exp.Map(s);
            Out.DivBy(Out.Sum());
            if (TrainMode)
            {
                /*Gradient = (Out - 1).Mul(Out);*/

                Gradient = Out.Copy();
                /*double outSum = Out.Sum();
                Gradient.Sub(Out*outSum);*/


                /*for (int i = 0; i < Dimension; i++)
                {
                    double o = 0;
                    for (int j = 0; j < Dimension; j++)
                        o += Out[j];
                    Gradient[i] -= Out[i] * o;
                }*/


                /*Gradient = (1 - Out).Mul(Out);
                for (int i = 0; i < Dimension; i++)
                {
                    var o = -Out[i];
                    for (int j = 0; j < Dimension; j++)
                        o += Out[j];
                    Gradient.Sub(o * Out[i]);
                }*/
            }
            return Out;
        }
    }
}
