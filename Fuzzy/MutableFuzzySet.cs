namespace Fuzzy
{
    public class MutableFuzzySet : FuzzySet
    {
        private double[] mu;

        public MutableFuzzySet(IDomain domain) : base(domain)
        {
            mu = new double[domain.Cardinality];
        }

        public override double Membership(DomainElement e) => mu[Domain.IndexOf(e)];

        public MutableFuzzySet SetMembership(DomainElement e, double value)
        {
            mu[Domain.IndexOf(e)] = value;
            return this;
        }
    }
}
