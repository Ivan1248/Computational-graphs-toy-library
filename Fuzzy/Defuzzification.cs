using System;

namespace Fuzzy
{
    public delegate double Defuzzifier(IFuzzySet fuzzySet);

    public static class Defuzzification
    {
        public static readonly Defuzzifier CenterOfAreaDefuzzifier = set =>
        {
            double area = 0, result = 0;
            foreach (var e in set.Domain)
            {
                area += set.Membership(e);
                result += e[0] * set.Membership(e);
            }
            if (area == 0)
                return double.NaN;
            return result / area;
        };

        public static readonly Defuzzifier CenterOfMaxDefuzzifier = set =>
        {
            double area = 0, result = 0, max = 0;
            foreach (var e in set.Domain)
            {
                var curr = set.Membership(e);
                if (curr > max)
                {
                    max = curr;
                    area = max;
                    result = e[0] * max;
                }
                else if (curr == max)
                {
                    area += max;
                    result += e[0] * max;
                }
            }
            if (area == 0)
                return double.NaN;
            return result / area;
        };
    }
}