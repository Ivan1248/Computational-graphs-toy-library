using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGraphs
{
    public partial class Node
    {
        public static Node operator +(Node a, Node b) => a.Add(b);
        public static Node operator +(Node a, double s) => a.Add(s);
        public static Node operator +(double s, Node a) => a.Add(s);
        public static Node operator -(Node a, Node b) => a.Sub(b);
        public static Node operator -(Node a, double s) => a.Add(-s);
        public static Node operator -(Node a) => a.Neg();
        public static Node operator *(Node a, Node b) => a.Mul(b);
        public static Node operator /(Node a, Node b) => a.DivBy(b);
        public static Node operator /(double s, Node b) => s.DivBy(b);
    }
}
