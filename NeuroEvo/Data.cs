using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinAlg;

namespace NeuroEvo
{
    static class Data
    {
        public static void Load(string filePath, out List<Vector> X, out List<Vector> Y)
        {
            X = new List<Vector>();
            Y = new List<Vector>();
            using (var sr = File.OpenText(filePath))
            {
                while (!sr.EndOfStream)
                {
                    var s = sr.ReadLine().Split('\t').Select(double.Parse).ToArray();
                    X.Add(Vector.Of(s[0], s[1]));
                    Y.Add(Vector.Of(s[2], s[3], s[4]));
                }
            }
        }
    }
}
