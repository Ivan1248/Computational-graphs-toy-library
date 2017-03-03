using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace ComputationalGraphs
{
    class CollectBreak : Id
    {
        public CollectBreak(Node x, string name = null) : base(x, name)
        {
        }

        protected override void CollectPreceedingNodes(HashSet<Node> nodes, Predicate<Node> filter,
            int maxDepth = int.MaxValue, bool ignoreBreaks = false)
        {
        }
    }
}