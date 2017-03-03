using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Fuzzy
{

    class CompositeDomain : Domain
    {
        private readonly SimpleDomain[] components;
        private readonly int[] digitWeights;

        public CompositeDomain(SimpleDomain[] simpleDomains)
        {
            components = simpleDomains;
            digitWeights = new int[components.Length];
            int ds = 1;
            for (int i = components.Length - 1; i >= 0; ds *= components[i--].Cardinality)
                digitWeights[i] = ds;
            Cardinality = ds;
        }

        public override IEnumerator<DomainElement> GetEnumerator()
        {
            var iterators = new IEnumerator<DomainElement>[components.Length];
            for (int i = 0; i < components.Length; i++)
                iterators[i] = components[i].GetEnumerator();

            var point = new int[components.Length];
            var en = GeneratePoint(point, 0);
            while (en.MoveNext()) yield return DomainElement.Of(point);
        }

        private IEnumerator<int[]> GeneratePoint(int[] point, int i)
        {
            if (i == ComponentCount) yield return point;
            else
                foreach (var p in components[i])
                {
                    var en = GeneratePoint(point, i + 1);
                    point[i] = p[0];
                    while (en.MoveNext()) yield return point;
                }
        }

        public sealed override int Cardinality { get; }

        public override IDomain GetComponent(int i) => components[i];

        public override int ComponentCount => components.Length;

        public override int IndexOf(DomainElement element)
        {
            int index = 0;
            for (int i = 0; i < ComponentCount; i++)
            {
                int compInd = components[i].IndexOf(DomainElement.Of(element[i]));
                if (compInd == -1) return -1;
                index += compInd * digitWeights[i];
            }
            return index;
        }

        public override DomainElement this[int i]
        {
            get
            {
                if (i >= Cardinality || i < 0) throw new ArgumentOutOfRangeException();
                var point = new int[components.Length];
                for (int k = 0; k < ComponentCount; k++)
                {
                    DomainElement ek = components[k][i / digitWeights[k]];
                    point[k] = ek[0];
                    i -= components[k].IndexOf(ek) * digitWeights[k];
                }
                return DomainElement.Of(point);
            }
        }

        public override bool Equals(IDomain other)
        {
            if (other?.ComponentCount != ComponentCount) return false;
            if (other.Cardinality != Cardinality) return false;
            for (int i = 0; i < ComponentCount; i++)
                if (!components[i].Equals(other.GetComponent(i))) return false;
            return true;
        }
    }
}
