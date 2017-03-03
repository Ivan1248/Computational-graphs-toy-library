using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace EvoAlg
{
    public static class Generation
    {
        static Random Random = new Random();

        public static Vector RandomWithinHypercube(Vector dimensionMins, Vector dimensionMaxs)
        {
            var foo = dimensionMaxs - dimensionMins;
            for (int i = 0; i < foo.Dimension; i++)
                foo[i] *= Random.NextDouble();
            return foo.Add(dimensionMins);
        }

        public static Vector RandomNormal(int dimension, double mu, double sigma)
        {
            var foo = Vector.Zeros(dimension);
            for (int i = 0; i < foo.Dimension; i++)
                foo[i] = Random.NextNormal() * sigma + mu;
            return foo;
        }
    }
}
