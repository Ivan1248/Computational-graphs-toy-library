using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Fuzzy;

namespace FuzzyShipControlSystem
{
    public abstract class FuzzySystem
    {
        protected static int dMax = 200;  // 20
        protected static int vMax = 300;  // 40

        protected static int DomainIndex(int element, IDomain domain) => domain.IndexOf(DomainElement.Of(element));

        protected static int v(int x) => x / 20;
        protected static int vb(int x) => Math.Min(vMax, x) / 20;
        protected static int vi(int x) => DomainIndex(v(x), velDomain);
        protected static int dv(double x) => (int)(x * 20);
        protected static int d(int x) => x / 20;
        protected static int db(int x) => Math.Min(dMax, x) / 20;
        protected static int di(int x) => DomainIndex(d(x), velDomain);
        protected static int dd(double x) => (int)(x * 10);
        
        protected static IDomain
            distDomain = Domain.IntRange(d(0), d(dMax) + 1),
            velDomain = Domain.IntRange(v(0), v(vMax) + 1),
            dirDomain = Domain.IntRange(0, 1);

        protected static IFuzzySet
            extremelyClose = new CalculatedFuzzySet(distDomain, LFunction(20, 40, di)),
            veryClose = new CalculatedFuzzySet(distDomain, LFunction(40, 60, di)),
            quiteClose = new CalculatedFuzzySet(distDomain, LambdaFunction(40, 60, 70, di)),
            moderatelyClose = new CalculatedFuzzySet(distDomain, LambdaFunction(50, 70, 90, di)),
            close = new CalculatedFuzzySet(distDomain, LFunction(50, 70, di)),
            far = new CalculatedFuzzySet(distDomain, GammaFunction(50, 70, di)),
            notVeryClose = Operations.ZadehNot.Of(veryClose),
            notClose = far;

        protected static IFuzzySet
            verySlow = new CalculatedFuzzySet(velDomain, LFunction(60, 100, vi)),
            quiteSlow = new CalculatedFuzzySet(velDomain, LambdaFunction(80, 120, 140, vi)),
            slow = new CalculatedFuzzySet(velDomain, LFunction(120, 140, vi)),
            fast = new CalculatedFuzzySet(velDomain, GammaFunction(140, 160, vi)),
            tooFast = new CalculatedFuzzySet(velDomain, GammaFunction(160, 220, vi)),
            notFast = Operations.ZadehNot.Of(fast),
            notVerySlow = Operations.ZadehNot.Of(verySlow),
            notSlow = Operations.ZadehNot.Of(slow);

        protected static Func<int, double> LFunction(int a, int b, Func<int, int> conv) =>
            StandardFuzzySets.LFunction(conv(a), conv(b));
        protected static Func<int, double> LambdaFunction(int a, int b, int c, Func<int, int> conv) =>
            StandardFuzzySets.LambdaFunction(conv(a), conv(b), conv(c));
        protected static Func<int, double> GammaFunction(int a, int b, Func<int, int> conv) =>
            StandardFuzzySets.GammaFunction(conv(a), conv(b));


        public FuzzySystem(Defuzzifier defuzzify, BinaryOperation implication)
        {
            this.defuzzify = defuzzify;
            this.implication = implication ?? Math.Min;
        }

        public abstract int Infer(int L, int R, int LF, int RF, int Vel, int Dir);

        protected Rule[] rules;

        protected Defuzzifier defuzzify;

        protected BinaryOperation implication;
    }
}
