using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvoAlg
{
    public class GenerationalProportionalGenAlg<T> : GenAlg<T>
    {
        protected int elitism;

        public GenerationalProportionalGenAlg(ErrorFunc<T> errorFunc,
            Generator<T> generate, Mutator<T> mutate, Crossoverator<T> crossover,
            int elitism = 1, int populationSize = 20, int maxIterations = 100000, int maxEvaluations = 100000,
            double targetError = 1E-15, bool printProgress = false)
            : base(errorFunc, generate, mutate, crossover, populationSize, maxIterations, maxEvaluations, targetError, printProgress)
        {
            this.elitism = elitism;
        }

        protected override void SelectNextPopulation()
        {
            var populationFitnessSum = population.Sum(o => o.Fitness);
            Func<T> selectParent = () =>
            {
                var t = Random.NextDouble() * populationFitnessSum;
                foreach (var c in population)
                {
                    t -= c.Fitness;
                    if (t < 0)
                        return c.Value;
                }
                return population[population.Length - 1].Value;
            };
            Func<int, T[]> selectParents = n =>
            {
                var parents = new List<T>(n) { selectParent() };
                for (int i = 1; i < n;)
                {
                    var par = selectParent();
                    if (parents.Any(x => ReferenceEquals(par, x)))
                        continue;
                    parents.Add(par);
                    i++;
                }
                return parents.ToArray();
            };
            var nextPopulation = new Individual<T>[population.Length];
            for (int i = 0; i < elitism; i++) nextPopulation[i] = population[i];
            for (int i = elitism; i < population.Length; i++)
            {
                var c = Crossover(selectParents(2));
                c = Mutate(c);
                var fitness = 1 / (Error(c));
                nextPopulation[i] = new Individual<T> { Value = c, Fitness = fitness };
            }
            population = nextPopulation;
        }
    }
}
