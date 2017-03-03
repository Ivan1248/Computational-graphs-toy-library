using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace EvoAlg
{
    public static class GenOps
    {
        private static Random Random = new Random();

        #region Generic
        public static Mutator<T> DummyMutator<T>() => x => x;
        public static Crossoverator<T> DummyCrossoverator<T>() => x => x[0];
        #endregion

        #region Vector
        public static Mutator<Vector> VectorNormalMutator(double mutationProbability, double sigma) =>
            (Vector x) =>
            {
                x = x.Copy();
                for (int i = 0; i < x.Dimension; i++)
                    if (Random.NextDouble() < mutationProbability)
                        x[i] += Random.NextNormal() * sigma;
                return x;
            };

        public static Mutator<Vector> VectorNormalReplacementMutator(double mutationProbability, double sigma) =>
            (Vector x) =>
            {
                x = x.Copy();
                for (int i = 0; i < x.Dimension; i++)
                    if (Random.NextDouble() < mutationProbability)
                        x[i] = Random.NextNormal() * sigma;
                return x;
            };

        public static Crossoverator<Vector> VectorAverageCrossoverator() =>
            (Vector[] x) =>
            {
                var r = x[0].Copy();
                for (int i = 1; i < x.Length; i++)
                    r.Add(x[i]);
                return r.DivBy(x.Length);
            };

        public static Crossoverator<Vector> VectorGenerateRandomOrCrossoverCrossoverator(Generator<Vector> generate, Crossoverator<Vector> crossover, double generateRandomProbability) =>
            (Vector[] x) => Random.NextDouble() < generateRandomProbability ? generate() : crossover(x);

        public static Crossoverator<Vector> VectorSegmentedCrossoverator(double switchProbability) =>
            (Vector[] x) =>
            {
                var r = Vector.Zeros(x[0].Dimension);
                int p = Random.Next(0, x.Length);
                switchProbability *= (double)x.Length / (x.Length - 1);
                for (int i = 0; i < x[0].Dimension; i++)
                    r[i] = x[Random.NextDouble() < switchProbability ? (p = Random.Next(0, x.Length)) : p][i];
                return r;
            };
        #endregion

        #region BitArray
        public static Mutator<BitArray> BitArrayMutator(double bitMutationProbability) =>
            (BitArray x) =>
            {
                x = new BitArray(x);
                for (int i = 0; i < x.Count; i++)
                    if (Random.NextDouble() < bitMutationProbability)
                        x[i] = !x[i];
                return x;
            };

        public static Crossoverator<BitArray> BitArrayUniformCrossoverator() =>
            (BitArray[] x) =>
            {
                var r = new BitArray(x[0]);
                for (int i = 0; i < x[0].Length; i++)
                    r[i] = x[Random.Next(0, x.Length)][i];
                return r;
            };

        public static Crossoverator<BitArray> BitArraySegmentedCrossoverator(double switchProbability) =>
            (BitArray[] x) =>
            {
                var r = new BitArray(x[0]);
                int p = Random.Next(0, x.Length);
                switchProbability *= (double)x.Length / (x.Length - 1);
                for (int i = 0; i < x[0].Length; i++)
                    r[i] = x[Random.NextDouble() < switchProbability ? (p = Random.Next(0, x.Length)) : p][i];
                return r;
            };

        public static Crossoverator<BitArray> BitArrayCloneParentCrossoverator(int parent) =>
            (BitArray[] x) => new BitArray(x[parent]);

        public static Crossoverator<BitArray> BitArrayGenerateRandomOrCrossoverCrossoverator(Generator<BitArray> generate, Crossoverator<BitArray> crossover, double generateRandomProbability) =>
            (BitArray[] x) => Random.NextDouble() < generateRandomProbability ? generate() : crossover(x);
        #endregion
    }
}
