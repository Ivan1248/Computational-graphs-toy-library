using System;
using System.Collections.Generic;
using EvoAlg;
using LinAlg;

namespace NeuroEvo
{
    public static class ListGenOps
    {
        private static readonly Random Random = new Random();

        public static Mutator<IList<T>> Mutator<T>(Mutator<T> elementMutator) =>
            x =>
            {
                var mutant = new List<T>(x.Count);
                foreach (var v in x) mutant.Add(elementMutator(v));
                return mutant;
            };

        public static Mutator<IList<T>> CombinedMutator<T>(double p1,
            Mutator<IList<T>> elementMutator1, Mutator<IList<T>> elementMutator2) =>
            x => Random.NextDouble() <= p1 ? elementMutator1(x) : elementMutator2(x);

        public static Mutator<IList<T>> ElementwiseCombinedMutator<T>(double p1,
            Mutator<T> elementMutator1, Mutator<T> elementMutator2) =>
            x =>
            {
                var mutant = new List<T>(x.Count);
                foreach (var v in x)
                    mutant.Add((Random.NextDouble() <= p1 ? elementMutator1 : elementMutator2)(v));
                return mutant;
            };

        public static Crossoverator<IList<T>> Crossoverator<T>(Crossoverator<T> elementCrossoverator) =>
            p =>
            {
                var mutant = new List<T>(p[0].Count);
                for (int i = 0; i < p[0].Count; i++)
                {
                    var alleles = new List<T>(p.Length);
                    foreach (var gene in p)
                        alleles.Add(gene[i]);
                    mutant.Add(elementCrossoverator(alleles.ToArray()));
                }
                return mutant;
            };

        public static Crossoverator<IList<T>> CombinedCrossoverator<T>(double p1,
            Crossoverator<IList<T>> elementCrossoverator1, Crossoverator<IList<T>> elementCrossoverator2) =>
            x => Random.NextDouble() <= p1 ? elementCrossoverator1(x) : elementCrossoverator2(x);
    }
}