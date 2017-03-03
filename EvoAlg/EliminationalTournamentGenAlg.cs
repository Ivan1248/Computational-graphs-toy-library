using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvoAlg
{
    public class EliminationalTournamentGenAlg<T> : GenAlg<T>
    {
        protected int mortality;
        protected int tournamentSize;

        public EliminationalTournamentGenAlg(ErrorFunc<T> errorFunc, Generator<T> generate, Mutator<T> mutate,
            Crossoverator<T> crossover, int mortality = 1, int populationSize = 20, int maxIterations = 100000,
            int maxEvaluations = 100000, double targetError = 1e-15, bool printProgress = false, int tournamentSize = 3)
            : base(errorFunc, generate, mutate, crossover, populationSize, maxIterations, maxEvaluations, targetError, printProgress)
        {
            this.mortality = mortality;
            this.tournamentSize = tournamentSize;
        }

        protected override void SelectNextPopulation()
        {
            Func<int, int[]> selectTournament = n =>
            {
                int worst = Random.Next(0, population.Length);
                var tournament = new List<int>(n);
                for (int i = 0; i < n - 1;)
                {
                    var par = Random.Next(0, population.Length);
                    if (tournament.Contains(par) || par == worst) continue;
                    if (population[par].Fitness > population[worst].Fitness)
                        tournament.Add(par);
                    else
                    {
                        tournament.Add(worst);
                        worst = par;
                    }
                    i++;
                }
                tournament.Add(worst);
                return tournament.ToArray();
            };
            for (int i = 0; i < mortality; i++)
            {
                var tournament = selectTournament(tournamentSize);
                var c = Crossover(population[tournament[0]].Value, population[tournament[1]].Value);
                c = Mutate(c);
                population[tournament[tournamentSize - 1]] = new Individual<T> { Value = c, Fitness = 1 / Error(c) };
            }
        }
    }
}
