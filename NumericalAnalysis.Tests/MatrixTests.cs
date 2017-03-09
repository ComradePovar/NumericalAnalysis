using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericalAnalysis;

namespace NumericalAnalysis.Tests
{

    [TestClass]
    public class MatrixTests
    {

        [TestMethod]
        public void Inversion_Compare_With_Example()
        {
            Matrix A = new Matrix(new double[,] { { 5, 7 }, { 2, -10 } });

            Matrix expected = new Matrix(new double[,] { { 10D / 64, 7D / 64 }, { 2D / 64, -5 / 64D } });

            Matrix actual = A.Inversion;

            Assert.AreEqual(expected, actual);
            //for (int i = 0; i < A.Dimension.rowCount; i++)
            //    for (int j = 0; j < A.Dimension.columnCount; j++)
            //        Assert.AreEqual(expected[i, j], actual[i, j]);
        }
        [TestMethod]
        public void getDeterminant_Compare_With_Example()
        {
            Matrix A = new Matrix(new double[,] { { 5, 7 }, { 2, -10 } });

            double expected = -64;
            double actual = A.Determinant;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sub_Compare_WithExample()
        {
            Matrix A = new Matrix(new double[,] { { 5, 7 }, { 2, -10 } });

            Matrix B = new Matrix(new double[,] { { 2, 3 }, { 0, 10 } });
            Matrix expected = new Matrix(new double[,] { { 3, 4 }, { 2, -20 } });

            Matrix actual = A - B;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Mul_Compare_WithExample()
        {
            Matrix A = new Matrix(new double[,] { { 5, 7 }, { 2, -10 } });

            Matrix B = new Matrix(new double[,] { { 2, 3 }, { 0, 10 } });
            Matrix expected = new Matrix(new double[,] { { 10, 85 }, { 4, -94 } });

            Matrix actual = A * B;

            Assert.AreEqual(expected, actual);
        }
    }
}
