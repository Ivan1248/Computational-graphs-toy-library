using System;

namespace Fuzzy
{
    public class CalculatedFuzzySet : FuzzySet
    {
        private readonly Func<int, double> membership;
        public CalculatedFuzzySet(IDomain domain, Func<int, double> membership) : base(domain)
        {
            this.membership = membership;
        }

        public override double Membership(DomainElement e) => membership(Domain.IndexOf(e));
    }
}
