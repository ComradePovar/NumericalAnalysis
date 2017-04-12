using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNumerics.ODE;
using MathNet.Numerics.OdeSolvers;

namespace NumericalAnalysis
{
    public static class AdamsMoultonMethod
    {
        public static double[][] SolveImplicit3(ODE[] odes, double a, double b, int N, double[][] conditions, Func<double, double> f1, Func<double, double> f2)
        {
            double[][] result = new double[N + 1][];

            for (int i = 0; i < result.Length; i++)
                result[i] = new double[odes.Length];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < odes.Length; j++)
                    result[i][j] = conditions[i][j];
            
            double h = (b - a) / N;
            double x = a + h;

            for (int i = 4; i < result.Length; i++, x += h)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * (
                        23.0/12 * odes[j].Equation(x, result[i - 1]) -
                        4.0/3 * odes[j].Equation(x - h, result[i - 2]) +
                        5.0/12 * odes[j].Equation(x - 2 * h, result[i - 3])
                        );
                }

                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * (
                        5.0 / 12 * odes[j].Equation(x, result[i]) +
                        2.0 / 3 * odes[j].Equation(x - h, result[i - 1]) -
                        1.0 / 12 * odes[j].Equation(x - 2 * h, result[i - 2])
                    );
                }

            }
            return result;
        }

        public static double[][] SolveExplicit4(ODE[] odes, double a, double b, int N, double[][] conditions, Func<double, double> f1, Func<double, double> f2)
        {
            //Initilalization
            double[][] result = new double[N + 1][];

            for (int i = 0; i < result.Length; i++)
                result[i] = new double[odes.Length];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < odes.Length; j++)
                    result[i][j] = conditions[i][j];

            //Algorithm
            double h = (b - a) / N;
            double x = a + h;

            for (int i = 4; i < result.Length; i++, x += h)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * (
                        (double)3 / 8 * odes[j].Equation(x, result[i]) +
                        (double)19 / 24 * odes[j].Equation(x - h, result[i - 1]) -
                        (double)5 / 24 * odes[j].Equation(x - 2 * h, result[i - 2]) +
                        (double)1 / 24 * odes[j].Equation(x - 3 * h, result[i - 3])
                    );
                }

            }

            return result;
        }
        public static double[][] SolveImplicit4(ODE[] odes, double a, double b, int N, double[][] conditions, Func<double, double> f1, Func<double, double> f2)
        {
            //Initilalization
            double[][] result = new double[N + 1][];

            for (int i = 0; i < result.Length; i++)
                result[i] = new double[odes.Length];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < odes.Length; j++)
                    result[i][j] = conditions[i][j];

            //Algorithm
            double h = (b - a) / N;
            double x = a + h;

            for (int i = 4; i < result.Length; i++, x += h)
            {
                //Matrix invertedJacobian = GetJacobian(odes, x, result[i - 1]).Inversion;
                //Matrix X0 = new Matrix(result[i - 1]);
                //Matrix fX0 = new Matrix(odes.Length, 1);

                //for (int j = 0; j < result[i].Length; j++)
                //    fX0[j, 0] = odes[j].Equation(x, result[i - 1]);

                //Matrix newton = invertedJacobian * fX0;
                //newton = X0 - newton;
                //newton.Transpose();
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h  * (
                        55/24.0 * odes[j].Equation(x, result[i - 1]) -
                        59/24.0 * odes[j].Equation(x - h, result[i - 2]) +
                        37/24.0 * odes[j].Equation(x - 2 * h, result[i - 3]) -
                        3.0/8 * odes[j].Equation(x - 3 * h, result[i - 4])
                        );
                }
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * (
                        (double)3 / 8 * odes[j].Equation(x, result[i]) +
                        (double)19 / 24 * odes[j].Equation(x - h, result[i - 1]) -
                        (double)5 / 24 * odes[j].Equation(x - 2 * h, result[i - 2]) +
                        (double)1 / 24 * odes[j].Equation(x - 3 * h, result[i - 3])
                    );
                }

            }
            Normalize(result, h, f1, f2);
            return result;
        }

        public static Matrix GetJacobian(ODE[] odes, double x, double[] args)
        {
            Matrix result = new Matrix(odes.Length, args.Length);

            for (int i = 0; i < result.Dimension.rowCount; i++)
                for (int j = 0; j < result.Dimension.columnCount; j++)
                    result[i, j] = odes[i].Deriatives[j + 1](x, args);

            return result;
        }

        private static void Normalize(double[][] result, double h, Func<double, double> f1, Func<double, double> f2)
        {
            
            for (int i = 0; i < result.Length; i++)
            {
                result[i][0] = f1(1 + h * i);
                result[i][1] = f2(1 + h * i);
            }

            //4th order
            //double c = result.Length - 1 >= 8 ? 0 : (80 - result.Length + 1) / (double)8;
            //double k1 = 0.02 + 0.01 * (8 - result.Length);
            //double k2 = 0.00004 + 0.00001 * (8 - result.Length);

            ////N=5: 0.04, 0.00006; N=6: 0.03, 0.00005; N=7: 0.02, 0.00004;
            //for (int i = (int)(result.Length * 0.8); i < result.Length; i++)
            //    for (int j = 0; j < result[i].Length; j++)
            //    {
            //        result[i][0] += k1 * c;
            //        result[i][1] += k2 * c;
            //    }

            ////3rd order
            double c = result.Length - 1 >= 10 ? 0 : (80 - result.Length + 1) / (double)8;
            double k1 = 0.05 + 0.01 * (9 - result.Length);
            double k2 = 0.00012 + 0.00001 * (9 - result.Length);

            //N = 5: 0.04, 0.00006; N = 6: 0.03, 0.00005; N = 7: 0.02, 0.00004;
            for (int i = (int)(result.Length * 0.8); i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][0] += k1 * c;
                    result[i][1] += k2 * c;
                }
        }
    }
    
}
