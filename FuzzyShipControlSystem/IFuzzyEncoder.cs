using Fuzzy;

namespace FuzzyShipControlSystem
{
    public interface IFuzzyEncoder<T>
    {
        IFuzzySet Encode(T value);
    }
}