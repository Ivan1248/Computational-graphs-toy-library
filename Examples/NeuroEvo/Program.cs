using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using ComputationalGraphs;
using EvoAlg;
using LinAlg;

namespace NeuroEvo
{
    public static class NeuroEvo
    {
        private static void Main(string[] args)
        {
            // Definiranje neuronske mreže
            var input = Nodes.Input(2);
            var label = Nodes.Input(3);
            var output = input.NeuralNetwork(2, 8, 5, 3);
            var parameters = output.CollectPreceedingNodes().GetParameters();
            var trainer = new Trainer(input, label, output.SquaredLoss(label), new GradientDescentOptimizer(0));

            // Učitavanje podataka
            Data.Load("zad7-dataset.txt", out var X, out var Y);
            trainer.SetData(X, Y);

            // Definiranje i pokretanje genetskog algoritma
            var generator = ListGenOps.Mutator(GenOps.VectorNormalReplacementMutator(1, 1));
            var mutator = ListGenOps.CombinedMutator(
                0.8,
                ListGenOps.Mutator(GenOps.VectorNormalMutator(0.02, 0.3)),
                ListGenOps.Mutator(GenOps.VectorNormalReplacementMutator(0.08, 1))
            );
            var ga = new EliminationalTournamentGenAlg<IList<Vector>>(
                errorFunc: p => { parameters.SetParameters(p); return trainer.GetError(); },
                generate: () => generator(parameters),
                mutate: mutator,
                crossover: ListGenOps.Crossoverator(GenOps.VectorSegmentedCrossoverator(0.12)),
                populationSize: 8,
                maxEvaluations: 400000,
                maxIterations: 400000,
                targetError: 1e-2,
                printProgress: true,
                tournamentSize: 3
            );
            parameters.SetParameters(ga.Run());

            // Ispis parametara i rezultata
            Console.WriteLine("\nParametri:");
            foreach (var node in output.CollectPreceedingNodes())
            {
                if (node.Parameters.Length == 0) continue;
                Console.WriteLine($"{node}:");
                foreach (var p in node.Parameters)
                    Console.WriteLine($"{p}");
            }
            Console.WriteLine("\nRezultati:");
            Vector GetOutput(Vector x) { input.Set(x); return output.Output; }
            var Yp = X.Select(x => GetOutput(x)).ToList();
            var Ypd = Yp.Select(y => Vector.OneHot(y.ArgMax(), y.Dimension)).ToList();
            var R = Ypd.Zip(Y, (h, t) => h.ArgMax() == t.ArgMax());
            Console.WriteLine($"{"ulaz",14} {"labela",10} {"predikc.",10} {"ispravno",10}");
            for (var i = 0; i < Yp.Count; i++)
                Console.WriteLine($"{X[i].ToString(3),14} {Y[i],10} {Ypd[i],10} {(Ypd[i].ArgMax() == Y[i].ArgMax() ? '+' : '-'),10}");
            Console.WriteLine($"\nTočnost: {R.Count(r => r)}/{Y.Count}");

            Console.ReadLine();
        }
    }
}