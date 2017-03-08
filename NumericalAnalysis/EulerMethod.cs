using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    public static class EulerMethod
    {
        public static double[][] SolveExplicit(ODE[] odes, double a, double b, int N)
        {
            //Initialization
            double[][] result = new double[N + 1][];

            for (int i = 0; i < result.Length; i++)
                result[i] = new double[odes.Length];
            for (int i = 0; i < odes.Length; i++)
                result[0][i] = odes[i].y0;

            //Algorithm
            double h = (b - a) / N;
            double x = a;

            for (int i = 1; i < result.Length; i++, x += h)
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * odes[j].Equation(x, result[i - 1]);
                }

            return result;
        }

        public static double[][] SolveImplicit(ODE[] odes, double a, double b, int N)
        {
            //Initilalization
            double[][] result = new double[N + 1][];

            for (int i = 0; i < result.Length; i++)
                result[i] = new double[odes.Length];
            for (int i = 0; i < odes.Length; i++)
                result[0][i] = odes[i].y0;

            //Algorithm
            double h = (b - a) / N;
            double x = a + h;

            for (int i = 1; i < result.Length; i++, x += h)
            {
                double[] newton = NewtonMethod(odes, result[i - 1], x);

                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * odes[j].Equation(x, newton);
                }
            }

            //throw new NotImplementedException();
            return result;
        }

        private static double[] NewtonMethod(ODE[] odes, double[] initConditions, double arg)
        {
            Matrix invertedJacobian = GetJacobian(odes, arg, initConditions).Inversion;
            Matrix X0 = new Matrix(initConditions);
            Matrix fX0 = new Matrix(odes.Length, 1);

            for (int j = 0; j < initConditions.Length; j++)
                fX0[j, 0] = odes[j].Equation(arg, initConditions);

            Matrix newton = (X0 - invertedJacobian * fX0);
            newton.Transpose();

            return newton.JaggedArray[0];
        }
        public static Matrix GetJacobian(ODE[] odes, double x, double[] args)
        {
            Matrix result = new Matrix(odes.Length, args.Length);

            for (int i = 0; i < result.Dimension.rowCount; i++)
                for (int j = 0; j < result.Dimension.columnCount; j++)
                    result[i, j] = odes[i].Deriatives[j + 1](x, args); //Значение х???

            return result;
        }
    }
}
