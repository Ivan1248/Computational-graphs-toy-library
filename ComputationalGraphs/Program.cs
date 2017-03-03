using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace ComputationalGraphs
{
    class Program
    {
        static void Main(string[] args)
        {
            InputNode inputNode = Nodes.Input(1), labelNode = Nodes.Input(1);
            var annOutputNode = inputNode.WeightedSums(2).Tanh().WeightedSums(1);
            var lossNode = annOutputNode.SquaredLoss(labelNode);
            var trainer = new Trainer(
                inputNode: inputNode,
                labelNode: labelNode,
                lossNode: lossNode,
                optimizer: new GradientDescentOptimizer(0.001),
                X:
                new List<Vector>
                {
                    Vector.Of(-3.0),
                    Vector.Of(-2.0),
                    Vector.Of(0.0),
                    Vector.Of(2.0),
                    Vector.Of(3.0),
                },
                Y:
                new List<Vector> {Vector.Of(9.0), Vector.Of(4.0), Vector.Of(0.0), Vector.Of(4.0), Vector.Of(9.0),}
            );
            double error;
            while ((error = trainer.GetError()) > 1e-6)
            {
                Console.WriteLine(error);
                trainer.TrainEpoch();
            }
        }
    }
}