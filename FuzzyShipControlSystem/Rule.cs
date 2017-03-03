using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Fuzzy;

namespace FuzzyShipControlSystem
{
    public class Rule
    {
        private IFuzzySet relation;
        private IDomain consequentDomain;

        private Func<DomainElement[]> fetchSingletonAntecedents;  // referenca na varijable (alternativa za dvostruki pokazivač)
        public Rule(BinaryOperation operation, Func<DomainElement[]> fetchSingletonAntecedents, IFuzzySet[] antecedents, IFuzzySet consequent)
        {
            if (antecedents.Any(s => s.Domain.ComponentCount != 1) || consequent.Domain.ComponentCount != 1)
                throw new ArgumentException();
            relation = antecedents[0];
            for (int i = 1; i < antecedents.Length; i++)
                relation = operation.Outer(relation, antecedents[i]);
            relation = operation.Outer(relation, consequent);
            this.fetchSingletonAntecedents = fetchSingletonAntecedents;
            this.consequentDomain = consequent.Domain;
        }

        public IFuzzySet Evaluate()
        {
            var antecendents = fetchSingletonAntecedents();
            MutableFuzzySet conclusion = new MutableFuzzySet(consequentDomain);
            foreach (var ec in consequentDomain)
            {
                var e = DomainElement.Of(DomainElement.Of(antecendents), ec);
                conclusion.SetMembership(ec, relation.Membership(e));
            }
            return conclusion;
        }
    }

}
