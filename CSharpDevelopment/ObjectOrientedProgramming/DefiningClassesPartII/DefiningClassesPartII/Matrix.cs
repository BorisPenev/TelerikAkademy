using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClassesPartII
{
    public class Matrix<T>
    {
        private T[,] martix;
        private T[,] Martix
        {
            get { return martix; }
            set { martix = value; }
        }

        private readonly int length = 0;
        public int Length
        {
            get { return this.Martix.Length; }
        }

        public Matrix(int rows, int cols)
        {
            if ((typeof(T) == typeof(int) ||
                typeof(T) == typeof(long) ||
                typeof(T) == typeof(double) ||
                typeof(T) == typeof(decimal) ||
                typeof(T) == typeof(float)) &&
                rows == cols)
                this.Martix = new T[rows, cols];
            else
                throw new InvalidCastException("Unallowed type."+ Environment.NewLine+"Use only int, long, double, decimal or float. Or rows is not equal to cols");
        }

        public T this[int row, int col]
        {
            get
            {
                return Martix[row, col];
            }
            set
            {
                Martix[row, col] = value;
            }
        }

        public static Matrix<T> operator -(Matrix<T> m)
        {
            return Multiply(-1, m);
        }

        public static Matrix<T> operator -(Matrix<T> m1, Matrix<T> m2)
        {
            return Add(m1, -m2);
        }

        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            return Add(m1, m2);
        }

        public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
        {
            return Multiply(m1, m2);
        }

        public static bool operator true(Matrix<T> m)
        {
            if (m == null || m.Length == 0)
            {
                return false;
            }

            for (int row = 0; row < m.Length / 2; row++)
            {
                for (int col = 0; col < m.Length / 2; col++)
                {
                    if (m[row, col].Equals(0))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool operator false(Matrix<T> m)
        {
            if (m == null || m.Length == 0)
            {
                return true;
            }

            for (int row = 0; row < m.Length / 2; row++)
            {
                for (int col = 0; col < m.Length / 2; col++)
                {
                    if (m[row, col].Equals(0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static Matrix<T> Multiply(int c, Matrix<T> m)
        {
            if (m == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                Matrix<T> result = new Matrix<T>(m.Length / 2, m.Length / 2);
                for (int row = 0; row < m.Length / 2; row++)
                {
                    for (int col = 0; col < m.Length / 2; col++)
                    {
                        checked
                        {
                            result[row, col] = (dynamic)m[row, col] * c;
                        }
                    }
                }

                return result;
            }
            catch (OverflowException ex)
            {
                throw new OverflowException(ex.Message);
            }
        }

        private static Matrix<T> Multiply(Matrix<T> m1, Matrix<T> m2)
        {
            CheckMatrix(m1, m2);

            try
            {
                Matrix<T> result = new Matrix<T>(m1.Length / 2, m1.Length / 2);
                for (int row = 0; row < m1.Length / 2; row++)
                {
                    for (int col = 0; col < m1.Length / 2; col++)
                    {
                        checked
                        {
                            result[row, col] = (dynamic)m1[row, col] * m2[row, col];
                        }
                    }
                }
                return result;
            }
            catch (OverflowException ex)
            {
                throw new OverflowException(ex.Message);
            }
        }

        private static Matrix<T> Add(Matrix<T> m1, Matrix<T> m2)
        {
            CheckMatrix(m1, m2);

            try
            {
                Matrix<T> result = new Matrix<T>(m1.Length / 2, m1.Length / 2);
                for (int row = 0; row < m1.Length / 2; row++)
                {
                    for (int col = 0; col < m1.Length / 2; col++)
                    {
                        checked
                        {
                            result[row, col] = (dynamic)m1[row, col] + m2[row, col];
                        }
                    }
                }

                return result;
            }
            catch (OverflowException ex)
            {
                throw new OverflowException(ex.Message);
            }
        }

        private static void CheckMatrix(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1 == null || m2 == null)
            {
                throw new NullReferenceException();
            }

            if (m1.length != m2.length)
            {
                throw new InvalidOperationException();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < this.Martix.Length / 2; row++)
            {
                for (int col = 0; col < this.Martix.Length / 2; col++)
                {
                    sb.Append(this.Martix[row, col] + " ");
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
