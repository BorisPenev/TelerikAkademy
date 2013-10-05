using System;

namespace WalkInMatrix
{
    public class WalkInMatrixProgram
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine( "Enter a positive number " );
            //string input = Console.ReadLine(  );
            //int n = 0;
            //while ( !int.TryParse( input, out n ) || n < 0 || n > 100 )
            //{
            //    Console.WriteLine( "You haven't entered a correct positive number" );
            //    input = Console.ReadLine(  );
            //}
            int n = 6;
            Matrix matrix = new Matrix(new int[n, n]);
            matrix.MaxValue = 1;
            matrix.GameMatrix[matrix.CurrentPosition.Row, matrix.CurrentPosition.Col] = matrix.MaxValue;

            while (CanMove(matrix))
            {
                matrix.DirectionLeftCount = Enum.GetNames(typeof(DirectionTypes)).Length;
                matrix.Move();
            }

            Console.WriteLine(matrix.ToString());
        }

        internal static bool CanMove(Matrix matrix)
        {
            if(!IsPossibleMove(matrix))
            {
                if (FindEmptyField(matrix))
                {
                    RestartWalk(matrix);
                    return IsPossibleMove(matrix);
                }
                return false;
            }
            return true;
        }

        internal static bool IsPossibleMove(Matrix matrix)
        {
            if (matrix.DirectionLeftCount <= 0)
            {
                return false;
            }

            if (matrix.IsOutOfBorders())
            {
                if (TryChangeDirection(matrix))
                {
                    return true;
                }
                return false;
            }

            if (matrix.IsEmptyField())
            {
                return true;
            }
            return TryChangeDirection(matrix);
        }

        /// <summary>
        /// Search for empty cell to restart the matrix walk.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>If find an empty cell set Matrix Currect Row and Col to it's coordinates - 1 and return true. Else return false.</returns>
        internal static bool FindEmptyField(Matrix matrix)
        {
            for (int row = 0; row < matrix.GameMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GameMatrix.GetLength(0); col++)
                {
                    if (matrix.GameMatrix[row, col] == 0)
                    {
                        matrix.CurrentPosition.Row = row - 1;
                        matrix.CurrentPosition.Col = col - 1;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Restart default valued of matrix:
        /// DirectionLeftCount and DirectionType
        /// Change direction with TryChangeDirection() and walk with Move()
        /// </summary>
        /// <param name="matrix"></param>
        internal static void RestartWalk(Matrix matrix)
        {
            matrix.DirectionLeftCount = Enum.GetNames(typeof(DirectionTypes)).Length;
            matrix.DirectionType = DirectionTypes.East;
            TryChangeDirection(matrix);
            matrix.Move();
        }

        /// <summary>
        /// After change direction to next set DirectionLeftCount -1 and start IsPossibleMove check to be sure if we can walk to the new direction
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        internal static bool TryChangeDirection(Matrix matrix)
        {
            matrix.DirectionType = (DirectionTypes)((int)matrix.DirectionType + 1);
            switch (matrix.DirectionType)
            {
                case DirectionTypes.South:
                    matrix.Direction.Row = 1;
                    matrix.Direction.Col = 0;
                    break;
                case DirectionTypes.Southwest:
                    matrix.Direction.Row = 1;
                    matrix.Direction.Col = -1;
                    break;
                case DirectionTypes.West:
                    matrix.Direction.Row = 0;
                    matrix.Direction.Col = -1;
                    break;
                case DirectionTypes.Northwest:
                    matrix.Direction.Row = -1;
                    matrix.Direction.Col = -1;
                    break;
                case DirectionTypes.North:
                    matrix.Direction.Row = -1;
                    matrix.Direction.Col = 0;
                    break;
                case DirectionTypes.Northeast:
                    matrix.Direction.Row = -1;
                    matrix.Direction.Col = 1;
                    break;
                case DirectionTypes.East:
                    matrix.Direction.Row = 0;
                    matrix.Direction.Col = 1;
                    break;
                default:
                    matrix.DirectionType = DirectionTypes.Southeast;
                    matrix.Direction.Row = 1;
                    matrix.Direction.Col = 1;
                    break;
            }
            matrix.DirectionLeftCount--;
            return IsPossibleMove(matrix);
        }
    }
}