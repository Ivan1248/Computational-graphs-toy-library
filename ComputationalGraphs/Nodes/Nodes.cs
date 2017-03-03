using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGraphs.Utilities;
using LinAlg;

namespace ComputationalGraphs
{
    public static class Nodes
    {
        // Identity
        public static Node Id(this Node x, string name = null) => new Id(x, name);

        // Special
        public static InputNode Input(int dimension, string name = null) => new InputNode(dimension, name);
        public static InputNode Constant(int dimension, double c, string name = null)
        {
            var cn = Nodes.Input(dimension, name);
            cn.Set(Vector.Filled(dimension, c));
            return cn;
        }
        public static Node CollectBreak(this Node x, string name = null) => new CollectBreak(x, name);
        public static Node Dropout(this Node x, double dropoutRate, string name = null) =>
            new Dropout(x, dropoutRate, name);
        public static Node BackpropAmplifier(this Node x, double a, string name = null) =>
            new BackpropAmplifier(x, a, name);

        // Operations
        public static Node Abs(this Node a, string name = null) => new Abs(a, name);
        public static Node Add(this Node a, Node b, string name = null) => new Add(a, b, name);
        public static Node Add(this Node a, double s, string name = null) =>
            s == 1 ? (Node)new AddCScal1(a, name) : new AddCScal(a, s, name);
        public static Node Add(this Node a, string name = null) => new AddP(a, name);
        public static Node AffineMap(this Node x, int dimension, string name = null) =>
            new AffineMap(x, dimension, name);
        public static Node Concat(params Node[] nodes) => new Concat(nodes);
        public static Node Concat(Node[] nodes, string name) => new Concat(name, nodes);
        public static Node Concat(IEnumerable<Node> nodes, string name = null) => new Concat(name, nodes.ToArray());
        public static Node DivBy(this Node a, Node b, string name = null) =>
            b.Dimension == 1 ? new DivByScalar(a, b, name) : (Node)new DivBy(a, b, name);
        public static Node DivBy(this double s, Node b, string name = null) =>
            s == 1.0 ? b.Pow(-1) : Nodes.Constant(b.Dimension, s).DivBy(b);
        public static Node Exp(this Node x, string name = null) => new Exp(x, name);
        public static Node Gaussian(this Node x, string name = null) => new Gaussian(x, name);
        public static Node LinearMap(this Node x, int dimension, string name = null) => 
            new LinearMap(x, dimension, name);
        public static Node Logistic(this Node x, string name = null) => new Logistic(x, name);
        public static Node Min(params Node[] nodes) => new Min(nodes);
        public static Node Min(Node[] nodes, string name) => new Min(name, nodes);
        public static Node Mul(this Node a, Node b, string name = null) => new Mul(a, b, name);
        public static Node Mul(this Node a, string name = null) => new PMul(a, name);
        public static Node Neg(this Node a, string name = null) => new Neg(a, name);
        public static Node PLogistic(this Node x, string name = null) => new PLogistic(x, name);
        public static Node PositiveLinearMap(this Node x, int dimension, string name = null) =>
            new PositiveLinearMap(x, dimension, name);
        public static Node Pow(this Node x, double p, string name = null) =>
            p == -1 ? (Node)new PowCScalM1(x, name) : new PowCScal(x, p, name);
        public static Node Pow(this Node x, string name = null) => new PowP(x, name);
        public static Node PReLU(this Node x, string name = null) => new PReLU(x, name);
        public static Node ReduceMax(this Node x, string name = null) => new ReduceMax(x, name);
        public static Node ReduceMin(this Node x, string name = null) => new ReduceMin(x, name);
        public static Node ReduceSum(this Node x, string name = null) => new ReduceSum(x, name);
        public static Node ReLU(this Node x, string name = null) => new ReLU(x, name);
        public static Node Repeat(this Node x, int n, string name = null) => new Repeat(x, n, name);
        public static Node Slice(this Node x, int from, int to, string name = null) => new Slice(x, from, to, name);
        public static Node Softmax(this Node x, string name = null) => new Softmax(x, name);
        public static Node Sqr(this Node x, string name = null) => new Sqr(x, name);
        public static Node Sub(this Node a, Node b, string name = null) => new Sub(a, b, name);
        public static Node Tanh(this Node x, string name = null) => new Tanh(x, name);
        public static Node TanhLeCun(this Node x, string name = null) => new TanhLeCun(x, name);
        public static Node WeightedAverages(this Node x, int dimension, string name = null) =>
            new WeightedAverages(x, dimension, name);
        public static Node WeightedSums(this Node x, int dimension, bool bias = false, string name = null) =>
            bias ? new AffineMap(x, dimension, name) : new LinearMap(x, dimension, name);

        // Losses
        public static LossNode SquaredLoss(this Node prediction, Node goal, string name = null) =>
            new SquaredLoss(prediction, goal, name);
        public static LossNode CathegoricCrossEntropyLoss(this Node prediction, Node goal, string name = null) =>
            new CathegoricCrossEntropyLoss(prediction, goal, name);
        public static LossNode BernoulliCrossEntropyLoss(this Node prediction, Node goal, string name = null) =>
            new BernoulliCrossEntropyLoss(prediction, goal, name);


        // Helper functions
        public static IList<Vector> GetParameters(this IList<Node> nodes)
        {
            var result = new List<Vector>();
            foreach (var node in nodes)
                result.AddRange(node.Parameters);
            return result;
        }

        public static void SetParameters(this IList<Vector> parameters, IList<Vector> newParematers) =>
            parameters.DoElementwise(newParematers, (o, n) => o.SetAll(n));

        public static void SetParameters(this IList<Node> nodes, IList<Vector> newParematers) =>
            nodes.GetParameters().DoElementwise(newParematers, (o, n) => o.SetAll(n));

        public static IList<Vector> GetParameterGradients(this IList<Node> nodes)
        {
            var result = new List<Vector>();
            foreach (var node in nodes)
                result.AddRange(node.ParameterErrorGradients);
            return result;
        }
    }
}