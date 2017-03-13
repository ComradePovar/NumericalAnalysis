using System;
using System.Drawing;

namespace NumericalAnalysis
{
    public enum ReflectionRelation { Ox, Oy, O};
    public enum RotationDirection { Clockwise, CounterClockwise };
    public enum ScalingRelation { Ox, Oy, Both };
    public enum TranslationDirection { Left, Right, Up, Down };
    public class Matrix
    {
        private double[,] _matrix;
        
        public struct MatrixDimension
        {
            public int rowCount;
            public int columnCount;
            public MatrixDimension(int rowCount, int columnCount)
            {
                this.rowCount = rowCount;
                this.columnCount = columnCount;
            }
            public static bool operator ==(MatrixDimension left, MatrixDimension right)
            {
                return left.rowCount == right.rowCount && left.columnCount == right.columnCount;
            }
            public static bool operator !=(MatrixDimension left, MatrixDimension right)
            {
                return left.rowCount != right.rowCount || left.columnCount != right.columnCount;
            }
            public override bool Equals(object obj)
            {
                return ((MatrixDimension)obj).columnCount == columnCount &&
                       ((MatrixDimension)obj).rowCount == rowCount;
            }
            public override int GetHashCode()
            {
                return rowCount ^ columnCount % 17;
            }
        }
        public MatrixDimension Dimension { get; private set; }
        public double [,] Array
        {
            get
            {
                return _matrix;
            }
        }
        public double[][] JaggedArray
        {
            get
            {
                double[][] result = new double[Dimension.rowCount][];
                for (int i = 0; i < result.Length; i++)
                    result[i] = new double[Dimension.columnCount];

                for (int i = 0; i < _matrix.GetLength(0); i++)
                    for (int j = 0; j < _matrix.GetLength(1); j++)
                        result[i][j] = _matrix[i, j];

                return result;
            }
        }
        public Matrix Inversion
        {
            get
            {
                Matrix matrix = new Matrix(_matrix);
                matrix.Invert();
                return matrix;
            }
        }
        public double Determinant
        {
            get
            {
                if (Dimension.rowCount != Dimension.columnCount)
                    throw new NotImplementedException("кол-во столбцов не равно кол-ву строк");
                return getDeterminant();
            }
        }
        public bool HasInverse
        {
            get
            {
                return Dimension.rowCount == Dimension.columnCount;
            }
        }
        public bool IsSquare
        {
            get
            {
                return Dimension.rowCount == Dimension.columnCount;
            }
        }
        public bool IsIdentity
        {
            get
            {
                for (int i = 0; i < Dimension.rowCount; i++)
                    for (int j = i; j < Dimension.columnCount; j++)
                    {
                        if (i == j && _matrix[i, i] != 1)
                            return false;
                        if (i != j && (_matrix[i, j] != 0 || _matrix[j, i] != 0))
                            return false;
                    }

                return true;
            }
        }
        public double this[int i, int j]
        {
            get
            {
                return _matrix[i, j];
            }
            set
            {
                _matrix[i, j] = value;
            }
        }

        public Matrix()
        {}
        public Matrix(double[] vector)
        {
            _matrix = new double[vector.Length, 1];

            for (int i = 0; i < vector.Length; i++)
            {
                _matrix[i, 0] = vector[i];
            }

            Dimension = new MatrixDimension(vector.Length, 1);
        }
        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
            Dimension = new MatrixDimension(matrix.GetLength(0), matrix.GetLength(1));
        }
        public Matrix(MatrixDimension dimension)
        {
            Dimension = dimension;
            _matrix = new double[dimension.rowCount, dimension.columnCount];
        }
        public Matrix(Matrix value)
        {
            _matrix = value._matrix;
            Dimension = value.Dimension;
        }
        public Matrix(int rowCount, int columnCount)
        {
            Dimension = new MatrixDimension(rowCount, columnCount);
            _matrix = new double[rowCount, columnCount];
        }

