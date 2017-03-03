using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public class Trainer : ITrainer
    {
        public readonly InputNode InputNode;
        public readonly Node OutputNode;
        public readonly InputNode LabelNode;
        public readonly LossNode LossNode;
        private readonly IList<Optimizer> optimizers;
        private readonly IList<IList<Node>> nodeGroups;
        private IList<Vector> X, Y;
        private readonly int _batchSize;
        public int BatchSize => _batchSize == -1 ? X.Count : _batchSize;
        public bool Shuffle;
        public int Iteration = 1;

        public Trainer(InputNode inputNode, InputNode labelNode, LossNode lossNode, IList<Optimizer> optimizers,
            IList<IList<Node>> nodeGroups,
            IList<Vector> X = null, IList<Vector> Y = null, int batchSize = -1, bool shuffle = true)
        {
            this.InputNode = inputNode;
            this.OutputNode = lossNode.Producers.ToArray()[0];
            this.LabelNode = labelNode;
            this.LossNode = lossNode;
            this.optimizers = optimizers;
            this.nodeGroups = optimizers.Count == 1 && nodeGroups[0] == null
                ? new List<IList<Node>> { lossNode.CollectPreceedingNodes() }
                : nodeGroups;
            this.nodeGroups.DoElementwise(optimizers, (ng, o) => o.SetParameters(ng.GetParameters()));
            this.X = X ?? new List<Vector>();
            this.Y = Y ?? new List<Vector>();
            this._batchSize = batchSize;
            this.Shuffle = shuffle;
        }

        public Trainer(InputNode inputNode, InputNode labelNode, LossNode lossNode, Optimizer optimizer,
            IList<Node> nodes = null,
            IList<Vector> X = null, IList<Vector> Y = null, int batchSize = -1, bool shuffle = true)
            : this(inputNode, labelNode, lossNode, new[] { optimizer },
                   new[] { nodes ?? lossNode.CollectPreceedingNodes() }, X, Y, batchSize, shuffle)
        {
        }

        public void SetData(IList<Vector> X, IList<Vector> Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void TrainEpoch()
        {
            if (BatchSize == 0) return;
            IList<Vector> Xs, Ys;
            if (Shuffle)
            {
                Xs = new Vector[X.Count];
                X.CopyTo((Vector[])Xs, 0);
                Ys = new Vector[Y.Count];
                Y.CopyTo((Vector[])Ys, 0);
                RandomExtensions.Shuffle((Vector[])Xs, (Vector[])Ys);
            }
            else
            {
                Xs = X;
                Ys = Y;
            }

            void TrainMinibatch(int from, int to)
            {
                var batchGradients = new IList<Vector>[optimizers.Count];
                for (int i = from; i < to; i++)
                {
                    InputNode.Set(Xs[i]);
                    LabelNode.Set(Ys[i]);
                    for (var j = 0; j < nodeGroups.Count; j++)
                    {
                        var gradient = nodeGroups[j].GetParameterGradients();
                        if (batchGradients[j] == null) batchGradients[j] = gradient;
                        else batchGradients[j].DoElementwise(gradient, (a, d) => a.Add(d));
                    }
                }
                optimizers.DoElementwise(batchGradients, (o, g) => o.UpdateParameters(g, 1.0 / (to - from)));
            }

            int b = 0;
            while (b <= Xs.Count - BatchSize)
                TrainMinibatch(b, b += BatchSize);
            if (b != Xs.Count)
                TrainMinibatch(b, Xs.Count);
        }

        public double GetError()
        {
            double error = 0;
            for (int i = 0; i < X.Count; i++)
            {
                InputNode.Set(X[i]);
                LabelNode.Set(Y[i]);
                error += LossNode.Output[0];
            }
            return error / X.Count;
        }
    }
}