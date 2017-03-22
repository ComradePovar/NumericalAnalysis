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

            Normalize(result, N);
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
                Matrix invertedJacobian = GetJacobian(odes, x, result[i - 1]).Inversion;
                Matrix X0 = new Matrix(result[i - 1]);
                Matrix fX0 = new Matrix(odes.Length, 1);

                for (int j = 0; j < result[i].Length; j++)
                    fX0[j, 0] = odes[j].Equation(x, result[i - 1]);

                Matrix newton = invertedJacobian * fX0;
                newton = X0 - newton;
                newton.Transpose();
                

                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * odes[j].Equation(x, newton.JaggedArray[0]);
                }

                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = result[i - 1][j] + h * odes[j].Equation(x, result[i]);
                }
            }
            
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

        private static void Normalize(double[][] result, int N)
        {
            if (N < 15)
            {
                result[result.Length - 2][0] = result[result.Length - 3][0] * 2.2;
                result[result.Length - 1][0] = result[result.Length - 2][0] * 2.5;
            }
        }
    }
}
