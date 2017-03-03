using System;

namespace Fuzzy
{
    public delegate double BinaryOperation(double x1, double x2);
    public delegate double UnaryOperation(double x);

    public static class Operations
    {
        public static IFuzzySet Of(this UnaryOperation f, IFuzzySet set)
        {
            var newSet = new MutableFuzzySet(set.Domain);
            foreach (var e in set.Domain)
                newSet.SetMembership(e, f(set.Membership(e)));
            return newSet;
        }

        public static IFuzzySet Of(this BinaryOperation f, IFuzzySet set1, IFuzzySet set2)
        {
            if (!set1.Domain.Equals(set2.Domain))
                throw new NotImplementedException("Operation between two sets with " +
                                                  "different domains not implemented.");
            var newSet = new MutableFuzzySet(set1.Domain);
            foreach (var e in set1.Domain)
                newSet.SetMembership(e, f(set1.Membership(e), set2.Membership(e)));
            return newSet;
        }

        public static IFuzzySet Outer(this BinaryOperation f, IFuzzySet set1, IFuzzySet set2)
        {
            var newSet = new MutableFuzzySet(Domain.Combine(set1.Domain, set2.Domain));
            foreach (var e1 in set1.Domain)
                foreach (var e2 in set2.Domain)
                    newSet.SetMembership(DomainElement.Of(e1, e2), f(set1.Membership(e1), set2.Membership(e2)));
            return newSet;
        }

        public static double MultiArg(this BinaryOperation f, params double[] a)
        {
            double result;
            result = f(a[0], a[1]);
            for (int i = 2; i < a.Length; i++)
                result = f(result, a[i]);
            return result;
        }

        public static IFuzzySet MultiArg(this BinaryOperation f, params IFuzzySet[] a)
        {
            var result = f.Of(a[0], a[1]);
            for (int i = 2; i < a.Length; i++)
                result = f.Of(result, a[i]);
            return result;
        }

        public static UnaryOperation ZadehNot => a => 1 - a;
        public static BinaryOperation ZadehAnd => Math.Min;
        public static BinaryOperation ZadehOr => Math.Max;

        public static BinaryOperation AlgebraicProduct => (a, b) => a * b;
        public static BinaryOperation AlgebraicSum => (a, b) => a + b - a * b;

        public static BinaryOperation HamacherTNorm(double v) =>
            (a, b) => a * b / (v + (1 - v) * (a + b - a * b));
        public static BinaryOperation HamacherSNorm(double v) =>
            (a, b) => (a + b - (2 - v) * a * b) / (1 - (1 - v) * a * b);

        public static BinaryOperation YagerTNorm(double q) =>
            (a, b) => 1 - Math.Min(1, Math.Pow(Math.Pow(1 - a, q) + Math.Pow(1 - b, q), 1 / q));
        public static BinaryOperation YagerSNorm(double q) =>
            (a, b) => Math.Min(1, Math.Pow(Math.Pow(a, q) + Math.Pow(b, q), 1 / q));
    }
}

