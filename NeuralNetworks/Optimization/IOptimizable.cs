using System;
using System.Security.Cryptography.X509Certificates;

namespace NeuralNetworks
{
    public interface IOptimizable<P>
    {
        P Copy();
        P Add(P other);
        P AddWeighted(double thisWeight, P other, double otherWeight);
        P AddWeighted(P other, double otherWeight);
        P Sub(P other);
        P Multiply(double m);
        P Multiply(P m);
        P DivideBy(double d);
        P DivideBy(P d);
        P Map(Func<double,double> f);
        double NormSquare();
    }
}