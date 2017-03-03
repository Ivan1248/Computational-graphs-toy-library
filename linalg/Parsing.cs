using System.Collections.Generic;
using System.Globalization;

namespace LinAlg
{
    static class Parsing
    {
        public static double[] ParseDoubleArray(string repr)
        {
            int start = 0;
            repr += ' ';
            List<double> vals = new List<double>(4);

            space: for (int i = start; i < repr.Length; i++)
                if (repr[i] != ' ' && repr[i] != '\t')
                {
                    start = i;
                    goto value;
                }
            goto end;
            value: for (int i = start + 1; i < repr.Length; i++)
                if (repr[i] == ' ')
                {
                    vals.Add(double.Parse(repr.Substring(start, i - start), CultureInfo.InvariantCulture));
                    start = i;
                    goto space;
                }
            end: return vals.ToArray();
        }
    }
}
