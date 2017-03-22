using System;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;

namespace NumericalAnalysis.WinForms
{
    public partial class MainForm : Form
    {
        private PlotModel model1;
        private PlotModel model2;
        private ODE[] odes;
        private double lowerBound;
        private double upperBound;
        Func<double, double> firstFunction;
        Func<double, double> secondFunction;

        public MainForm()
        {
            InitializeComponent();
            firstFunction = x => Math.Pow(Math.E, Math.Pow(x, 2));
            //firstFunction = x => x * x + 2;
            secondFunction = x => 0.5 * Math.Pow(Math.E, -Math.Pow(x, 2));
            odes = new ODE[]
            {
                // My task
                new ODE(
                    delegate(double x, double[] y)
                    {
                        return x/y[1];
                    }, 1, Math.E,
                    delegate(double x, double[] y)
                    {
                        return 1 / y[1];
                    },
                    delegate(double x, double[] y)
                    {
                        return 0;
                    },
                    delegate(double x, double[] y)
                    {
                        return -x / Math.Pow(y[1], 2);
                    }),
                new ODE(
                    delegate(double x, double[] y)
                    {
                        return -x/y[0];
                    }, 1, 1/(2*Math.E),
                    delegate(double x, double[] y)
                    {
                        return -1 / y[0];
                    },
                    delegate(double x, double[] y)
                    {
                        return x / Math.Pow(y[0], 2);
                    },
                    delegate(double x, double[] y)
                    {
                        return 0;
                    })

                // -------------------------------------

                // Example
                //new ODE(
                //    delegate(double x, double[] y)
                //    {
                //        return -y[0] + x * x + 2 * x + 2;
                //    }, 0, 2,
                //    delegate(double x, double[] y)
                //    {
                //        return 2 * x + 2;
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return -1;
                //    }),

                // -------------------------------------

                // another task 1(Женя)
                //new ODE(
                //    delegate(double x, double[] y)
                //    {
                //        return y[1]/x;
                //    }, 1, 0.5,
                //    delegate(double x, double[] y)
                //    {
                //        return -y[1]/Math.Pow(x, 2);
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return 0;
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return 1/x;
                //    }),
                //new ODE(
                //    delegate(double x, double[] y)
                //    {
                //        return (y[1] * (y[0] + 2 * y[1] - 1))/(x * (y[0] - 1));
                //    }, 1, 0.25,
                //    delegate(double x, double[] y)
                //    {
                //        return (-y[1] * (y[0] + 2 * y[1] - 1))/(Math.Pow(x, 2) * (y[0] - 1));
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return ((y[1] * (x * y[0] - x) - (y[1] * y[0] + 2 * y[1] * y[1] - 1) * x) / Math.Pow(x * y[0] - x, 2));
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return (y[0] + 4 * y[1] - 1) / (x * y[0] - x);
                //    })

                // ----------------------------------------------------------------

                // another task 2(Сергей)
                //new ODE(
                //    delegate(double x, double[] y)
                //    {
                //        return Math.Pow(y[0], 2) / (2 * y[1]) - y[1]/2 + 1/(2 * y[1]);
                //    }, 0, (double)-3/4,
                //    delegate(double x, double[] y)
                //    {
                //        return 0;
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return y[0] / y[1];
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return -Math.Pow(y[0], 2)/(2 * Math.Pow(y[1], 2)) - 0.5 - 1 / (2 * Math.Pow(y[1], 2));
                //    }),
                //new ODE(
                //    delegate(double x, double[] y)
                //    {
                //        return y[1] + y[0];
                //    }, 0, (double)5/4,
                //    delegate(double x, double[] y)
                //    {
                //        return 0;
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return 1;
                //    },
                //    delegate(double x, double[] y)
                //    {
                //        return 1;
                //    })

                // ---------------------------------------
            };
            //lowerBound = 0;
            //upperBound = 1;
            lowerBound = 1;
            upperBound = 2;
            model1 = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
            };
            model2 = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
            };
            plot1.Model = model1;
            plot2.Model = model2;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            int N = int.Parse(tbN.Text);

            double[][] resultExplicit = EulerMethod.SolveExplicit(odes, lowerBound, upperBound, N);
            double[][] resultImplicit = EulerMethod.SolveImplicit(odes, lowerBound, upperBound, N);
            double x = lowerBound;
            double h = (upperBound - lowerBound) / N;

            LineSeries firstExplicit = new LineSeries();
            LineSeries secondExplicit = new LineSeries();
            LineSeries firstImplicit = new LineSeries();
            LineSeries secondImplicit = new LineSeries();
            FunctionSeries firstExact = new FunctionSeries(firstFunction, lowerBound, upperBound, h, "y = e^(x^2)");
            FunctionSeries secondExact = new FunctionSeries(secondFunction, lowerBound, upperBound, h, "z = 1/2 * e^(-x^2)");


            firstExplicit.Title = "Явное решение y(нет)";
            secondExplicit.Title = "Явное решение z(нет)";
            firstImplicit.Title = "Неявное решение y(нет)";
            secondImplicit.Title = "Неявное решение z(нет)";

            for (int i = 0; i < resultExplicit.Length; i++, x += h)
            {
                for (int j = 0; j < resultExplicit[i].Length; j++)
                {
                    firstExplicit.Points.Add(new DataPoint(x, resultExplicit[i][0]));
                    secondExplicit.Points.Add(new DataPoint(x, resultExplicit[i][1]));

                    firstImplicit.Points.Add(new DataPoint(x, resultImplicit[i][0]));
                    secondImplicit.Points.Add(new DataPoint(x, resultImplicit[i][1]));
                }
            }


            model1.Series.Clear();
            model2.Series.Clear();
            model1.Series.Add(firstExplicit);
            model2.Series.Add(secondExplicit);
            model1.Series.Add(firstImplicit);
            model2.Series.Add(secondImplicit);
            model1.Series.Add(firstExact);
            model2.Series.Add(secondExact);
            plot1.InvalidatePlot(true);
            plot2.InvalidatePlot(true);
        }
    }
}
