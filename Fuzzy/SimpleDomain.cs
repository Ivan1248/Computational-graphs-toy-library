using System;
using System.Collections.Generic;

namespace Fuzzy
{
    class SimpleDomain : Domain
    {
        public SimpleDomain(int first, int to)
        {
            this.First = first;
            this.Last = to - 1;
        }

        public override IEnumerator<DomainElement> GetEnumerator()
        {
            for (var i = First; i <= Last; i++)
                yield return DomainElement.Of(i);
        }

        public override int Cardinality => Last - First + 1;

        public override IDomain GetComponent(int i) => this;

        public override int ComponentCount => 1;

        public override int IndexOf(DomainElement element)
        {
            if (element.ComponentCount != 1) return -1;
            int index = element[0] - First;
            if (index > Last - First) return -1;
            return index;
        }

        public override DomainElement this[int i]
        {
            get
            {
                var value = First + i;
                if (value > Last) throw new IndexOutOfRangeException();
                return DomainElement.Of(value);
            }
        }

        public override bool Equals(IDomain other) =>
            other?.ComponentCount == 1 && ((SimpleDomain)other).First == First && ((SimpleDomain)other).Last == Last;

        public int First { get; }
        public int Last { get; }
    }
}
