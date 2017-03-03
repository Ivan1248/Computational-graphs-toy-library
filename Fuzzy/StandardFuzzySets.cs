using System;

namespace Fuzzy
{
    public static class StandardFuzzySets
    {
        public static Func<int, double> LFunction(int a, int b) => delegate (int i)
        {
            if (i <= a) return 1.0;
            if (i >= b) return 0.0;
            return (b - i) / (double)(b - a);
        };
        public static Func<int, double> GammaFunction(int a, int b) => delegate (int i)
        {
            if (i <= a) return 0.0;
            if (i >= b) return 1.0;
            return (i - a) / (double)(b - a);
        };
        public static Func<int, double> LambdaFunction(int a, int b, int c) => delegate (int i)
        {
            if (i <= a) return 0.0;
            if (i >= c) return 0.0;
            if (i <= b) return (i - a) / (double)(b - a);
            return (c - i) / (double)(c - b);
        };
    }
}
