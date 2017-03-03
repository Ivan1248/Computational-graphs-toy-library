using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Fuzzy
{
    public static class Relations
    {
        public static bool IsUTimesURelation(this IFuzzySet rel) =>
            rel.Domain.ComponentCount == 2 && rel.Domain.GetComponent(0).Equals(rel.Domain.GetComponent(1));

        public static bool IsSymmetric(this IFuzzySet rel)
        {
            if (!rel.IsUTimesURelation()) throw new ArgumentException("The argument is not a U×U relation.");
            IDomain d1 = rel.Domain.GetComponent(0);
            for (int i = 0; i < d1.Cardinality; i++)
            {
                int e1 = d1[i][0];
                for (int j = i + 1; j < d1.Cardinality; j++)
                {
                    int e2 = d1[j][0];
                    if (rel.Membership(DomainElement.Of(e1, e2)) != rel.Membership(DomainElement.Of(e2, e1)))
                        return false;
                }
            }
            return true;
        }

        public static bool IsReflexive(this IFuzzySet rel)
        {
            if (!rel.IsUTimesURelation()) throw new ArgumentException("The given argument is not a U×U relation.");
            var d = rel.Domain.GetComponent(0);
            for (int i = 0; i < d.Cardinality; i++)
            {
                int e = d[i][0];
                if (rel.Membership(DomainElement.Of(e, e)) != 1)
                    return false;
            }
            return true;
        }

        public static bool IsMaxMinTransitive(this IFuzzySet rel)
        {
            if (!rel.IsUTimesURelation()) throw new ArgumentException("The given argument is not a U×U relation.");
            var d = rel.Domain.GetComponent(0);
            for (int i = 0; i < d.Cardinality; i++)
            {
                int x = d[i][0];
                for (int k = 0; k < d.Cardinality; k++)
                {
                    int z = d[k][0];
                    var maxmin = double.MinValue;
                    for (int j = 0; j < d.Cardinality; j++)
                    {
                        int y = d[j][0];
                        var min = Math.Min(rel.Membership(DomainElement.Of(x, y)), rel.Membership(DomainElement.Of(y, z)));  // tu zapinje
                        if (min > maxmin) maxmin = min;
                    }
                    if (rel.Membership(DomainElement.Of(x, z)) < maxmin)
                        return false;
                }
            }
            return true;
        }

        public static bool IsCompatibilityRelation(this IFuzzySet rel) =>
            rel.IsReflexive() && rel.IsSymmetric();

        public static bool IsFuzzyEquivalence(this IFuzzySet rel) =>
            rel.IsCompatibilityRelation() && rel.IsMaxMinTransitive();

        public static IFuzzySet CompositionOfBinaryRelations(IFuzzySet rel1, IFuzzySet rel2)
        {
            IDomain d2;
            if (rel1.Domain.ComponentCount != 2 || rel2.Domain.ComponentCount != 2
                || !(d2 = rel1.Domain.GetComponent(1)).Equals(rel2.Domain.GetComponent(0)))
                throw new ArgumentException("The domains of arguments are not U×V and V×U.");
            IDomain d1 = rel1.Domain.GetComponent(0), d3 = rel2.Domain.GetComponent(1);
            var comp = new MutableFuzzySet(Domain.Combine(d1, d3));
            for (int i = 0; i < d1.Cardinality; i++)
            {
                int x = d1[i][0];
                for (int k = 0; k < d3.Cardinality; k++)
                {
                    int z = d3[k][0];
                    var maxmin = double.MinValue;
                    for (int j = 0; j < d2.Cardinality; j++)
                    {
                        int y = d2[j][0];
                        var min = Math.Min(rel1.Membership(DomainElement.Of(x, y)), rel2.Membership(DomainElement.Of(y, z)));
                        if (min > maxmin) maxmin = min;
                    }
                    comp.SetMembership(DomainElement.Of(x, z), maxmin);
                }
            }
            return comp;
        }

        public static IFuzzySet CompositionOfBinaryRelations(IFuzzySet rel1, IFuzzySet rel2,
            BinaryOperation SNorm, BinaryOperation TNorm)
        {
            IDomain d2;
            if (rel1.Domain.ComponentCount != 2 || rel2.Domain.ComponentCount != 2
                || !(d2 = rel1.Domain.GetComponent(1)).Equals(rel2.Domain.GetComponent(0)))
                throw new ArgumentException("The domains of arguments are not U×V and V×W.");
            IDomain d1 = rel1.Domain.GetComponent(0), d3 = rel2.Domain.GetComponent(1);
            var comp = new MutableFuzzySet(Domain.Combine(d1, d3));
            for (int i = 0; i < d1.Cardinality; i++)
            {
                int x = d1[i][0];
                for (int k = 0; k < d3.Cardinality; k++)
                {
                    int z = d3[k][0];
                    var maxmin = double.MinValue;
                    for (int j = 0; j < d2.Cardinality; j++)
                    {
                        int y = d2[j][0];
                        maxmin = SNorm(maxmin, TNorm(rel1.Membership(DomainElement.Of(x, y)), rel2.Membership(DomainElement.Of(y, z))));
                    }
                    comp.SetMembership(DomainElement.Of(x, z), maxmin);
                }
            }
            return comp;
        }

        public static IFuzzySet CompositionOfUnaryAndBinaryRelations(IFuzzySet rel1, IFuzzySet rel2, BinaryOperation TNorm)
        {
            IDomain d1 = rel1.Domain, d2 = rel2.Domain.GetComponent(1);
            if (rel1.Domain.ComponentCount != 1 || rel2.Domain.ComponentCount != 2
                || !d1.Equals(rel2.Domain.GetComponent(0)))
                throw new ArgumentException("The domains of arguments are not U and U×V.");
            var comp = new MutableFuzzySet(d2);
            for (int j = 0; j < d2.Cardinality; j++)
            {
                int y = d2[j][0];
                var maxmin = double.MinValue;
                for (int i = 0; i < d1.Cardinality; i++)
                {
                    int x = d1[i][0];
                    maxmin = Math.Max(maxmin, TNorm(rel1.Membership(DomainElement.Of(x)), rel2.Membership(DomainElement.Of(x, y))));
                }
                comp.SetMembership(DomainElement.Of(y), maxmin);
            }
            return comp;
        }
    }
}
