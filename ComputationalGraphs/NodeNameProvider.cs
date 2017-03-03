using System;
using System.Collections.Generic;

namespace ComputationalGraphs
{
    static class NodeNameProvider
    {
        static Dictionary<Type, int> counter = new Dictionary<Type, int>();

        public static string GetName(Type type)
        {
            if (!counter.ContainsKey(type)) counter[type] = 0;
            return $"{type.Name}_{counter[type]++}";
        }
    }
}