        private Matrix getMinor(int iRow, int iColumn)
        {
            Matrix result = new Matrix(Dimension.rowCount - 1, Dimension.columnCount - 1);

            for (int i = 0, row = 0; i < Dimension.rowCount; i++)
            {
                if (i != iRow)
                {
                    for (int j = 0, col = 0; j < Dimension.columnCount; j++)
                    {
                        if (j != iColumn)
                        {
                            result[row, col++] = _matrix[i, j];
                        }
                    }

                    row++;
                }
            }
            return result;
        }
        private double getDeterminant()
        {
            //if (Dimension.rowCount == 1)
            //    return _matrix[0, 0];

            //double det = 0;
            //int sign = 1;
            //Matrix minor = this;

            //for (int i = 0; i < Dimension.rowCount; i++)
            //{
            //    minor = minor.getMinor(i, 0);
            //    det += sign * _matrix[i, 0] * minor.getDeterminant();
            //    sign = -sign;
            //}

            //return det;
            return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];

        }
        public void Invert()
        {
            //Не работает, fixed with G.O.V.N.O.K.O.D.
            //if (!IsSquare)
            //    throw new NotImplementedException("не квадратная");

            //double det = 0; //getDeterminant();
            //if (Dimension.rowCount == 1 && Dimension.columnCount == 1)
            //    det = this[0, 0];
            //else
            //    det = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];

            //if (det == 0)
            //    throw new NotImplementedException("вырожденная");

            //Matrix inversionMatrix = new Matrix(Dimension);

            //for (int i = 0; i < Dimension.rowCount; i++)
            //{
            //    for (int j = 0; j < Dimension.columnCount; j++)
            //    {
            //        inversionMatrix[i, j] = getMinor(i, j).getDeterminant();
            //        if ((i + j) % 2 != 0)
            //            inversionMatrix[i, j] = -inversionMatrix[i, j];
            //    }
            //}

            //inversionMatrix.Transpose();
            //_matrix = (1 / det * inversionMatrix)._matrix;
            if (this.Dimension.rowCount == 2 && this.Dimension.columnCount == 2)
            {
                double det = Determinant;
                double t = this[0, 0];
                this[0, 0] = this[1, 1] / det;
                this[0, 1] = -this[0, 1] / det;
                this[1, 0] = -this[1, 0] / det;
                this[1, 1] = t / det;
            }
            
        }
        public void Transpose()
        {
            double[,] newMatrix = new double[Dimension.columnCount, Dimension.rowCount];

            for (int i = 0; i < Dimension.columnCount; i++)
                for (int j = i; j < Dimension.rowCount; j++)
                {
                    newMatrix[i, j] = _matrix[j, i];
                }

            _matrix = newMatrix;
            Dimension = new MatrixDimension(Dimension.columnCount, Dimension.rowCount);
        }
        public void Scale(ScalingRelation relation, double scale)
        {
            Matrix scalingMatrix = null;
            switch (relation)
            {
                case ScalingRelation.Ox:
                    scalingMatrix = new Matrix(new double[,] { { scale, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
                    break;
                case ScalingRelation.Oy:
                    scalingMatrix = new Matrix(new double[,] { { 1, 0, 0 }, { 0, scale, 0 }, { 0, 0, 1 } });
                    break;
                case ScalingRelation.Both:
                    scalingMatrix = new Matrix(new double[,] { { scale, 0, 0 }, { 0, scale, 0 }, { 0, 0, 1 } });
                    break;
            }
            _matrix = (scalingMatrix * this)._matrix;
        }
        public void Rotate(double angle, RotationDirection direction)
        {
            Matrix rotationMatrix = null;
            switch (direction)
            {
                case RotationDirection.Clockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { Math.Cos(angle * Math.PI / 180), Math.Sin(angle * Math.PI / 180), 0 },
                        { -Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180), 0},
                        { 0 , 0, 1}
                        });
                    break;
                case RotationDirection.CounterClockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { Math.Cos(angle * Math.PI / 180), -Math.Sin(angle * Math.PI / 180), 0 },
                        { Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180), 0},
                        { 0 , 0, 1}
                        });
                    break;
            }              
            _matrix = (rotationMatrix * this)._matrix;
        }
        public void Reflect(Matrix point1, Matrix point2)
        {
            double A = point2.ToPointF().X - point1.ToPointF().X;
            double B = point2.ToPointF().Y - point1.ToPointF().Y;
            //Перенос на (-x1, -y1)
            Translate(new Matrix(new double[] { -point1.ToPointF().X, -point1.ToPointF().Y, 1 })                            );
            //Поворот на угол а
            Rotate((A / Math.Sqrt(A * A + B * B)), (B / Math.Sqrt(A * A + B * B)), RotationDirection.Clockwise);
            //Отражение по оси х
            Reflect(ReflectionRelation.Ox);
            //Поворот на -а
            Rotate((A / Math.Sqrt(A * A + B * B)), (B / Math.Sqrt(A * A + B * B)), RotationDirection.CounterClockwise);
            //Перенос на (x1, y1)
            Translate(new Matrix(new double[] { point1.ToPointF().X, point1.ToPointF().Y, 1 }));           
        }
        public void Rotate(double cos, double sin, RotationDirection direction)
        {
            Matrix rotationMatrix = null;
            switch (direction)
            {
                case RotationDirection.Clockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { cos, sin, 0 },
                        { -sin, cos, 0 },
                        { 0 , 0, 1 }
                        });
                    break;
                case RotationDirection.CounterClockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { cos, -sin, 0 },
                        { sin, cos, 0 },
                        { 0 , 0, 1 }
                        });
                    break;
            }
            _matrix = (rotationMatrix * this)._matrix;
        }
        public void RotateAroundPoint(PointF point, double angle, RotationDirection direction)
        {
            Matrix rotationMatrix = null;
            switch (direction)
            {
                case RotationDirection.Clockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { Math.Cos(angle * Math.PI / 180), Math.Sin(angle * Math.PI / 180), point.X },
                        { -Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180), point.Y },
                        { 0 , 0, 1}
                        });
                    break;
                case RotationDirection.CounterClockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { Math.Cos(angle * Math.PI / 180), -Math.Sin(angle * Math.PI / 180), point.X },
                        { Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180), point.Y },
                        { 0 , 0, 1}
                        });
                    break;
            }
            this[0, 0] -= point.X * this[2, 0];
            this[1, 0] -= point.Y * this[2, 0];
            _matrix = (rotationMatrix * this)._matrix;
        }
        public void RotateAroundPoint(PointF point, double cos, double sin, RotationDirection direction)
        {
            Matrix rotationMatrix = null;
            switch (direction)
            {
                case RotationDirection.Clockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { cos, sin, point.X },
                        { -sin, cos, point.Y },
                        { 0 , 0, 1 }
                        });
                    break;
                case RotationDirection.CounterClockwise:
                    rotationMatrix = new Matrix(new double[,] {
                        { cos, -sin, point.Y },
                        { sin, cos, point.X },
                        { 0 , 0, 1 }
                        });
                    break;
            }
            this[0, 0] -= point.X * this[2, 0];
            this[1, 0] -= point.Y * this[2, 0];
            _matrix = (rotationMatrix * this)._matrix;
        }
        public void Translate(Matrix vector)
        {
            if (vector.Dimension.rowCount != 3 || vector.Dimension.columnCount != 1)
                throw new NotImplementedException("не вектор");

            Matrix translationMatrix = new Matrix(new double[,]
            {
                { 1, 0, vector[0, 0] },
                { 0, 1, vector[1, 0] },
                { 0, 0, 1 }
            });
            _matrix = (translationMatrix * this)._matrix;
        }
        public void Reflect(ReflectionRelation relation)
        {
            Matrix reflectionMatrix = null;
            switch (relation)
            {
                case ReflectionRelation.Ox:
                    reflectionMatrix = new Matrix(new double[,] { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } });
                    break;
                case ReflectionRelation.Oy:
                    reflectionMatrix = new Matrix(new double[,] { { -1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
                    break;
                case ReflectionRelation.O:
                    reflectionMatrix = new Matrix(new double[,] { { -1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } });
                    break;
            }
            _matrix = (reflectionMatrix * this)._matrix;
        }
        public PointF ToPointF()
        {
            if (Dimension.rowCount != 3 || Dimension.columnCount != 1)
                throw new NotImplementedException("не вектор");

            return new PointF((float)(_matrix[0, 0] / _matrix[2, 0]),
                              (float)(_matrix[1, 0] / _matrix[2, 0]));
        }
        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left.Dimension != right.Dimension)
                throw new NotImplementedException("разные размерности");

            Matrix result = new Matrix(left.Dimension);

            for (int i = 0; i < result.Dimension.rowCount; i++)
            {
                for (int j = 0; j < result.Dimension.columnCount; j++)
                {
                    result[i, j] = left[i, j] + right[i, j];
                }
            }

            return result;
        }
        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left.Dimension != right.Dimension)
                throw new NotImplementedException("разные размерности");

            Matrix result = new Matrix(left.Dimension);

            for (int i = 0; i < result.Dimension.rowCount; i++)
            {
                for (int j = 0; j < result.Dimension.columnCount; j++)
                {
                    result[i, j] = left[i, j] - right[i, j];
                }
            }

            return result;
        }
        public static Matrix operator -(Matrix value)
        {
            Matrix result = new Matrix(value.Dimension);

            for (int i = 0; i < result.Dimension.rowCount; i++)
            {
                for (int j = 0; j < result.Dimension.columnCount; j++)
                {
                    result[i, j] = -value[i, j];
                }
            }

            return result;
        }
        public static Matrix test(Matrix left, Matrix right)
        {
            if (left.Dimension.columnCount != right.Dimension.rowCount)
                throw new NotImplementedException("кол-во столбцов left должно быть равно кол-ву строк right");

            Matrix result = new Matrix(left.Dimension.rowCount, right.Dimension.columnCount);

            for (int i = 0; i < left.Dimension.rowCount; i++)
            {
                for (int j = 0; j < right.Dimension.columnCount; j++)
                {
                    for (int k = 0; k < right.Dimension.rowCount; k++)
                    {
                        result[i, j] += left[i, k] * right[k, j];
                    }
                }
            }

            return result;
        }
        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.Dimension.columnCount != right.Dimension.rowCount)
                throw new NotImplementedException("кол-во столбцов left должно быть равно кол-ву строк right");

            Matrix result = new Matrix(left.Dimension.rowCount, right.Dimension.columnCount);

            for (int i = 0; i < left.Dimension.rowCount; i++)
            {
                for (int j = 0; j < right.Dimension.columnCount; j++)
                {
                    for (int k = 0; k < right.Dimension.rowCount; k++)
                    {
                        result[i, j] += left[i, k] * right[k, j];
                    }
                }
            }

            return result;
        }
        public static Matrix operator *(Matrix left, double right)
        {
            Matrix result = new Matrix(left.Dimension);

            for (int i = 0; i < result.Dimension.rowCount; i++)
            {
                for (int j = 0; j < result.Dimension.columnCount; j++)
                {
                    result[i, j] = left[i, j] * right;
                }
            }

            return result;
        }
        public static Matrix operator *(double left, Matrix right)
        {
            Matrix result = new Matrix(right.Dimension);

            for (int i = 0; i < result.Dimension.rowCount; i++)
            {
                for (int j = 0; j < result.Dimension.columnCount; j++)
                {
                    result[i, j] = left * right[i, j];
                }
            }

            return result;
        }
        public static Matrix operator /(Matrix left, Matrix right)
        {
            return left * right.Inversion;
        }
        public static Matrix operator /(Matrix left, double right)
        {
            return left * (1 / right);
        }
        public static Matrix operator /(double left, Matrix right)
        {
            return left * right.Inversion;
        }
        public override bool Equals(object obj)
        {
            Matrix value = (Matrix)obj;

            if (Dimension != value.Dimension)
                return false;
            for (int i = 0; i < Dimension.rowCount; i++)
                for (int j = 0; j < Dimension.columnCount; j++)
                    if (_matrix[i, j] != value._matrix[i, j])
                        return false;

            return true;
        }
    }
}
