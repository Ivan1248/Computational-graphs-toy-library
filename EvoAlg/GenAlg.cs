using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvoAlg
{
    public struct Individual<T>
    {
        public T Value;
        public double Fitness;
        public override string ToString() => $"({Value}, {Fitness})";
    }

    public delegate double ErrorFunc<T>(T c);

    public delegate T Generator<T>();

    public delegate T Mutator<T>(T x);

    public delegate T Crossoverator<T>(params T[] x);

    public abstract class GenAlg<T>
    {

        protected readonly ErrorFunc<T> Error;

        protected readonly Mutator<T> Mutate;

        protected readonly Crossoverator<T> Crossover;

        private int iteration = 0;

        private int evaluationCount = 0;

        private readonly int maxIterations;
        private readonly int maxEvaluations;
        private readonly double targetFitness;

        protected Individual<T>[] population;

        protected readonly Random Random = new Random();

        public List<Individual<T>> Population => new List<Individual<T>>(population);
        public int Iteration => iteration;
        public int EvaluationCount => evaluationCount;

        private bool PrintProgress { get; set; }

        protected GenAlg(ErrorFunc<T> errorFunc,
            Generator<T> generate, Mutator<T> mutate, Crossoverator<T> crossover,
            int populationSize = 20, int maxIterations = 100000, int maxEvaluations = 100000, double targetError = 1e-15,
            bool printProgress = false)
        {
            this.Error = x => { evaluationCount++; return errorFunc(x); };
            this.Mutate = mutate;
            this.Crossover = crossover;

            this.population = new Individual<T>[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                var chromosome = generate();
                this.population[i] = new Individual<T> { Value = chromosome, Fitness = 1 / Error(chromosome) };
            }
            this.maxIterations = maxIterations;
            this.maxEvaluations = maxEvaluations;
            this.targetFitness = 1 / targetError;

            this.PrintProgress = printProgress;
        }

        public T Run()
        {
            while (iteration < maxIterations && evaluationCount < maxEvaluations)
            {
                RunIteration();
                if (population[0].Fitness > targetFitness) break;
            }
            return population[0].Value;
        }

        public T RunIteration()
        {
            iteration++;
            var previousBestFitness = population[0].Fitness;
            SelectNextPopulation();

            int ib = 0; double fb = population[0].Fitness;
            for (int i = 1; i < population.Length; i++)
            {
                if (population[i].Fitness <= fb) continue;
                ib = i; fb = population[i].Fitness;
            }
            var indb = population[ib]; population[ib] = population[0]; population[0] = indb;
            //Array.Sort(population, (a, b) => a.Fitness > b.Fitness ? -1 : 1);
            if (PrintProgress && population[0].Fitness > previousBestFitness)
            {
                Console.WriteLine($"Iteration: {iteration}:\n" +
                                  $"  eval.: {evaluationCount}\n" +
                                  $"  error: {1 / population[0].Fitness}\n" +
                                  $"  value: {population[0].Value}");
            }
            return population[0].Value;
        }

        protected abstract void SelectNextPopulation();
    }
}
