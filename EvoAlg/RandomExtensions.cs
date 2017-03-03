using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoAlg
{
    public static class RandomExtensions
    {
        public static double NextNormal(this Random r) =>  // http://stackoverflow.com/questions/218060/random-gaussian-variables
            Math.Sqrt(-2.0 * Math.Log(r.NextDouble())) * Math.Sin(2.0 * Math.PI * r.NextDouble());
    }
}
