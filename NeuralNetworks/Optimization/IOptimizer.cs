namespace NeuralNetworks.Optimization
{
    public interface IOptimizer<P> where P : IOptimizable<P>
    {
        void UpdateParameters(P parameters, P gradient);
    }
}
