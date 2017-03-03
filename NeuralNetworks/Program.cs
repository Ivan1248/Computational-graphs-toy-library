using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    class Program
    {
        // TODO?: batch normalization
        static void Main(string[] args)
        {
            var ann = new NeuralNetwork(
                inputDimension: 3,
                activationLayers: new ActivationLayer[] {new TanhLayer(4), new TanhLayer(3), new LinearLayer(2)},
                optimizer: new GradientDescentOptimizer<ParameterContainer>(0.1),
                lossFunction: LossFunctions.SquaredLoss);
        }
    }
}
