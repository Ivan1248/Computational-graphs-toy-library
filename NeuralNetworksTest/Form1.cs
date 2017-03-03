using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using LinAlg;
using NeuralNetworks;
using OxyPlot;
using OxyPlot.Series;
using ComputationalGraphs;

namespace NeuralNetworksTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }

        private GraphLearningMachine ann = CreateNet2();

        /*private static ILearningMachine CreateNet1() => new NeuralNetwork(
            inputDimension: 1,
            activationLayers: new ActivationLayer[] { new TanhLayer(9), new LinearLayer(1) },
            //optimizer: new GradientDescentOptimizer<ParameterContainer>(0.001),
            optimizer: new MomentumRMSPropOptimizer<ParameterContainer>(0.0003, gamma: 0.999, momentum: 0.9995),
            lossFunction: LossFunctions.SquaredLoss,
            minibatchSize: 10);*/

        private static GraphLearningMachine CreateNet2()
        {
            InputNode inputNode = Nodes.Input(1), labelNode = Nodes.Input(1);
            var annOutputNode = inputNode.FC(9).Tanh().FC(1);
            var lossNode = annOutputNode.SquaredLoss(labelNode);
            var trainer = new Trainer(
                inputNode: inputNode,
                labelNode: labelNode,
                lossNode: lossNode,
                optimizer: new MomentumRMSPropOptimizer(0.00000001, gamma: 0.999, momentum: 0.9995),
                nodes: annOutputNode.CollectPreceedingNodes(filter: n => n.Parameters.Length > 0),
                X: new List<Vector>(), Y: new List<Vector>(),
                batchSize: 10
            );
            return new GraphLearningMachine(trainer);
        }


        /* private NeuralNetwork ann = new NeuralNetwork(
             inputDimension: 1,
             activationLayers: new Activationlayer[] { new LogisticLayer(8), new LogisticLayer(8), new SoftmaxLayer(1), },
             optimizer: new MomentumRMSPropOptimizer<ParameterContainer>(0.0005, momentum: 0.99),
             //optimizer: new GradientDescentOptimizer<ParameterContainer>(0.01), 
             lossFunction: LossFunctions.SquaredLoss,
             minibatchSize: 16);*/

        private List<Vector> x = new List<Vector>();
        private List<Vector> y = new List<Vector>();

        private ScatterSeries scat = new ScatterSeries();
        private PlotModel plot = new PlotModel {};

        //private Func<double, double> f = Math.Cos;
        private Func<double, double> f = x => 0.2 * Math.Sin(x) + 0.2 * Math.Sin(4 * x + Math.PI / 7);
        //private Func<double, double> f = x => Math.Pow(x-3, 2);

        private void Form1_Load(object sender, EventArgs e)
        {
            plot.Series.Add(new FunctionSeries(f, 0, 10, 0.1, "cos(x)"));
            plot.Series.Add(scat);
            plot.Series.Add(new FunctionSeries(x => ann.Predict(Vector.Filled(1, x))[0], 0, 10, 0.1, "ann(x)"));
            RefreshPlotAndInfo();
            plot.MouseDown += (s, ev) =>
            {
                var p = scat.InverseTransform(ev.Position);
                switch (ev.ChangedButton)
                {
                    case OxyMouseButton.Left:
                        AddPoint(p.X, p.Y);
                        break;
                    case OxyMouseButton.Right:
                        RemovePoint(p.X, p.Y, 5);
                        break;
                    default:
                        return;
                }
                RefreshPlotAndInfo();
            };

            this.plot1.Model = plot;
        }

        private void RefreshPlotAndInfo()
        {
            //if (plot.Series.Count > 2)
            plot.Series.RemoveAt(2);
            //plot.Series.Add(new FunctionSeries(x => ann.Predict(Vector.Filled(1, x))[0], 0, 10, 0.1, "ann(x)"));
            double xmin = 0, xmax = 10;
            if (plot.DefaultXAxis != null)
            {
                xmin = plot.DefaultXAxis.ActualMinimum;
                xmax = plot.DefaultXAxis.ActualMaximum;
            }
            var s = (xmax - xmin) / 200;
            plot.Series.Add(new FunctionSeries(x => ann.Predict(Vector.Filled(1, x))[0], xmin + 2 * s, xmax - 2 * s, s,
                "ann(x)"));
            plot.InvalidatePlot(true);
            errorLabel.Text = $"Error: {ann.GetError(x, y).ToString("0.0000e+00")}";
            epochsCompletedLabel.Text = $"Epochs: {CompletedEpochCount}";
        }

        private void AddPoint(double x, double y)
        {
            this.x.Add(Vector.New(x));
            this.y.Add(Vector.New(y));
            scat.Points.Add(new ScatterPoint(x, y, 2));
        }

        private void RemovePoint(double x, double y, double tol = 5)
        {
            DataPoint p1 = scat.InverseTransform(new ScreenPoint(1, 1)),
                p2 = scat.InverseTransform(new ScreenPoint(0, 0));
            double dx = Math.Abs(p1.X - p2.X), dy = Math.Abs(p1.Y - p2.Y);
            for (int i = 0; i < this.x.Count; i++)
            {
                if (Vector.New((x - this.x[i][0]) / dx, (y - this.y[i][0]) / dy).Norm() > 10) continue;
                this.x.RemoveAt(i);
                this.y.RemoveAt(i);
                this.scat.Points.RemoveAt(i);
                return;
            }
        }

        private int CompletedEpochCount { get; set; } = 0;

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ann.ReinitializeParameters();
            CompletedEpochCount = 0;
            RefreshPlotAndInfo();
        }

        private bool Training
        {
            get { return _training; }
            set
            {
                _training = value;
                trainButton.Text = value ? "Stop" : "Train";
            }
        }

        private bool _training = false;

        private void TrainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(Training = !Training)) return;
            long t = DateTime.Now.Ticks;
            for (int i = 0; Training && i < int.Parse(epochCountTextbox.Text); i++, CompletedEpochCount++)
            {
                ann.TrainEpoch(x, y);
                long t1 = DateTime.Now.Ticks;
                if (t1 - t < 1000000) continue;
                t = t1;
                RefreshPlotAndInfo();
                Application.DoEvents();
            }
            Training = false;
        }

        private void ClearDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x.Clear();
            y.Clear();
            scat.Points.Clear();
            RefreshPlotAndInfo();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Training = false;
        }

        private void SampleFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double d = 2 * Math.PI / 200;
            for (int i = 0; i < 200; i++)
            {
                double x = i * d;
                AddPoint(x, f(x));
            }
            RefreshPlotAndInfo();
        }
    }
}