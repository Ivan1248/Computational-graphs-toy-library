using System;
using System.Collections.Generic;

namespace Fuzzy
{
    public interface IDomain : IEnumerable<DomainElement>, IEquatable<IDomain>
    {
        int Cardinality { get; }
        IDomain GetComponent(int i);
        int ComponentCount { get; }
        int IndexOf(DomainElement element);
        DomainElement this[int i] { get; }
    }
}
