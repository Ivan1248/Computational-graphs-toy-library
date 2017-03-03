using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public class ParameterContainer : IOptimizable<ParameterContainer>
    {
        public Matrix[] W;
        public Vector[] B;

        public static ParameterContainer ZerosLike(Matrix[] W, Vector[] B)
        {
            ParameterContainer pe = new ParameterContainer
            {
                W = new Matrix[W.Length],
                B = new Vector[B.Length]
            };
            for (int l = 0; l < W.Length; l++)
            {
                pe.W[l] = Matrix.Zeros(W[l].RowCount, W[l].ColCount);
                pe.B[l] = Vector.Zeros(B[l].Dimension);
            }
            return pe;
        }

        public ParameterContainer Copy()
        {
            ParameterContainer pe = new ParameterContainer
            {
                W = new Matrix[W.Length],
                B = new Vector[B.Length]
            };
            for (int l = 0; l < W.Length; l++)
            {
                pe.W[l] = W[l].Copy();
                pe.B[l] = B[l].Copy();
            }
            return pe;
        }

        public ParameterContainer Add(ParameterContainer other)
        {
            for (int l = 0; l < W.Length; l++)
            {
                W[l].Add(other.W[l]);
                B[l].Add(other.B[l]);
            }
            return this;
        }

        public ParameterContainer AddWeighted(double thisWeight, ParameterContainer other, double otherWeight)
        {
            for (int l = 0; l < W.Length; l++)
            {
                W[l].Mul(thisWeight).Add(other.W[l].Copy().Mul(otherWeight));
                B[l].Mul(thisWeight).Add(other.B[l].Copy().Mul(otherWeight));
            }
            return this;
        }

        public ParameterContainer AddWeighted(ParameterContainer other, double otherWeight)
        {
            for (int l = 0; l < W.Length; l++)
            {
                W[l].Add(other.W[l].Copy().Mul(otherWeight));
                B[l].Add(other.B[l].Copy().Mul(otherWeight));
            }
            return this;
        }

        public ParameterContainer Sub(ParameterContainer other)
        {
            for (int l = 0; l < W.Length; l++)
            {
                W[l].Sub(other.W[l]);
                B[l].Sub(other.B[l]);
            }
            return this;
        }

        public ParameterContainer Multiply(double m)
        {
            for (int l = 0; l < W.Length; l++) { W[l].Mul(m); B[l].Mul(m); }
            return this;

        }

        public ParameterContainer Multiply(ParameterContainer p)
        {
            for (int l = 0; l < W.Length; l++) { W[l].Mul(p.W[l]); B[l].Mul(p.B[l]); }
            return this;
        }

        public ParameterContainer DivideBy(double d)
        {
            for (int l = 0; l < W.Length; l++) { W[l].DivBy(d); B[l].DivBy(d); }
            return this;

        }

        public ParameterContainer DivideBy(ParameterContainer p)
        {
            for (int l = 0; l < W.Length; l++) { W[l].DivBy(p.W[l]); B[l].DivBy(p.B[l]); }
            return this;
        }

        public ParameterContainer Map(Func<double, double> f)
        {
            for (int l = 0; l < W.Length; l++)
            {
                W[l] = f.Map(W[l]);
                B[l] = f.Map(B[l]);
            }
            return this;
        }

        public double NormSquare()
        {
            double ns = 0;
            for (int l = 0; l < W.Length; l++)
            {
                ns += B[l].NormSquare();
                for (int i = 0; i < W[l].RowCount; i++)
                    for (int j = 0; j < W[l].ColCount; j++)
                        ns += W[l][i, j] * W[l][i, j];
            }
            return ns;
        }
    }
}
