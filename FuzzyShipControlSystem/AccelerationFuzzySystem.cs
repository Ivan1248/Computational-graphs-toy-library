using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Fuzzy;

namespace FuzzyShipControlSystem
{
    public class AccelerationFuzzySystem : FuzzySystem
    {
        private static int aMax = 300;  // 400
        private static IDomain accelDomain = Domain.IntRange(a(-aMax), a(aMax) + 1);
        private static IFuzzySet
            highNegAccel = new CalculatedFuzzySet(accelDomain, LFunction(-aMax, (int)(-aMax * 0.8), ai)),
            lowNegAccel = new CalculatedFuzzySet(accelDomain, LambdaFunction((int)(-aMax * 0.8), (int)(-aMax * 0.6), (int)(-aMax * 0.4), ai)),
            veryLowNegAccel = new CalculatedFuzzySet(accelDomain, LambdaFunction((int)(-aMax * 0.4), (int)(-aMax * 0.2), 0, ai)),
            zeroLikeAccel = new CalculatedFuzzySet(accelDomain, LambdaFunction(-80, 0, 80, ai)),
            lowPosAccel = new CalculatedFuzzySet(accelDomain, LambdaFunction((int)(aMax * 0.4), (int)(aMax * 0.6), (int)(aMax * 0.8), ai)),
            highPosAccel = new CalculatedFuzzySet(accelDomain, GammaFunction((int)(aMax * 0.8), aMax, ai));
        private static int a(int x) => x / 20;
        private static int ai(int x) => DomainIndex(a(x), accelDomain);
        private static int da(double x) => (int)(x * 20);

        DomainElement LRDistance = DomainElement.Of(dMax);
        DomainElement FDistance = DomainElement.Of(dMax);
        DomainElement Vel = DomainElement.Of(0);

        private int trace = int.MinValue;

        public AccelerationFuzzySystem(Defuzzifier defuzzify, BinaryOperation implication = null)
            : base(defuzzify, implication)
        {
            this.defuzzify = defuzzify;
            rules = new[]
            {
                new Rule(implication, () =>
                    new[] {LRDistance, Vel},
                    new[] {veryClose , verySlow},
                    highPosAccel),

               new Rule(implication, () =>
                    new[] {FDistance, Vel},
                    new[] {quiteClose, notSlow},
                    lowNegAccel),
                new Rule(implication, () =>
                    new[] {FDistance, Vel},
                    new[] {veryClose, notVerySlow },
                    lowNegAccel),
                new Rule(implication, () =>
                    new[] {FDistance, Vel},
                    new[] {extremelyClose, notVerySlow },
                    highNegAccel),

                new Rule(implication, () =>
                    new[] {Vel},
                    new[] {tooFast},
                    lowNegAccel),
                new Rule(implication, () =>
                    new[] {Vel    , FDistance},
                    new[] {notFast, notClose},
                    highPosAccel),
            };
        }

        public override int Infer(int L, int R, int LF, int RF, int Vel, int Dir)
        {
            LRDistance = DomainElement.Of(db(Math.Min(L, R)));
            FDistance = DomainElement.Of(db(Math.Min(LF, RF)));
            this.Vel = DomainElement.Of(vb(Vel));

            IFuzzySet[] conclusions = new IFuzzySet[rules.Length];
            for (int i = 0; i < rules.Length; i++)
                conclusions[i] = rules[i].Evaluate();
            if (trace >= 0) Trace(conclusions[trace]);
            IFuzzySet finalConclusion = Operations.MultiArg(Math.Max, conclusions);
            double result = defuzzify(finalConclusion);
            if (trace == -1) Trace(finalConclusion);
            if (!double.IsNaN(result))
                return da(result);
            ((Action)(() => Console.Beep(1200, 30))).BeginInvoke(null, null);
            return 0;
        }

        public void SetTrace(int ruleNumber = int.MinValue) => trace = ruleNumber;

        private void Trace(IFuzzySet conclusion)
        {
            Debug.WriteLine(conclusion);
            var concl = defuzzify(conclusion);
            Debug.WriteLine($"{concl} -> a={da(concl)}");
        }
    }
}