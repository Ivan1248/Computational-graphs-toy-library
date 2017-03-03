using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinAlg;

namespace NeuralNetworks
{
    public abstract class ActivationLayer
    {
        private bool _trainMode = false;
        public Vector Out { get; protected set; }
        public Vector Gradient { get; protected set; }

        public int Dimension { get; private set; }

        public bool TrainMode
        {
            get { return _trainMode; }
            set { _trainMode = value; if (!value) Gradient = null; }
        }

        protected ActivationLayer(int dimension)
        {
            Dimension = dimension;
        }

        public abstract double PreferredInputMean { get; }
        public abstract double PreferredInputDeviation { get; }
        public abstract double OutputMean { get; }
        public abstract double OutputDeviation { get; }

        public abstract Vector PropagateForward(Vector s);
    }
}