using System;
using System.Linq;

namespace WalkInMatrix
{
    public static class WalkExtensions
    {
        public static bool IsOutOfBorders(this Matrix matrix)
        {
            int length = matrix.GameMatrix.GetLength(0);
            if (matrix.CurrentPosition.Row + matrix.Direction.Row >= length ||
                matrix.CurrentPosition.Row + matrix.Direction.Row < 0 ||
                matrix.CurrentPosition.Col + matrix.Direction.Col >= length ||
                matrix.CurrentPosition.Col + matrix.Direction.Col < 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsEmptyField(this Matrix matrix)
        {
            if (matrix.GameMatrix[matrix.CurrentPosition.Row + matrix.Direction.Row, matrix.CurrentPosition.Col + matrix.Direction.Col] == 0)
            {
                return true;
            }
            return false;
        }

        internal static void Move(this Matrix matrix)
        {
            matrix.MaxValue++;
            matrix.CurrentPosition.Row += matrix.Direction.Row;
            matrix.CurrentPosition.Col += matrix.Direction.Col;
            matrix.GameMatrix[matrix.CurrentPosition.Row, matrix.CurrentPosition.Col] = matrix.MaxValue;
        }
    }
}
