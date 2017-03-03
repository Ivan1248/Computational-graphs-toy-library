using System.Collections.Generic;
using LinAlg;

namespace LearningMachine
{
    public static class ITrainableExtensions
    {
        public static IList<Vector> Predict(this ILearningMachine its, IList<Vector> X)
        {
            var result = new List<Vector>(X.Count);
            foreach (var x in X) result.Add(its.Predict(x));
            return result;
        }
    }

    public interface ILearningMachine
    {
        void ReinitializeParameters();

        Vector Predict(Vector x);

        void TrainEpoch(IList<Vector> X, IList<Vector> Y, bool shuffle = true);

        double GetError(IList<Vector> X, IList<Vector> Y);
    }
}
