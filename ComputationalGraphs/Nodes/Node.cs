using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinAlg;

namespace ComputationalGraphs
{
    public abstract partial class Node
    {
        protected static Random Random = new Random();
        protected static readonly Vector[] emptyVectorArray = { };

        private readonly List<Node> consumers = new List<Node>();
        private readonly Node[] producers;
        private Vector output;
        private bool outputNeedsUpdate = false;
        private Vector[] inputErrorGradients;
        private Vector[] parameterErrorGradients;
        private bool gradientsNeedsUpdate = false;
        private readonly string name;

        public int Dimension { get; private set; }

        public IReadOnlyList<Node> Consumers => consumers;
        public IReadOnlyList<Node> Producers => producers;

        public void ReinitializeParameters(bool propagateBackward = true)
        {
            ReinitializeParametersProtected();
            foreach (var node in propagateBackward ? Producers : Consumers)
                node.ReinitializeParameters(propagateBackward);
        }

        public abstract Vector[] Parameters { get; }

        public virtual (double mean, double deviation) InputCharacteristics => (0.0, 1.0);

        public Vector Output
        {
            get
            {
                UpdateOutputIfNecessary();
                return output;
            }
        }

        public Vector[] ParameterErrorGradients
        {
            get
            {
                UpdateErrorGradientsIfNecessary();
                return parameterErrorGradients;
            }
        }

        public Vector GetInputErrorGradient(Node producer)
        {
            UpdateErrorGradientsIfNecessary();
            for (var i = 0; i < producers.Length; i++)
                if (producers[i] == producer)
                    return inputErrorGradients[i];
            return null;
        }

        public List<Node> CollectPreceedingNodes(Predicate<Node> filter = null, int maxDepth = int.MaxValue,
            bool ignoreBreaks = false)
        {
            var nodes = new HashSet<Node>();
            CollectPreceedingNodes(nodes, filter ?? (n => true), maxDepth, ignoreBreaks);
            return new List<Node>(nodes);
        }

        internal void RegisterConsumer(Node consumer)
        {
            consumers.Add(consumer);
            ReinitializeParameters();
        }

        protected Node(IEnumerable<Node> producers, int dimension, string name)
        {
            this.producers = producers.ToArray();
            foreach (var producer in this.producers)
                producer.RegisterConsumer(this);
            outputNeedsUpdate = true;
            Dimension = dimension;
            this.name = name ?? NodeNameProvider.GetName(this.GetType());
        }

        protected (double mean, double deviation) ConsumerInputCharacteristics
        {
            get
            {
                double mean = 0, deviation = 0;
                foreach (var consumer in Consumers)
                {
                    var ic = consumer.InputCharacteristics;
                    mean += ic.mean;
                    deviation += ic.deviation;
                }
                mean /= Consumers.Count();
                deviation /= Consumers.Count();
                return (mean, deviation);
            }
        }

        protected abstract void ReinitializeParametersProtected();

        protected virtual void CollectPreceedingNodes(HashSet<Node> nodes, Predicate<Node> filter,
            int maxDepth = int.MaxValue, bool ignoreBreaks = false)
        {
            if (maxDepth <= 0 || !Producers.Any() || nodes.Contains(this)) return;
            foreach (var producer in Producers)
                producer.CollectPreceedingNodes(nodes, filter, maxDepth - 1, ignoreBreaks);
            if (filter(this))
                nodes.Add(this);
        }

        protected void SetNeedsEvaluation()
        {
            if (outputNeedsUpdate) return;
            outputNeedsUpdate = true;
            foreach (var consumer in consumers)
                consumer.SetNeedsEvaluation();
        }

        protected abstract Vector GetOutputProtected();

        protected abstract void GetErrorGradients(Vector error, out Vector[] inputErrors, out Vector[] parameterErrors);

        private void UpdateOutputIfNecessary()
        {
            if (!outputNeedsUpdate) return;
            output = GetOutputProtected();
            outputNeedsUpdate = false;
            gradientsNeedsUpdate = true;
        }

        private void UpdateErrorGradientsIfNecessary()
        {
            UpdateOutputIfNecessary();
            if (!gradientsNeedsUpdate) return;
            Vector error;
            if (Consumers.Any())
            {
                error = consumers[0].GetInputErrorGradient(this);
                for (var i = 1; i < consumers.Count; i++)
                    error.Add(consumers[i].GetInputErrorGradient(this));
            }
            else error = Vector.Zeros(Output.Dimension);
            GetErrorGradients(error, out inputErrorGradients, out parameterErrorGradients);
            Debug.Assert(inputErrorGradients.Length == producers.Length);
            gradientsNeedsUpdate = false;
        }

        public override string ToString() => name;
    }
}