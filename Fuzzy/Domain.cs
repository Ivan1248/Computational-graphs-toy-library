using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Fuzzy
{
    public abstract class Domain : IDomain
    {
        public abstract IEnumerator<DomainElement> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public abstract int Cardinality { get; }

        public abstract IDomain GetComponent(int i);

        public abstract int ComponentCount { get; }

        public abstract int IndexOf(DomainElement element);

        public abstract DomainElement this[int i] { get; }

        public static IDomain IntRange(int from, int to) => new SimpleDomain(from, to);

        public static IDomain Combine(IDomain d1, IDomain d2)
        {
            var components = new SimpleDomain[d1.ComponentCount + d2.ComponentCount];
            for (int i = 0; i < d1.ComponentCount; i++)
                components[i] = (SimpleDomain)d1.GetComponent(i);
            for (int i = 0; i < d2.ComponentCount; i++)
                components[i + d1.ComponentCount] = (SimpleDomain)d2.GetComponent(i);
            return new CompositeDomain(components);
        }

        public abstract bool Equals(IDomain other);

        public override string ToString()
        {
            var sb = new StringBuilder(this.Cardinality * this.ComponentCount * 2);
            sb.AppendLine($"Domain(card={Cardinality}):");
            foreach (var p in this) sb.Append("  ").AppendLine(p.ToString());
            return sb.ToString();
        }
    }
}
