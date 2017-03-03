using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace EvoAlg
{
    public class Conversion
    {
        public static BitArray VectorToBitArray(Vector x, Vector min, Vector max, int bitsPerDimension = 16)
        {
            var ba = new BitArray(bitsPerDimension * x.Dimension);
            var len = max - min;
            var xs = (x - min).DivBy(len).Mul(1 << (bitsPerDimension - 1) - 1);
            for (int i = 0; i < xs.Dimension; i++)
                CopyBits((long)xs[i], bitsPerDimension - 1, ba, i * bitsPerDimension);
            return ba;
        }
        public static Vector BitArrayToVector(BitArray x, Vector min, Vector max, int bitsPerDimension = 16)
        {
            var len = max - min;
            var xv = min.Copy();
            var a = 1 << (bitsPerDimension - 1) - 1;
            for (int i = 0; i < xv.Dimension; i++)
            {
                long ib = CopyBits(x, i * bitsPerDimension, bitsPerDimension);
                xv[i] += (double)ib / a * len[i];
            }
            return xv;
        }

        static void CopyBits(long source, int sourceStart, BitArray destination, int destinationStart)
        {
            for (int i = 0, bit = 1 << sourceStart; i <= sourceStart; i++, bit >>= 1)
                destination.Set(destinationStart + i, (source & bit) != 0);
        }

        static long CopyBits(BitArray source, int sourceStart, int numberOfBits)
        {
            long destination = 0;
            for (int i = 0, bit = 1 << (numberOfBits - 1); i < numberOfBits; i++, bit >>= 1)
                if (source[sourceStart + i])
                    destination |= (long)bit;
            return destination;
        }
    }
}
