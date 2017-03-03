using System.Collections.Generic;
using LinAlg;

namespace ComputationalGraphs
{
    public interface ITrainer
    {
        void SetData(IList<Vector> X, IList<Vector> Y);
        void TrainEpoch();
        double GetError();
    }
}