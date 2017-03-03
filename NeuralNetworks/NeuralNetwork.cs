using System;
using System.Collections.Generic;
using LinAlg;
using NeuralNetworks.Extensions;
using NeuralNetworks.Optimization;
using NeuralNetworks.Utilities;

namespace NeuralNetworks
{
    public class NeuralNetwork
    {
        private ActivationLayer[] ActivationLayers;
        private ActivationLayer InputLayer;

        public int InputDimension => InputLayer.Dimension;

        public Matrix[] W { get; }  // w[l][j,k]: l - layer, j - in, k - out
        public Vector[] B { get; }  // b[l][k]

        private Vector Input;

        private readonly IOptimizer<ParameterContainer> Optimizer;

        public LossFunction LossFunction { get; }

        public int MinibatchSize { get; }

        public NeuralNetwork(int inputDimension, ActivationLayer[] activationLayers,
            IOptimizer<ParameterContainer> optimizer, LossFunction lossFunction, int minibatchSize = -1, double inputMean = 0, double inputDeviation = 1)
        {
            InputLayer = new InputLayer(inputDimension, inputMean, inputDeviation);
            ActivationLayers = activationLayers;
            W = new Matrix[ActivationLayers.Length];
            B = new Vector[ActivationLayers.Length];
            ReinitializeParameters();
            Optimizer = optimizer;
            LossFunction = lossFunction;
            MinibatchSize = minibatchSize;
        }

        public void ReinitializeParameters()
        {
            var rand = new Random();
            ActivationLayer prev = InputLayer;
            for (int l = 0; l < ActivationLayers.Length; l++)
            {
                var curr = ActivationLayers[l];
                W[l] = Matrix.Zeros(curr.Dimension, prev.Dimension);
                B[l] = Vector.Zeros(curr.Dimension);
                //var inDev = curr.PreferredInputDeviation / (Math.Sqrt(prev.Dimension + 1) * prev.OutputDeviation * 4);
                var inDev = curr.PreferredInputDeviation / (Math.Sqrt(prev.Dimension) * 4);
                for (int i = 0; i < W[l].RowCount; i++)
                    for (int j = 0; j < W[l].ColCount; j++)
                        W[l][i, j] = rand.NextNormal() * inDev;
                for (int i = 0; i < B[l].Dimension; i++)
                    B[l][i] = rand.NextNormal() * inDev + curr.PreferredInputMean;
                prev = curr;
            }
        }
        public Vector Predict(Vector x)
        {
            Input = x;
            for (int l = 0; l < ActivationLayers.Length; l++)
                x = ActivationLayers[l].PropagateForward(W[l].MatMul(x).Add(B[l]));
            return x;
        }

        public IList<Vector> Predict(IList<Vector> X)
        {
            var result = new List<Vector>(X.Count);
            foreach (var x in X) result.Add(Predict(x));
            return result;
        }

        private ParameterContainer BackPropagate(Vector OutErrorGradient)
        {
            Matrix[] weightErrors = new Matrix[ActivationLayers.Length];
            Vector[] biasErrors = new Vector[ActivationLayers.Length];
            Vector delta;
            for (int l = ActivationLayers.Length - 1; l >= 1; l--)
            {
                delta = OutErrorGradient * ActivationLayers[l].Gradient;
                biasErrors[l] = OutErrorGradient;
                weightErrors[l] = delta.OutMul(ActivationLayers[l - 1].Out);
                OutErrorGradient = W[l].Tv.MatMul(delta);  // dE/dy[l-1]
            }
            delta = OutErrorGradient * ActivationLayers[0].Gradient;
            biasErrors[0] = delta;
            weightErrors[0] = delta.OutMul(Input);
            return new ParameterContainer { W = weightErrors, B = biasErrors };
        }

        private ParameterContainer ComputeGradient(Vector x, Vector y) =>
            BackPropagate(LossFunction.Gradient(Predict(x), y));

        public void TrainEpoch(IList<Vector> X, IList<Vector> Y, bool shuffle = true)
        {
            if (X.Count == 0) return;

            if (shuffle)
            {
                IList<Vector> X1 = new Vector[X.Count]; X.CopyTo((Vector[])X1, 0); General.Swap(ref X, ref X1);
                IList<Vector> Y1 = new Vector[Y.Count]; Y.CopyTo((Vector[])Y1, 0); General.Swap(ref Y, ref Y1);
                RandomExtensions.Shuffle((Vector[])X, (Vector[])Y);
            }

            foreach (var layer in ActivationLayers) layer.TrainMode = true;

            int minibatchSize = MinibatchSize == -1 ? X.Count : MinibatchSize;
            void TrainMinibatch(int from, int to)
            {
                var errorGradient = ComputeGradient(X[from], Y[from]);
                for (int i = from + 1; i < to; i++)
                    errorGradient.Add(ComputeGradient(X[i], Y[i]));
                Optimizer.UpdateParameters(new ParameterContainer { B = B, W = W }, errorGradient.DivideBy(minibatchSize));
            }

            int b = 0;
            while (b <= X.Count - minibatchSize)
                TrainMinibatch(b, b += minibatchSize);
            if (b != X.Count)
                TrainMinibatch(b, X.Count);

            foreach (var layer in ActivationLayers) layer.TrainMode = false;
        }

        public double GetError(IList<Vector> X, IList<Vector> Y) => this.MeanError(X, Y);
    }
}
