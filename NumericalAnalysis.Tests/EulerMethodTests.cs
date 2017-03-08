using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericalAnalysis;

namespace NumericalAnalysis.Tests
{
    [TestClass]
    public class EulerMethodTests
    {
        [TestMethod]
        public void SolveExplicit_Compare_With_Example()
        {
            //Arrange
            Equation func = delegate (double x, double[] y)
            {
                return -y[0] + x * x + 2 * x + 2;
            };
            ODE[] ode = new ODE[] { new ODE(func, 0, 2) };

            double a = 0;
            double b = 1;
            int N = 4;
            double h = (b - a) / N;
            double[][] expected = new double[N + 1][];
            double step = a;
            expected[0] = new double[] { 2 };
            expected[1] = new double[] { 2 };
            expected[2] = new double[] { 2.140625 };
            expected[3] = new double[] { 2.41796875 };
            expected[4] = new double[] { 2.8291015625 };

            //Act
            double[][] actual = EulerMethod.SolveExplicit(ode, a, b, N);

            //Assert
            for (int i = 0; i <= N; i++)
                for (int j = 0; j < ode.Length; j++)
                    Assert.AreEqual(expected[i][j], actual[i][j]);
        }
    }
}
