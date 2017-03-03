using System.Text;

namespace Fuzzy
{
    public abstract class FuzzySet : IFuzzySet
    {
        public IDomain Domain { get; }

        protected FuzzySet(IDomain domain)
        {
            Domain = domain;
        }

        public abstract double Membership(DomainElement e);

        public override string ToString()
        {
            var sb = new StringBuilder(Domain.Cardinality * Domain.ComponentCount * 2);
            sb.AppendLine("FuzzySet:");
            foreach (var p in Domain) sb.AppendLine($"  ({p} {Membership(p)})");
            return sb.ToString();
        }
    }
}
