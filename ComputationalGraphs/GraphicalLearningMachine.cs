using System.Collections.Generic;
using LinAlg;

namespace ComputationalGraphs
{
    public class GraphicalLearningMachine
    {
        private readonly Trainer _trainer;

        public GraphicalLearningMachine(Trainer trainer)
        {
            _trainer = trainer;
        }

        public void ReinitializeParameters() => _trainer.OutputNode.ReinitializeParameters();

        public Vector Predict(Vector x)
        {
            _trainer.InputNode.Set(x);
            return _trainer.OutputNode.Output;
        }

        public void TrainEpoch(IList<Vector> X, IList<Vector> Y, bool shuffle = true)
        {
            _trainer.SetData(X, Y);
            _trainer.Shuffle = shuffle;
            _trainer.TrainEpoch();
        }

        public double GetError(IList<Vector> X, IList<Vector> Y) => _trainer.GetError();
    }
}