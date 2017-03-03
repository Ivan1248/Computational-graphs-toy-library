using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LinAlg;
using FDataset = EvoAlg.Dataset<LinAlg.Vector, double>;

namespace EvoAlg
{
    static class Program
    {
        static Vector gsMin = Vector.Of(-4, -4, -4, -4, -4);
        static Vector gsMax = Vector.Of(4, 4, 4, 4, 4);

        static void Main(string[] args)
        {
            FDataset data = LoadDataset("zad4-dataset1.txt");
            Console.WriteLine("GenerationalProportionalGenAlg");
            Console.ReadLine();
            new GenerationalProportionalGenAlg<Vector>(
                errorFunc: ErrorFunc(data),
                generate: () => Generation.RandomWithinHypercube(gsMin, gsMax),
                mutate: GenOps.VectorNormalMutator(mutationProbability: 0.2, sigma: 0.1),
                crossover: GenOps.VectorAverageCrossoverator(),
                elitism: 1,
                maxIterations: 100000,
                maxEvaluations: 100000,
                targetError: double.Epsilon,
                populationSize: 40,
                printProgress: true)
                .Run();
            Console.WriteLine("EliminationalTournamentGenAlg");
            Console.ReadLine();
            new EliminationalTournamentGenAlg<Vector>(
                errorFunc: ErrorFunc(data),
                generate: () => Generation.RandomWithinHypercube(gsMin, gsMax),
                mutate: GenOps.VectorNormalMutator(mutationProbability: 0.1, sigma: 0.05),
                crossover: GenOps.VectorAverageCrossoverator(), 
                mortality: 1,
                maxIterations: 100000,
                maxEvaluations: 100000,
                targetError: double.Epsilon,
                populationSize: 50,
                printProgress: true)
                .Run();
            Console.WriteLine("EliminationalTournamentGenAlg");
            Console.ReadLine();
            new EliminationalTournamentGenAlg<BitArray>(
                errorFunc: p => ErrorFunc(data)(Conversion.BitArrayToVector(p, gsMin, gsMax, 24)),
                generate: () => Conversion.VectorToBitArray(Generation.RandomWithinHypercube(gsMin, gsMax), gsMin, gsMax, 24),
                mutate: GenOps.BitArrayMutator(0.06),
                crossover: GenOps.BitArraySegmentedCrossoverator(0.01),
                mortality: 1,
                maxIterations: 100000,
                maxEvaluations: 100000,
                targetError: double.Epsilon,
                populationSize: 50,
                printProgress: true)
                .Run();
            Console.WriteLine("Kraj");
            Console.Read();
        }

        static FDataset LoadDataset(string filePath)
        {
            FDataset dataset = new FDataset();
            using (StreamReader sr = File.OpenText(filePath))
                while (!sr.EndOfStream)
                {
                    var s = sr.ReadLine().Split('\t');
                    dataset.Add(Vector.Of(double.Parse(s[0]), double.Parse(s[1])), double.Parse(s[2]));
                }
            return dataset;
        }

        static double F(double b0, double b1, double b2, double b3, double b4, double x, double y) =>
            Math.Sin(b0 + b1 * x) + b2 * Math.Cos(x * (b3 + y)) * 1 / (1 + Math.Exp(Math.Pow(x - b4, 2)));

        static double F(Vector b, Vector x) => F(b[0], b[1], b[2], b[3], b[4], x[0], x[1]);

        static ErrorFunc<Vector> ErrorFunc(FDataset data) =>
            parameters => data.Average(dp => Math.Pow(F(parameters, dp.x) - dp.y, 2));

    }
}
