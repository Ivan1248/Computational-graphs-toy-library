using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Fuzzy;

namespace FuzzyShipControlSystem
{
    public class SteeringFuzzySystem : FuzzySystem
    {
        private static int rMax = 120;  // 40
        private static IDomain rudderDomain = Domain.IntRange(r(-rMax), r(rMax) + 1);
        private static IFuzzySet
            sharpLeft = new CalculatedFuzzySet(rudderDomain, GammaFunction(90, 120, ri)),
            moderateLeft = new CalculatedFuzzySet(rudderDomain, LambdaFunction(30, 60, 90, ri)),
            softLeft = new CalculatedFuzzySet(rudderDomain, LambdaFunction(0, 15, 30, ri)),
            forward = new CalculatedFuzzySet(rudderDomain, LambdaFunction(-1, 0, 1, ri)),
            forwarish = new CalculatedFuzzySet(rudderDomain, LambdaFunction(-30, 0, 30, ri)),
            softRight = new CalculatedFuzzySet(rudderDomain, LambdaFunction(-30, -15, 0, ri)),
            moderateRight = new CalculatedFuzzySet(rudderDomain, LambdaFunction(-90, -60, -30, ri)),
            sharpRight = new CalculatedFuzzySet(rudderDomain, LFunction(-120, -90, ri));
        private static int r(int x) => x / 15;
        private static int ri(int x) => DomainIndex(r(x), rudderDomain);
        private static int dr(double x) => (int)(x * 15);

        DomainElement LDistance = DomainElement.Of(dMax);
        DomainElement RDistance = DomainElement.Of(dMax);
        DomainElement Vel = DomainElement.Of(0);

        public SteeringFuzzySystem(Defuzzifier defuzzify, BinaryOperation implication = null)
            : base(defuzzify, implication)
        {
            rules = new[]
            {
                new Rule(implication, () =>
                    new[] {LDistance},
                    new[] {veryClose},
                    sharpRight),
                new Rule(implication, () =>
                    new[] {LDistance},
                    new[] {quiteClose},
                    moderateRight),
                new Rule(implication, () =>
                    new[] {LDistance},
                    new[] {moderatelyClose},
                    softRight),

                new Rule(implication, () =>
                    new[] {RDistance},
                    new[] {veryClose},
                    sharpLeft),
                new Rule(implication, () =>
                    new[] {RDistance},
                    new[] {quiteClose},
                    moderateLeft),
                new Rule(implication, () =>
                    new[] {RDistance},
                    new[] {moderatelyClose},
                    softLeft),

                /*new Rule(implication, () =>
                    new[] {LDistance, RDistance},
                    new[] {notClose , notClose},
                    forwardish),*/
            };
        }

        public override int Infer(int L, int R, int LF, int RF, int Vel, int Dir)
        {
            LDistance = DomainElement.Of(db(Math.Min(L * 2, LF)));
            RDistance = DomainElement.Of(db(Math.Min(R * 2, RF)));
            this.Vel = DomainElement.Of(vb(Vel));

            IFuzzySet[] conclusions = new IFuzzySet[rules.Length];
            for (int i = 0; i < rules.Length; i++)
                conclusions[i] = rules[i].Evaluate();
            IFuzzySet finalConclusion = Operations.MultiArg(Math.Max, conclusions);
            double result = defuzzify(finalConclusion);
            if (!double.IsNaN(result))
                return dr(result);
            ((Action)(() => Console.Beep(1200, 30))).BeginInvoke(null, null);
            return 0;
        }
    }
}