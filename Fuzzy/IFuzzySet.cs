namespace Fuzzy
{
    public interface IFuzzySet
    {
        IDomain Domain { get; }
        double Membership(DomainElement e);
    }
}
