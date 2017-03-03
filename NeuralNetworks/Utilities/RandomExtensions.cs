using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworks.Utilities;

namespace NeuralNetworks.Extensions
{
    public static class RandomExtensions
    {
        public static double NextNormal(this Random r) =>  // http://stackoverflow.com/questions/218060/random-gaussian-variables
            Math.Sqrt(-2.0 * Math.Log(r.NextDouble())) * Math.Sin(2.0 * Math.PI * r.NextDouble());

        public static void Shuffle<T>(params T[][] arrays)
        {
            int n = arrays[0].Length;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = rnd.Next(0, n--);
                foreach (var arr in arrays)
                    General.Swap(ref arr[k], ref arr[n]);
            }
        }
    }
}
