using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using EvoAlg;
using LinAlg;

namespace AprEvoAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                switch (Console.ReadLine())
                {
                    case "1": Demonstrate(Zadatak1); break;
                    case "2": Demonstrate(Zadatak2); break;
                    case "3": Demonstrate(Zadatak3); break;
                    case "4": Demonstrate(Zadatak4); break;
                    case "5": Demonstrate(Zadatak5); break;
                    case "q": return;
                }
            }
        }

        static void Demonstrate(Action action)
        {
            Console.Clear();
            action();
            //Console.ReadLine();
        }

        private static Func<ErrorFunc<Vector>, Generator<Vector>, GenAlg<Vector>>[] gaGenerators =
        {
            (err, gen) => new GenerationalProportionalGenAlg<Vector>(
                errorFunc: err,
                generate: gen,
                mutate: GenOps.VectorNormalMutator(mutationProbability: 0.2, sigma: 4), //*
                crossover: GenOps.VectorAverageCrossoverator(),
                elitism: 1, //*
                maxIterations: int.MaxValue,
                maxEvaluations: 100000,
                targetError: 1e-60,
                populationSize: 50, //*
                printProgress: false),
            (err, gen) => new EliminationalTournamentGenAlg<Vector>(
                errorFunc: err,
                generate: gen,
                mutate: GenOps.VectorNormalMutator(mutationProbability: 0.2, sigma: 4),
                crossover: GenOps.VectorAverageCrossoverator(),
                mortality: 1,
                maxIterations: int.MaxValue,
                maxEvaluations: 100000,
                targetError: 1e-60,
                populationSize: 50,
                printProgress: false),
        };
        static string[] gaNames = { "GenerationalProportional", "EliminationalTournament" };

        static string Test(int experimentCount, ErrorFunc<Vector> errorFunc,
            Func<ErrorFunc<Vector>, Vector> gaResultGenerator, double tol = 1e-6)
        {
            var errors = new double[experimentCount];
            for (int i = 0; i < experimentCount; i++)
            {
                errors[i] = errorFunc(gaResultGenerator(errorFunc));
            }
            return $"avg: {errors.Average().ToString("0.000e+00")}, " +
                   $"med: {errors[experimentCount / 2].ToString("0.000e+00")}, " +
                   $"min: {errors.Min().ToString("0.000e+00")}, " +
                   $"hitr: {((double)errors.Count(x => x < tol) / experimentCount).ToString("0.00")}";
        }


        /// <summary>
        /// Isprobajte vašu implementaciju GA nad svim funkcijama uz granice [-50, 150] za sve varijable. Za
        /// funkciju f3 odaberite barem 5 varijabli, a za f6 i f7 dvije varijable. Za sve funkcije možete smatrati
        /// da je rješenje pronađeno ako je krajnja vrijednost funkcije cilja manja od 10-6. Za neke funkcije
        /// algoritam će biti potrebno pokrenuti nekoliko puta. Koje zaključke možete donijeti o uspješnosti GA
        /// za pojedinu funkciju? Koje su se funkcije pokazale teškim i zašto?
        /// </summary>
        static void Zadatak1()
        {
            Console.WriteLine("\n1. Zadatak\n");
            ErrorFunc<Vector>[] errorFunctions =
            {
                Functions.F3, Functions.ShafferF6, Functions.ShafferF7ish
            };
            int[] numbersOfVariables = { 5, 2, 2 };

            for (int ef = 0; ef < errorFunctions.Length; ef++)
            {
                ErrorFunc<Vector> f = errorFunctions[ef];
                var n = numbersOfVariables[ef];
                Console.WriteLine(f.Method.Name);
                for (int ga = 0; ga < 2; ga++)
                {
                    Console.WriteLine($" {gaNames[ga]}");
                    for (int i = 0; i < 5; i++)
                    {
                        var result = gaGenerators[ga](f,
                            () => Generation.RandomWithinHypercube(Vector.Filled(n, -50), Vector.Filled(n, 150))
                            ).Run();
                        var error = f(result);
                        Console.WriteLine($"  {result,40} {error,20}");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Provedite GA na funkcijama f6 i f7 mijenjajući dimenzionalnost funkcije (1, 3, 6, 10). Kako
        /// povećanje dimenzionalnosti funkcije utječe na ponašanje algoritma?
        /// </summary>
        static void Zadatak2()
        {
            Console.WriteLine("\n2. Zadatak\n");
            ErrorFunc<Vector>[] errorFunctions = { Functions.ShafferF6, Functions.ShafferF7ish };

            foreach (ErrorFunc<Vector> ef in errorFunctions)
            {
                Console.WriteLine(ef.Method.Name);

                for (int ga = 0; ga < 2; ga++)
                {
                    Console.WriteLine($"  {gaNames[ga]}");
                    foreach (var n in new[] { 1, 3, 6, 10 })
                    {
                        var result = Test(10, ef, (f) => gaGenerators[ga](f,
                            () => Generation.RandomWithinHypercube(Vector.Filled(n, -50), Vector.Filled(n, 150))).Run());
                        Console.WriteLine($"    n={n}| {result}");
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Za funkcije f6 i f7 usporedite učinkovitost GA koji koristi binarni prikaz uz preciznost na 4 decimale
        /// (tj. 10-4) i GA koji koristi prikaz s pomičnom točkom (ostali parametri neka budu jednaki), za
        /// dimenzije 3 i 6. Rad algoritma ograničite zadanim brojem evaluacija (oko 1e5 - 1e6). Inačice
        /// algoritma usporedite po uputama u sljedećem odjeljku. Što možete zaključiti o različitim prikazima
        /// rješenja za različite funkcije?
        /// </summary>
        static void Zadatak3()
        {
            Console.WriteLine("\n3. Zadatak\n");
            ErrorFunc<Vector>[] errorFunctions = { Functions.ShafferF6, Functions.ShafferF7ish };
            int prec = (int)Math.Log(1e4 * (150 + 50), 2) + 1;

            Func<ErrorFunc<Vector>, int, GenAlg<Vector>> floatGa =
                (err, dim) => new EliminationalTournamentGenAlg<Vector>(
                    errorFunc: err,
                    generate: () => Generation.RandomWithinHypercube(Vector.Filled(dim, -50), Vector.Filled(dim, 150)),
                    mutate: GenOps.VectorNormalMutator(mutationProbability: 0.2, sigma: 4),
                    crossover: GenOps.VectorAverageCrossoverator(),
                    maxIterations: int.MaxValue,
                    maxEvaluations: 500000,
                    targetError: 1e-60,
                    populationSize: 30,
                    printProgress: false);

            Func<ErrorFunc<Vector>, int, GenAlg<BitArray>> bitGa =
                (err, dim) =>
                {
                    Vector l = Vector.Filled(dim, -50), r = Vector.Filled(dim, 150);
                    return new EliminationalTournamentGenAlg<BitArray>(
                        errorFunc: p => err(Conversion.BitArrayToVector(p, l, r, prec)),
                        generate: () =>
                            Conversion.VectorToBitArray(Generation.RandomWithinHypercube(l, r), l, r, prec),
                        mutate: GenOps.BitArrayMutator(0.2),
                        crossover: GenOps.BitArraySegmentedCrossoverator(0.01),
                        mortality: 1,
                        maxIterations: int.MaxValue,
                        maxEvaluations: 500000,
                        targetError: double.Epsilon,
                        populationSize: 30,
                        printProgress: false);
                };

            Func<ErrorFunc<Vector>, int, Vector>[] grgs =
            {
                (f, dim) => floatGa(f, dim).Run(),
                (f, dim) =>
                    Conversion.BitArrayToVector(bitGa(f, dim).Run(), Vector.Filled(dim, -50), Vector.Filled(dim, 150),
                        prec)
            };

            foreach (ErrorFunc<Vector> ef in errorFunctions)
            {
                Console.WriteLine(ef.Method.Name);
                for (int i = 0; i < grgs.Length; i++)
                {
                    var grg = grgs[i];
                    Console.WriteLine(new[]
                    {
                        "  EliminationalTournamentGenAlg<Vector>",
                        "  EliminationalTournamentGenAlg<BitArray>"
                    }[i]);
                    foreach (var n in new[] { 3, 6 })
                    {
                        var result = Test(10, ef, (f) => grg(f, n));
                        Console.WriteLine($"    n={n}| {result}");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Za funkciju f6 pokušajte pronaći 'idealne' parametre genetskog algoritma. 'Idealne' parametre
        /// potrebno je odrediti barem za veličinu populacije (npr. 30, 50, 100, 200) i vjerojatnost mutacije
        /// jedinke (npr. 0.1, 0.3, 0.6, 0.9) a po želji možete i za još neke druge parametre koje je vaš algoritam
        /// koristio. Jedan postupak traženja parametara opisan je u nastavku. Koristite medijan kao mjeru
        /// usporedbe i prikažite kretanje učinkovitosti za barem jedan parametar uz pomoć box-plot prikaza
        /// (opisano u nastavku).
        /// </summary>      
        static void Zadatak4()
        {
            Console.WriteLine("\n4. Zadatak\n");
            int n = 2;
            Console.WriteLine("\nn = 2\n");


            Func<ErrorFunc<Vector>, double, double, int, int, GenAlg<Vector>> floatGa =
                (err, mutProb, mutSigma, popSize, dim) => new EliminationalTournamentGenAlg<Vector>(
                    errorFunc: err,
                    generate: () => Generation.RandomWithinHypercube(Vector.Filled(dim, -50), Vector.Filled(dim, 150)),
                    mutate: GenOps.VectorNormalMutator(mutationProbability: mutProb, sigma: mutSigma),
                    crossover: GenOps.VectorAverageCrossoverator(),
                    maxIterations: int.MaxValue,
                    maxEvaluations: 100000,
                    targetError: 1e-60,
                    populationSize: popSize,
                    printProgress: false);
            foreach (var mutSigma in new[] { 1, 2, 4, 8 })
            {
                foreach (var popSize in new[] { 20, 30, 50, 100, 200 })
                {
                    foreach (var mutProb in new[] { 0.1, 0.2, 0.6, 0.9 })
                    {
                        ErrorFunc<Vector> errorFunc = Functions.ShafferF6;
                        var errors = new double[10];
                        for (int i = 0; i < 10; i++)
                            errors[i] = errorFunc(floatGa(errorFunc, mutProb, mutSigma, popSize, n).Run());
                        var result = $"med: {errors[10 / 2].ToString("0.000e+00")}, " +
                                     $"hitr: {((double)errors.Count(x => x < 1E-06) / 10).ToString("0.00")}";
                        Console.WriteLine($"   {mutSigma.ToString("0.0")} {popSize.ToString("###")} {mutProb} | {result}");
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Ako ste implementirali turnirsku selekciju, probajte nad nekom težom funkcijom (f6 ili f7) izvesti
        /// algoritam koristeći različite veličine turnira. Pomaže li veći turnir algoritmu da pronađe bolja
        /// rješenja? Ako ste implementirali roulette wheel selekciju, isprobajte više vrijednosti omjera odabira
        /// najbolje i najlošije jedinke (skaliranje funkcije cilja) te komentirajte dobivene rezultate (također
        /// možete isprobati generacijsku i eliminacijsku varijantu).
        /// </summary>   
        static void Zadatak5()
        {
            Console.WriteLine("\n5. Zadatak\n");

            Func<ErrorFunc<Vector>, int, GenAlg<Vector>> floatGa =
                (err, tournamentSize) => new EliminationalTournamentGenAlg<Vector>(
                    errorFunc: err,
                    generate: () => Generation.RandomWithinHypercube(Vector.Filled(1, -50), Vector.Filled(1, 150)),
                    mutate: GenOps.VectorNormalMutator(mutationProbability: 0.2, sigma: 2),
                    crossover: GenOps.VectorAverageCrossoverator(),
                    maxIterations: int.MaxValue,
                    maxEvaluations: 100000,
                    targetError: 1e-60,
                    populationSize: 50,
                    printProgress: false,
                    tournamentSize: tournamentSize);
            foreach (var ts in new[] { 3, 4, 5, 6, 10 })
            {
                const int t = 20;
                ErrorFunc<Vector> errorFunc = Functions.ShafferF6;
                var errors = new double[t];
                for (int i = 0; i < t; i++)
                    errors[i] = errorFunc(floatGa(errorFunc, ts).Run());
                var result = $"med: {errors[10 / 2].ToString("0.000e+00")}, " +
                             $"hitr: {((double)errors.Count(x => x < 1E-06) / t).ToString("0.00")}";
                Console.WriteLine($"   {ts} | {result}");
            }
            Console.WriteLine();
        }
    }
}
