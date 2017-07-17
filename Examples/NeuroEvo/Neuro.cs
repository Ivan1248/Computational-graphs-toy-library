using System.Linq;
using ComputationalGraphs;

namespace NeuroEvo
{
    public static class Neuro
    {
        public static Node CentroidLayer(this Node x, int dimension)
        {
            x = x.Repeat(dimension).Add().Mul().Abs();
            var sums = from i in Enumerable.Range(0, dimension) let k = i * 2 select x.Slice(k, k + 2).ReduceSum();
            return 1 / (1 + Nodes.Concat(sums));
        }

        public static Node NeuronLayer(this Node x, int dimension) => x.WeightedSums(dimension, true).Logistic();

        public static Node NeuralNetwork(this Node input, int centroidsDimension, params int[] neuronsDimensions)
        {
            input = input.CentroidLayer(centroidsDimension);
            return neuronsDimensions.Aggregate(input, (current, d) => current.NeuronLayer(d));
        }

        public static Node NeuronLayerP(this Node x, int dimension) => x.PositiveLinearMap(dimension).Add().Logistic();

        public static Node NeuralNetworkP(this Node input, int centroidsDimension, params int[] neuronsDimensions)
        {
            input = input.CentroidLayer(centroidsDimension);
            return neuronsDimensions.Aggregate(input, (current, d) => current.NeuronLayerP(d));
        }
    }
}