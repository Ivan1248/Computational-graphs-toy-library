using System;
using System.Diagnostics;
using Fuzzy;

namespace FuzzyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(TestDomains);
            Run(TestSets);
            Run(TestOperations);

            Run(TestRelationProperties);
            Run(TestCompositionOfBinaryRelations);
            Run(TestFuzzyEquivalenceRelation);
        }

        static void Run(Action action)
        {
            Console.Clear();
            action();
            Console.ReadLine();
        }

        static void TestDomains()
        {
            IDomain d1 = Domain.IntRange(0, 5); // {0,1,2,3,4}
            Console.WriteLine(d1);
            IDomain d2 = Domain.IntRange(0, 3); // {0,1,2}
            Console.WriteLine(d2);
            IDomain d3 = Domain.Combine(d1, d2);
            Console.WriteLine(d3);
            Console.WriteLine(d3);
            Console.WriteLine(d3[0]);
            Console.WriteLine(d3[5]);
            Console.WriteLine(d3[14]);
            Console.WriteLine(d3.IndexOf(DomainElement.Of(4, 1)));
        }

        static void TestSets()
        {
            IDomain d = Domain.IntRange(0, 11); // {0,1,...,10}
            d = Domain.Combine(d, d);
            IFuzzySet set1 = new MutableFuzzySet(d)
                .SetMembership(DomainElement.Of(0, 0), 0.7)
                .SetMembership(DomainElement.Of(1, 1), 0.8)
                .SetMembership(DomainElement.Of(2, 2), 0.6)
                .SetMembership(DomainElement.Of(3, 3), 0.4)
                .SetMembership(DomainElement.Of(4, 4), 0.2);
            Console.WriteLine($"Set1:{set1}");
            IDomain d2 = Domain.IntRange(-5, 6); // {-5,-4,...,4,5}
            IFuzzySet set2 = new CalculatedFuzzySet(d2,
                StandardFuzzySets.LambdaFunction(
                    d2.IndexOf(DomainElement.Of(-4)),
                    d2.IndexOf(DomainElement.Of(0)),
                    d2.IndexOf(DomainElement.Of(4))
                    )
                );
            Console.WriteLine($"Set2:{set2}");
        }

        static void TestOperations()
        {
            IDomain d = Domain.IntRange(0, 11);
            IFuzzySet set1 = new MutableFuzzySet(d)
                .SetMembership(DomainElement.Of(0), 0.7)
                .SetMembership(DomainElement.Of(1), 0.8)
                .SetMembership(DomainElement.Of(2), 0.6)
                .SetMembership(DomainElement.Of(3), 0.4)
                .SetMembership(DomainElement.Of(4), 0.2);
            Console.WriteLine($"Set1:{set1}");
            IFuzzySet notSet1 = Operations.ZadehNot.Of(set1);
            Console.WriteLine($"notSet1:{notSet1}");
            IFuzzySet union = Operations.ZadehOr.Of(set1, notSet1);
            Console.WriteLine($"union:{union}");
            IFuzzySet hinters = Operations.HamacherTNorm(1.0).Of(set1, notSet1);
            Console.WriteLine("Set1 intersection with notSet1 using parameterised " +
                              $"Hamacher T norm with parameter 1.0:{hinters}");
        }

        static void TestRelationProperties()
        {
            IDomain u = Domain.IntRange(1, 6); // {1,2,3,4,5}
            IDomain u2 = Domain.Combine(u, u);
            IFuzzySet r1 = new MutableFuzzySet(u2)
                .SetMembership(DomainElement.Of(1, 1), 1)
                .SetMembership(DomainElement.Of(2, 2), 1)
                .SetMembership(DomainElement.Of(3, 3), 1)
                .SetMembership(DomainElement.Of(4, 4), 1)
                .SetMembership(DomainElement.Of(5, 5), 1)
                .SetMembership(DomainElement.Of(3, 1), 0.5)
                .SetMembership(DomainElement.Of(1, 3), 0.5);
            IFuzzySet r2 = new MutableFuzzySet(u2)
                .SetMembership(DomainElement.Of(1, 1), 1)
                .SetMembership(DomainElement.Of(2, 2), 1)
                .SetMembership(DomainElement.Of(3, 3), 1)
                .SetMembership(DomainElement.Of(4, 4), 1)
                .SetMembership(DomainElement.Of(5, 5), 1)
                .SetMembership(DomainElement.Of(3, 1), 0.5)
                .SetMembership(DomainElement.Of(1, 3), 0.1);
            IFuzzySet r3 = new MutableFuzzySet(u2)
                .SetMembership(DomainElement.Of(1, 1), 1)
                .SetMembership(DomainElement.Of(2, 2), 1)
                .SetMembership(DomainElement.Of(3, 3), 0.3)
                .SetMembership(DomainElement.Of(4, 4), 1)
                .SetMembership(DomainElement.Of(5, 5), 1)
                .SetMembership(DomainElement.Of(1, 2), 0.6)
                .SetMembership(DomainElement.Of(2, 1), 0.6)
                .SetMembership(DomainElement.Of(2, 3), 0.7)
                .SetMembership(DomainElement.Of(3, 2), 0.7)
                .SetMembership(DomainElement.Of(3, 1), 0.5)
                .SetMembership(DomainElement.Of(1, 3), 0.5);
            IFuzzySet r4 = new MutableFuzzySet(u2)
                .SetMembership(DomainElement.Of(1, 1), 1)
                .SetMembership(DomainElement.Of(2, 2), 1)
                .SetMembership(DomainElement.Of(3, 3), 1)
                .SetMembership(DomainElement.Of(4, 4), 1)
                .SetMembership(DomainElement.Of(5, 5), 1)
                .SetMembership(DomainElement.Of(1, 2), 0.4)
                .SetMembership(DomainElement.Of(2, 1), 0.4)
                .SetMembership(DomainElement.Of(2, 3), 0.5)
                .SetMembership(DomainElement.Of(3, 2), 0.5)
                .SetMembership(DomainElement.Of(1, 3), 0.4)
                .SetMembership(DomainElement.Of(3, 1), 0.4);
            bool test1 = r1.IsUTimesURelation();
            Console.WriteLine("r1 je definiran nad UxU? " + test1);
            bool test2 = r1.IsSymmetric();
            Console.WriteLine("r1 je simetrična? " + test2);
            bool test3 = r2.IsSymmetric();
            Console.WriteLine("r2 je simetrična? " + test3);
            bool test4 = r1.IsReflexive();
            Console.WriteLine("r1 je refleksivna? " + test4);
            bool test5 = r3.IsReflexive();
            Console.WriteLine("r3 je refleksivna? " + test5);
            bool test6 = r3.IsMaxMinTransitive();
            Console.WriteLine("r3 je max-min tranzitivna? " + test6);
            bool test7 = r4.IsMaxMinTransitive();
            Console.WriteLine("r4 je max-min tranzitivna? " + test7);
        }

        static void TestCompositionOfBinaryRelations()
        {
            IDomain u1 = Domain.IntRange(1, 5); // {1,2,3,4}
            IDomain u2 = Domain.IntRange(1, 4); // {1,2,3}
            IDomain u3 = Domain.IntRange(1, 5); // {1,2,3,4}
            IFuzzySet r1 = new MutableFuzzySet(Domain.Combine(u1, u2))
                .SetMembership(DomainElement.Of(1, 1), 0.3)
                .SetMembership(DomainElement.Of(1, 2), 1)
                .SetMembership(DomainElement.Of(3, 3), 0.5)
                .SetMembership(DomainElement.Of(4, 3), 0.5);
            IFuzzySet r2 = new MutableFuzzySet(Domain.Combine(u2, u3))
                .SetMembership(DomainElement.Of(1, 1), 1)
                .SetMembership(DomainElement.Of(2, 1), 0.5)
                .SetMembership(DomainElement.Of(2, 2), 0.7)
                .SetMembership(DomainElement.Of(3, 3), 1)
                .SetMembership(DomainElement.Of(3, 4), 0.4);
            IFuzzySet r1r2 = Relations.CompositionOfBinaryRelations(r1, r2);
            foreach (DomainElement e in r1r2.Domain)
                Console.WriteLine("mu(" + e + ")=" + r1r2.Membership(e));
        }

        static void TestFuzzyEquivalenceRelation()
        {
            IDomain u = Domain.IntRange(1, 5); // {1,2,3,4}
            IFuzzySet r = new MutableFuzzySet(Domain.Combine(u, u))
                .SetMembership(DomainElement.Of(1, 1), 1)
                .SetMembership(DomainElement.Of(2, 2), 1)
                .SetMembership(DomainElement.Of(3, 3), 1)
                .SetMembership(DomainElement.Of(4, 4), 1)
                .SetMembership(DomainElement.Of(1, 2), 0.3)
                .SetMembership(DomainElement.Of(2, 1), 0.3)
                .SetMembership(DomainElement.Of(2, 3), 0.5)
                .SetMembership(DomainElement.Of(3, 2), 0.5)
                .SetMembership(DomainElement.Of(3, 4), 0.2)
                .SetMembership(DomainElement.Of(4, 3), 0.2);
            IFuzzySet r2 = r;
            Console.WriteLine(
            "Početna relacija je neizrazita relacija ekvivalencije? " +
            r2.IsFuzzyEquivalence());
            Console.WriteLine();
            for (int i = 1; i <= 3; i++)
            {
                r2 = Relations.CompositionOfBinaryRelations(r2, r);
                Console.WriteLine(
                "Broj odrađenih kompozicija: " + i + ". Relacija je:");
                foreach (DomainElement e in r2.Domain)
                    Console.WriteLine("mu(" + e + ")=" + r2.Membership(e));
                Console.WriteLine(
                "Ova relacija je neizrazita relacija ekvivalencije? " +
                r2.IsFuzzyEquivalence());
                Console.WriteLine();
            }
        }
    }
}
