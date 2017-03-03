using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy;

namespace FuzzyShipControlSystem
{
    public class FuzzyEncoder<T> : IFuzzyEncoder<T>
    {
        private readonly IDomain domain;
        private readonly Func<T, double>[] fuzzifiers;

        public FuzzyEncoder(IDomain domain, Func<T, double>[] fuzzifiers)
        {
            if (fuzzifiers.Length != domain.Cardinality) throw new ArgumentException();
            this.domain = domain;
            this.fuzzifiers = fuzzifiers;
        }

        public IFuzzySet Encode(T value)
        {
            var fs = new MutableFuzzySet(domain);
            for (int i = 0; i < domain.Cardinality; i++)
                fs.SetMembership(domain[i], fuzzifiers[i](value));
            return fs;
        }
    }
}
