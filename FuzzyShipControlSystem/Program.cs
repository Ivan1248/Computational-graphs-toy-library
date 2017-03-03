using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy;

namespace FuzzyShipControlSystem
{
    class Program
    {
        private static AccelerationFuzzySystem afs;
        private static SteeringFuzzySystem sfs;
        static void Main(string[] args)
        {
            Debugger.Launch();
            afs = new AccelerationFuzzySystem(Defuzzification.CenterOfAreaDefuzzifier, Math.Min/*(a, b) => a * b*/);
            sfs = new SteeringFuzzySystem(Defuzzification.CenterOfAreaDefuzzifier, Math.Min/*(a, b) => a * b*/);

            string str = Console.ReadLine();
            if (str[0] == 't')
            {
                afs.SetTrace(int.Parse(str.Substring(1)));
                str = Console.ReadLine();
            }
            afs.SetTrace(0);
            while (str[0] != 'K')
            {
                string[] p = str.Split(' ');
                int L = int.Parse(p[0]);
                int D = int.Parse(p[1]);   // udaljenost od obale desno
                int LK = int.Parse(p[2]);  // udaljenost od obale lijevo pod 45°
                int DK = int.Parse(p[3]);  // udaljenost od obale desno pod 45°
                int V = int.Parse(p[4]);   // brzina
                int S = int.Parse(p[5]);   // smjer (1|0)

                // fuzzy magic ...
                var akcel = afs.Infer(L, D, LK, DK, V, S);
                var kormilo = sfs.Infer(L, D, LK, DK, V, S);

                Console.Write($"{akcel} {kormilo}\r\n");
                Console.Out.Flush();
                str = Console.ReadLine();
            }
        }
    }
}
